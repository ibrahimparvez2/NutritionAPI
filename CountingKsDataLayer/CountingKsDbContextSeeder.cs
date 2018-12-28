using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CountingKsDataLayer
{
    public class CountingKsDbContextSeeder : CreateDatabaseIfNotExists<CountingKsContext>
    {

        protected override void Seed(CountingKsContext context)
        {
            new CountingKsSeeder(context).Seed();
        }

        public CountingKsDbContextSeeder(CountingKsContext ctx)
        {
            new CountingKsSeeder(ctx).Seed();
        }
   }
}
