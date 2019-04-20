using Common.Models;
using Models.Common;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DBContext
{
    public class AirportProjectDb : DbContext
    {
        public virtual DbSet<Airplane> Airplanes { get; set; }
        public virtual DbSet<Station> Stations { get; set; }
        public virtual DbSet<FlightsArchive> FlightsArchives { get; set; }

        public AirportProjectDb() : base("name=AirportDB")
        {
            Database.SetInitializer(new DbSeedInitializer());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Station>().HasMany(s => s.NextStation).WithMany(s => s.UnUsedCollection).Map(m =>
            {
                m.MapLeftKey("StationId");
                m.MapRightKey("ConnectedStationId");
                m.ToTable("StationConnections");
            });
        }
    }

    public class DbSeedInitializer : DropCreateDatabaseAlways<AirportProjectDb>
    {
        protected override void Seed(AirportProjectDb context)
        {
            var stations = new List<Station> {
                new Station{Id=1,Airplane=null,IsEnterStation=true,TypeOfStation=Station.StationType.StationForLanding },
                new Station{Id=2,Airplane=null,IsEnterStation=false,TypeOfStation=Station.StationType.StationForLanding},
                new Station{Id=3,Airplane=null,IsEnterStation=false,TypeOfStation=Station.StationType.StationForLanding },
                new Station{Id=4,Airplane=null,IsEnterStation=false,TypeOfStation=Station.StationType.StationForLandingAndDeparture  },
                new Station{Id=5,Airplane=null,IsEnterStation=false,TypeOfStation=Station.StationType.StationForLanding },
                new Station{Id=6,Airplane=null,IsEnterStation=true,TypeOfStation=Station.StationType.StationForLandingAndDeparture },
                new Station{Id=7,Airplane=null,IsEnterStation=true,TypeOfStation=Station.StationType.StationForLandingAndDeparture },
                new Station{Id=8,Airplane=null,IsEnterStation=false,TypeOfStation=Station.StationType.StationForDeparture  }
            };

            stations[0].NextStation.Add(stations[1]);
            stations[1].NextStation.Add(stations[2]);
            stations[2].NextStation.Add(stations[3]);
            stations[3].NextStation.Add(stations[4]);
            stations[4].NextStation.Add(stations[5]);
            stations[4].NextStation.Add(stations[6]);
            stations[5].NextStation.Add(stations[7]);
            stations[6].NextStation.Add(stations[7]);
            stations[7].NextStation.Add(stations[3]);
            context.Stations.AddRange(stations);
            context.SaveChanges();
        }
    }
}

