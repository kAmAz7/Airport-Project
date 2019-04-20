using Microsoft.AspNet.SignalR.Client;
using Models.Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace SimulatorFlights
{
    class Simulator
    {
        private Airplane newAirplane;
        static Random rand;
        static int id = 1;
        Timer timer;
        const string uri = "http://localhost:65334/";
        HubConnection connection;
        IHubProxy proxy;

        public Simulator()
        {
            timer = new Timer();
            rand = new Random();
            int time = rand.Next(4000, 7000);
            timer.Interval = time;
            timer.Elapsed += Timer_Elapsed;
            connection = new HubConnection(uri);
            proxy = connection.CreateHubProxy("AirportHub");
            connection.Start().Wait();
            timer.Start();
        }

        public void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            newAirplane = CreateRandomAirplane();
            Console.WriteLine("Airplane {0}, Flight number {1} with {2} company is {3} , flights' {4} time is {5}",
            newAirplane.Id, newAirplane.FlightNumber, newAirplane.AirplaneCompany,
            newAirplane.FlightState, newAirplane.FlightState, newAirplane.EnteredStartingStationDateTime);
            SendCreatedAirplane(newAirplane);
        }

        public Airplane CreateRandomAirplane()
        {
            Random rand = new Random();
            Array values1 = Enum.GetValues(typeof(FlightState));
            FlightState randomFlightState = (FlightState)values1.GetValue(rand.Next(values1.Length));
            Array values2 = Enum.GetValues(typeof(AirplaneCompany));
            AirplaneCompany randomCompany = (AirplaneCompany)values2.GetValue(rand.Next(values2.Length));
            int randomflightNumber = rand.Next(1000, 9000);
            Airplane a = new Airplane()
            {
                Id = id++,
                FlightState = randomFlightState,
                AirplaneStatus = AirplaneStatus.ReadyToMove,
                AirplaneCompany = randomCompany,
                FlightNumber = randomflightNumber,
                EnteredStartingStationDateTime = DateTime.Now
            };
            return a;
        }
    
        public void SendCreatedAirplane(Airplane airplane)
        {
            proxy.Invoke("SendPlane", airplane);
        }

        static void Main(string[] args)
        {
            Simulator s = new Simulator();
            Console.ReadKey();
        }
    }
}
