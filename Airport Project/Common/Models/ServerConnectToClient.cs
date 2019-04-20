using Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Models
{
    public class ServerConnectToClient
    {
        public List<Airplane> Airplanes { get; set; }
        public List<Station> Stations { get; set; }
        public List<FlightsArchive> FlightsArchives { get; set; }
    }
}
