using Common.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Timers;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Models.Common
{
    public class Airplane
    {
        [Key]
        public int Id { get; set; }
        public int FlightNumber { get; set; }
        public AirplaneCompany AirplaneCompany { get; set; }
        public FlightState FlightState { get; set; }
        [NotMapped]
        public AirplaneStatus AirplaneStatus { get; set; }
        public DateTime? EnteredStartingStationDateTime { get; set; }
        public DateTime? ExitedLastStationDateTime { get; set; }
        Timer moving = new Timer();
        Random rnd = new Random();

        public Airplane()
        {
            moving.AutoReset = false;
            moving.Elapsed += (sender, e) => AirplaneStatus = AirplaneStatus.ReadyToMove;
        }

        public void Move()
        {
            moving.Interval = rnd.Next(3000, 5000);
            AirplaneStatus = AirplaneStatus.Moving;
            moving.Start();
        }

        public override string ToString()
        {
            return $"Airplane {Id} flightnumber {FlightNumber} of {AirplaneCompany} is {FlightState}," +
                $" {FlightState}-entered-station time is: {EnteredStartingStationDateTime}, airplanestatus: {AirplaneStatus}";
        }
    }

    public enum FlightState
    {
        Landing,
        Departuring
    }

    public enum AirplaneCompany
    {
        Aeroflot,
        AirCanada,
        ELAL,
        Lufthansa,
        AmericanAirlaines,
        RyanAir,
        WizzAir
    }

    public enum AirplaneStatus
    {
        Moving,
        ReadyToMove,
        Finished
    }

}
