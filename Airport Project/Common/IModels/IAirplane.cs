using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.IModels
{
    public interface IAirplane
    {
        int Id { get; set; }
        int FlightNumber { get; set; }
        string FlightCompany { get; set; }
        FlightState FlightState { get; set; }
        DateTime EnteredLandingStationDateTime { get; set; }
        DateTime ExitedLandingStationDateTime { get; set; }
        DateTime EnteredDeparturingStationDateTime { get; set; }
        DateTime ExitedDeparturingStationDateTime { get; set; }
    }

    public enum FlightState
    {
        Landing,
        Departures
    }
}
