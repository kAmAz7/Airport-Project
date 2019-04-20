using Models.Common;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Interfaces
{
    public interface IDBRepository
    {
        void AddAirplane(Airplane airplane);

        void UpdateAirplane(Airplane airplane);

        List<Airplane> GetLandedAirplanes();

        List<Airplane> GetDeparturedAirplanes();

        List<Airplane> GetAllPlanes();

        void AddAirplaneToArchive(FlightsArchive airplane);

        List<FlightsArchive> GetAllArchivedFlights();

        void AddStation(Station station);

        void AddStations(List<Station> stations);

        void UpdateStations(Station[] stations);

        List<Station> GetLandingStations();

        List<Station> GetDeparturingStations();

        List<Station> GetAllStations();
    }
}
