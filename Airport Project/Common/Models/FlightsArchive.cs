using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Common
{
    public class FlightsArchive
    {
        [Key]
        public int Id { get; set; }
        public int AirplaneId { get; set; }
        [ForeignKey("AirplaneId")]
        public Airplane Airplane { get; set; }
        public int StationId { get; set; }
        [ForeignKey("StationId")]
        public Station Station { get; set; }
        public DateTime? EnteredStartingStationDateTime { get; set; }
        public DateTime? ExitedLastStationDateTime { get; set; }

        public override string ToString()
        {
            return $"Airplane {Airplane.Id} flight {Airplane.FlightNumber} of {Airplane.AirplaneCompany}" +
                $" finished {Airplane.FlightState} on station {Station.Id} at {ExitedLastStationDateTime}";
        }
    }
}
