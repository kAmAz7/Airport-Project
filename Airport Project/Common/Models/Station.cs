using Common.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Models.Common
{
    public class Station
    {
        [Key]
        public int Id { get; set; }
        public int? AirplaneId { get; set; }
        [ForeignKey("AirplaneId")]
        public Airplane Airplane { get; set; }
        [JsonIgnore]
        public ICollection<Station> NextStation { get; set; }
        [JsonIgnore]
        public ICollection<Station> UnUsedCollection { get; set; }
        public bool IsEnterStation { get; set; }
        public StationType TypeOfStation { get; set; }
        [NotMapped] 
        [JsonIgnore]
        public Action<Airplane, Station> PlaneFinished { get; set; }
        [NotMapped]
        [JsonIgnore] 
        public Action StationUpdated { get; set; }

        public Station()
        {
            NextStation = new List<Station>();
        }

        public async Task StartWorking()
        {
            while (true)
            {
                Station nextStation;
                while (Airplane == null || Airplane.AirplaneStatus == AirplaneStatus.Moving)
                {
                    await Task.Delay(200);
                }
                if (((Id == 6 || Id == 7) && Airplane.FlightState == FlightState.Landing) || (Id == 4 && Airplane.FlightState == FlightState.Departuring))
                {
                    Console.Write("");
                }
                var nextStations = NextStation.Where(s => s.TypeOfStation == StationType.StationForLandingAndDeparture || (Airplane.FlightState == FlightState.Departuring ? StationType.StationForDeparture : StationType.StationForLanding) == s.TypeOfStation);
                if (nextStations.Count() == 0)
                {
                    Airplane.AirplaneStatus = AirplaneStatus.Finished;
                    PlaneFinished?.Invoke(Airplane, this);
                    Airplane = null;
                }
                else
                {
                    while (nextStations.Any(s => s.Airplane == null) == false)
                    {
                        await Task.Delay(200);
                    }
                    nextStation = nextStations.First(s => s.Airplane == null);
                    nextStation.Airplane = Airplane;
                    Airplane.AirplaneStatus = AirplaneStatus.Moving;
                    Airplane.Move();
                    Trace.WriteLine($"airplane {Airplane.Id} moved from station {Id} to station {nextStation.Id}");
                    Airplane = null;
                }
                StationUpdated?.Invoke();
            }
        }

        public enum StationType
        {
            StationForLanding,
            StationForDeparture,
            StationForLandingAndDeparture
        }

        public override string ToString()
        {
            return $"Station {Id} is {TypeOfStation}. Has Airplane? => {(Airplane == null ? "Empty" : Airplane.FlightNumber.ToString())}," +
                $" airplane status {(Airplane == null ? "Empty" : Airplane.AirplaneStatus.ToString())}," +
                $" airplane is {(Airplane == null ? "No Airplane" : Airplane.FlightState.ToString())}";
        }
    }
}