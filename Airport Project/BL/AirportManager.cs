using Common.Interfaces;
using Microsoft.AspNet.SignalR.Client;
using Models.Common;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;
using static Models.Common.Station;

namespace BL
{
    public class AirportManager : IAirportManager
    {
        IAirplaneManager AManager;
        IStationManager SManager;
        public List<Airplane> AllPlanesInAirport { get; set; }
        public List<FlightsArchive> AllFlightsArchiveInAirport { get; set; }
        public AirplaneService AService { get; set; }
        public List<Station> AllStationsInAirport { get; set; }

        public AirportManager(IAirplaneManager airplaneManager, IStationManager stationManager) : this()
        {
            AManager = airplaneManager;
            SManager = stationManager;
        }
        public AirportManager(IAirplaneManager airplaneManager) : this()
        {
            AManager = airplaneManager;
        }
        public AirportManager(IStationManager stationManager) : this()
        {
            SManager = stationManager;
        }
        public AirportManager()
        {
            AManager = new AirplaneManager();
            AllPlanesInAirport = new List<Airplane>();
            AService = new AirplaneService();
            SManager = new StationManager();
            AllStationsInAirport = SManager.ListOfAllStations;
            AllFlightsArchiveInAirport = SManager.AllFlightsArchiveInAirport;
            var archivedFlights = AManager.AllArchivedFlights();
            for (int i = 0; i < archivedFlights.Count(); i++)
            {
                AllFlightsArchiveInAirport.Add(archivedFlights[i]);
            }
            AService.Register<Airplane>("GetPlane", AddAirplaneToQueueFromServer);
        }

        public void AddAirplaneToQueueFromServer(Airplane airplane)
        {
            AllPlanesInAirport.Add(airplane);
            AManager.AddAirplane(airplane);
            AService.Invoke("SendAirplaneToClient", airplane);
            SManager.SetAirplaneOnStartingStationAsync(airplane).ContinueWith(t => t.Result.Move());
        }

        public void UpdatePlane(Airplane airplane)
        {
            AManager.UpdatePlane(airplane);
        }
    }
}
