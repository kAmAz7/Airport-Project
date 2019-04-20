using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Interfaces
{
    public interface IAirplaneService
    {
        void Register<T>(string eventName, Action<T> onData);
        void Invoke<T>(string eventName, T data);
    }
}
