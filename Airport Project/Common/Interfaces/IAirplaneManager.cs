using Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Interfaces
{
    public interface IAirplaneManager
    {
        void AddAirplane(Airplane airplane);
        void UpdatePlane(Airplane airplane);
        List<FlightsArchive> AllArchivedFlights();
    }
}
