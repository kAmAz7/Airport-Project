using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.IModels
{
    public interface IStation
    {
        int Id { get; set; }
        IAirplane Airplane { get; set; }
        bool IsThereAnAirplaneInStation { get; set; }
        List<IStation> NetxLandingStations { get; set; }
        List<IStation> NetxDepartureStations { get; set; }
    }

    public enum WhatKindOfStation
    {
        StationForLanding,
        StationForDeparture,
        StationForLandingAndDeparture
    }
}
