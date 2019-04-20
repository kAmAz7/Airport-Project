using Common.Interfaces;
using DAL.Repository;
using Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class AirplaneManager : IAirplaneManager
    {
        IDBRepository repository;

        public AirplaneManager()
        {
            repository = new DBRepository();
        }
        public AirplaneManager(IDBRepository repositoryDB) : this()
        {
            repository = repositoryDB;
        }

        public void AddAirplane(Airplane airplane)
        {
            repository.AddAirplane(airplane);
        }

        public List<FlightsArchive> AllArchivedFlights()
        {
            var allArchivedFlights = new List<FlightsArchive>();
            allArchivedFlights.AddRange(repository.GetAllArchivedFlights());
            return allArchivedFlights;
        }

        public void UpdatePlane(Airplane airplane)
        {
            repository.UpdateAirplane(airplane);
        }

    }
}
