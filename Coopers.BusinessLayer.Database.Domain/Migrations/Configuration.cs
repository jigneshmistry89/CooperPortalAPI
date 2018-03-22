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

        }
    }
}
