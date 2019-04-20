using Common.Interfaces;
using Microsoft.AspNet.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class AirplaneService : IAirplaneService
    {
        IHubProxy proxy;
        HubConnection connection;
        public AirplaneService()
        {
            string uri = "http://localhost:65334";
            connection = new HubConnection(uri);
            proxy = connection.CreateHubProxy("AirportHub");
            connection.Start().Wait();
        }

        public void Invoke<T>(string eventName, T data)
        {
            proxy.Invoke(eventName, data);
        }

        public void Register<T>(string eventName, Action<T> onData)
        {
            proxy.On(eventName, onData);
        }
    }
}
