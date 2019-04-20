using Common.Interfaces;
using DAL.Repository;
using Models.Common;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Models.Common.Station;

namespace BL
{
    public class StationManager : IStationManager
    {
        IDBRepository repository;
        public List<Station> ListOfAllStations { get; set; }
        public List<FlightsArchive> AllFlightsArchiveInAirport { get; set; }
        public IAirplaneService AService { get; set; }

        public StationManager(IDBRepository repositoryDB) : this()
        {
            repository = repositoryDB;
        }
        public StationManager()
        {
            AService = new AirplaneService();
            AllFlightsArchiveInAirport = new List<FlightsArchive>();
            repository = new DBRepository();
            ListOfAllStations = repository.GetAllStations().OrderBy(s => s.Id).ToList();
            foreach (var station in ListOfAllStations)
            {
                station.PlaneFinished = (plane, stationnn) =>
                {
                    SendAirplaneToArchiveList(plane, stationnn);
                    UpdateStations();
                };
                station.StationUpdated = UpdateStations;
                var t = station.StartWorking();
            }
            UpdateStations();
        }

        public void UpdateStations()
        {
            var stations = ListOfAllStations.ToArray();
            AService.Invoke("SendStationsToClient", stations);
            repository.UpdateStations(stations);
        }

        public Task<Airplane> SetAirplaneOnStartingStationAsync(Airplane airplane)
        {
            List<Station> stations = stations = ListOfAllStations.Where(s => (s.TypeOfStation == (airplane.FlightState == FlightState.Landing ? StationType.StationForLanding : StationType.StationForDeparture) || s.TypeOfStation == StationType.StationForLandingAndDeparture) && s.IsEnterStation).ToList();
            return Task.Run(async () =>
           {
               while (stations.Any(s => s.Airplane == null) == false)
                   await Task.Delay(200);
               Station station = stations.First(s => s.Airplane == null);
               station.Airplane = airplane;
               airplane.AirplaneStatus = AirplaneStatus.Moving;
               airplane.EnteredStartingStationDateTime = DateTime.Now;
               Trace.WriteLine($"airplane {airplane.Id} started {(airplane.FlightState == FlightState.Landing ? "Landing" : "Departure")} on station {station.Id}");
               return airplane;
           });
        }

        public void SendAirplaneToArchiveList(Airplane airplane, Station station)
        {
            airplane.ExitedLastStationDateTime = DateTime.Now;
            repository.UpdateAirplane(airplane);
            FlightsArchive archive = new FlightsArchive { Airplane = airplane, Station = station, EnteredStartingStationDateTime = airplane.EnteredStartingStationDateTime, ExitedLastStationDateTime = DateTime.Now };
            AllFlightsArchiveInAirport.Add(archive);
            repository.AddAirplaneToArchive(archive);
            AService.Invoke("SendFlightsArchiveToClient", AllFlightsArchiveInAirport);
        }
    }
}

