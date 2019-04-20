using BL;
using Common.Interfaces;
using Common.Models;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AirplaneAPI.MyHub
{
    [HubName("AirportHub")]
    public class AirportHub : Hub
    {

        public void SendAirplaneToClient(Airplane plane)
        {
            Clients.All.ReceiveAirplane(plane);
        }

        public void SendStationsToClient(Station[] stations)
        {
            Clients.All.ReceiveStations(stations);
        }

        public void SendFlightsArchiveToClient(FlightsArchive[] archives)
        {
            Clients.All.ReceiveArchives(archives);
        }

        public void SendPlane(Airplane plane)
        {
            Clients.All.GetPlane(plane);
        }
    }
}