using Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Interfaces
{
    public interface IAirportManager
    {
        void AddAirplaneToQueueFromServer(Airplane airplane);
        void UpdatePlane(Airplane airplane);
    }
}
