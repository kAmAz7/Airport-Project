using Common.Interfaces;
using DAL.DBContext;
using Models.Common;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using Newtonsoft.Json;

namespace DAL.Repository
{
    public class DBRepository : IDBRepository
    {
        public void AddAirplane(Airplane airplane)
        {
            using (var context = new AirportProjectDb())
            {
                context.Airplanes.Add(airplane);
                context.SaveChanges();
            }
        }

        public void UpdateAirplane(Airplane airplane)
        {
            using (var context = new AirportProjectDb())
            {
                context.Airplanes.AddOrUpdate(airplane);
                context.SaveChanges();
            }
        }

        public List<Airplane> GetLandedAirplanes()
        {
            using (var context = new AirportProjectDb())
            {
                return context.Airplanes.Where(p => p.FlightState == FlightState.Landing).ToList();
            }
        }

        public List<Airplane> GetDeparturedAirplanes()
        {
            using (var context = new AirportProjectDb())
            {
                return context.Airplanes.Where(p => p.FlightState == FlightState.Departuring).ToList();
            }
        }

        public List<Airplane> GetAllPlanes()
        {
            using (var context = new AirportProjectDb())
            {
                return context.Airplanes.ToList();
            }
        }

        public void AddAirplaneToArchive(FlightsArchive AirplaneArchive)
        {
            using (var context = new AirportProjectDb())
            {
                FlightsArchive fl = JsonConvert.DeserializeObject<FlightsArchive>(JsonConvert.SerializeObject(AirplaneArchive));
                fl.AirplaneId = AirplaneArchive.Airplane.Id;
                fl.StationId = AirplaneArchive.Station.Id;
                fl.Station = null;
                fl.Airplane = null;
                context.FlightsArchives.Add(fl);
                context.SaveChanges();
            }
        }

        public List<FlightsArchive> GetAllArchivedFlights()
        {
            using (var context = new AirportProjectDb())
            {
                return context.FlightsArchives.Include(s => s.Station).Include(a => a.Airplane).ToList();
            }
        }

        public void AddStation(Station station)
        {
            using (var context = new AirportProjectDb())
            {
                context.Stations.Add(station);
                context.SaveChanges();
            }
        }

        public void AddStations(List<Station> stations)
        {
            using (var context = new AirportProjectDb())
            {
                context.Stations.AddRange(stations.ToArray());
                context.SaveChanges();
            }
        }

        public void UpdateStations(Station[] stations)
        {
            using (var context = new AirportProjectDb())
            {
                for (int i = 0; i < stations.Length; i++)
                {
                    if (stations[i].Airplane != null)
                    {
                        context.Airplanes.Attach(stations[i].Airplane);
                    }
                    var newStation = stations[i];
                    var oldStation = context.Stations.First(s => s.Id == newStation.Id);
                    oldStation.Airplane = newStation.Airplane;
                    context.Entry(oldStation).State = EntityState.Modified;
                }
                context.SaveChanges();
            }
        }

        public List<Station> GetLandingStations()
        {
            using (var context = new AirportProjectDb())
            {
                return context.Stations.Where(s => s.TypeOfStation == Station.StationType.StationForLanding).ToList();
            }
        }

        public List<Station> GetDeparturingStations()
        {
            using (var context = new AirportProjectDb())
            {
                return context.Stations.Where(s => s.TypeOfStation == Station.StationType.StationForDeparture).ToList();
            }
        }

        public List<Station> GetAllStations()
        {
            using (var context = new AirportProjectDb())
            {
                return context.Stations.Include(a => a.Airplane).Include(s => s.NextStation).ToList();
            }
        }
    }
}
