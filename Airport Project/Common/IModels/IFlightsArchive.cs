using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.IModels
{
    public interface IFlightsArchive
    {
        int Id { get; set; }
        IAirplane Airplane { get; set; }
        IStation Station { get; set; }
    }
}
