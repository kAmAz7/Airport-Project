using Models.Common;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Interfaces
{
    public interface IStationManager
    {
        List<Station> ListOfAllStations { get; set; }
        List<FlightsArchive> AllFlightsArchiveInAirport { get; set; }
        void UpdateStations();
        Task<Airplane> SetAirplaneOnStartingStationAsync(Airplane airplane);
        void SendAirplaneToArchiveList(Airplane airplane, Station station);
    }
}
