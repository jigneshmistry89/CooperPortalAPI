namespace Coopers.BusinessLayer.Database.Domain.Migrations
{
    using Models;
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<CooperAtkinEntities>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(CooperAtkinEntities context)
        {
            if (!context.Accounts.Any())
            {
                var accounts = new List<Account>
                {
                    new Account
                    {
                        Name = "Test"
                    },
                     new Account
                    {
                        Name = "Test2"
                    }
                };
                accounts.ForEach(acc => context.Accounts.AddOrUpdate(acc));
                context.SaveChanges();
            }

            if (!context.TaxableStates.Any())
            {
                var states = new List<TaxableStates>
                {
                    new TaxableStates
                    {
                        StateCode = "CT",
                        StateName = "Connecticut",
                        Tax = 6.35
                    },
                     new TaxableStates
                    {
                        StateCode = "NY",
                        StateName = "New York",
                        Tax = 4
                    },
                     new TaxableStates
                    {
                        StateCode = "NJ",
                        StateName = "New Jersey",
                        Tax = 7
                    },
                     new TaxableStates
                    {
                        StateCode = "FL",
                        StateName = "Florida",
                        Tax = 6
                    },
                    new TaxableStates
                    {
                        StateCode = "KS",
                        StateName = "Kansas",
                        Tax = 6.5
                    },
                     new TaxableStates
                    {
                        StateCode = "MN",
                        StateName = "Minnesota",
                        Tax = 6.8
                    },
                      new TaxableStates
                    {
                        StateCode = "CA",
                        StateName = "California",
                        Tax = 7.25
                    }
                };
                states.ForEach(x => context.TaxableStates.AddOrUpdate(x));
                context.SaveChanges();
            }

            if (!context.Users.Any())
            {
                var users = new List<User>
                {
                    new User
                    {
                        AccountID = 2,
                        UserName = "User2",
                        Password = "Lnw1V96KyY9r0JbKJEO32g==",
                        FirstName = "Bill",
                        LastName = "LastName",
                        NotificationEmail = "Reclamationbin@gmail.com",
                        NotificationPhone = "555-555-1234"
                    }
                };
                users.ForEach(user => context.Users.AddOrUpdate(user));
                context.SaveChanges();
            }


            if (!context.Locations.Any())
            {

                var locations = new List<Location>
                {
                    new Location
                    {
                        Title = "Boston Pizza 203",
                        Address = "1502 8th Street   Saskatoon  Saskatchewan S7S 1P4 Canada",
                        Latitude = 52.150813,
                        Longitude = -106.567821,
                        AccountID = 2
                    },
                    new Location {
                        Title = "Boston Pizza 207",
                        Address = "226 Broadway Street E  Yorkton Saskatchewan S3N 4C3 Canada",
                        Latitude = 51.209425,
                        Longitude = -102.449612,
                        AccountID = 2
                    },
                     new Location {
                        Title = "Boston Pizza 208",
                        Address = "3250 2nd Avenue West  Prince Albert Saskatchewan S6V 5E9 Canada",
                        Latitude = 53.183366,
                        Longitude = -105.759101,
                        AccountID = 2
                    },
                     new Location {
                        Title = "Boston Pizza 211",
                        Address = "11434 Railway Ave  North Battleford Saskatchewan S9A 3G8 Canada",
                        Latitude = 52.759558,
                        Longitude = -108.273011,
                        AccountID = 2
                    }
                };
                locations.ForEach(location => context.Locations.AddOrUpdate(location));
                context.SaveChanges();
            }

            if (!context.LocationNetworks.Any())
            {
                var locNetworks = new List<LocationNetwork>
                {
                    new LocationNetwork
                    {
                        LocationID = 1,
                        NetworkID =1000
                    },
                    new LocationNetwork
                    {
                        LocationID = 1,
                        NetworkID =1004
                    },
                    new LocationNetwork
                    {
                        LocationID = 1,
                        NetworkID =1534
                    },
                    new LocationNetwork
                    {
                        LocationID = 1,
                        NetworkID =1587
                    },
                    new LocationNetwork
                    {
                        LocationID = 1,
                        NetworkID =1588
                    },
                     new LocationNetwork
                    {
                        LocationID = 1,
                        NetworkID =1589
                    },
                };

                locNetworks.ForEach(locNet => context.LocationNetworks.AddOrUpdate(locNet));
                context.SaveChanges();
            }
        }
    }
}
