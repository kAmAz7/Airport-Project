using BL;
using Microsoft.AspNet.SignalR.Client;
using Models.Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using WPFUIClient.Commands;
using ListView = System.Windows.Controls.ListView;

namespace WPFUIClient.ViewModels
{
    class MainViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public HubConnection HubConnection { get; set; }
        public IHubProxy HubProxy { get; set; }

        private ObservableCollection<Airplane> _airplanes;
        public ObservableCollection<Airplane> Airplanes
        {
            get { return _airplanes; }
            set { _airplanes = value; OnPropertyChanged(); }
        }

        private ObservableCollection<Station> _stations;
        public ObservableCollection<Station> Stations
        {
            get { return _stations; }
            set { _stations = value; OnPropertyChanged(); }
        }

        private ObservableCollection<FlightsArchive> _flightsArchive;
        public ObservableCollection<FlightsArchive> FlightsArchive
        {
            get { return _flightsArchive; }
            set { _flightsArchive = value; OnPropertyChanged(); }
        }

        public MainViewModel()
        {
            AirportManager Manager = new AirportManager();
            Airplanes = new ObservableCollection<Airplane>();
            Stations = new ObservableCollection<Station>();
            FlightsArchive = new ObservableCollection<FlightsArchive>();
            HubConnection = new HubConnection("http://localhost:65334/");
            HubProxy = HubConnection.CreateHubProxy("AirportHub");
            HubProxy.On<Airplane>("ReceiveAirplane", (plane) => Application.Current.Dispatcher.Invoke(() => Airplanes.Add(plane)));
            HubProxy.On<Station[]>("ReceiveStations", (station) => Application.Current.Dispatcher.Invoke(() => Stations = new ObservableCollection<Station>(station)));
            HubProxy.On<FlightsArchive[]>("ReceiveArchives", (archive) => Application.Current.Dispatcher.Invoke(() => FlightsArchive = new ObservableCollection<FlightsArchive>(archive)));
            HubConnection.Start().Wait();
        }

        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
