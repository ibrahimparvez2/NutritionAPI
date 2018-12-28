using CountingKsDataLayer.Pocos;
using System.Data.Entity;

namespace CountingKsDataLayer
{
    public class CountingKsContext : DbContext
    {

        public CountingKsContext() : base("CountingKsConnectionString")
        {
            this.Configuration.LazyLoadingEnabled = false;
            this.Configuration.ProxyCreationEnabled = false;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            CountingKsMapping.Configure(modelBuilder);
        }


        public DbSet<ApiUser> ApiUsers { get; set; }
        public DbSet<AuthToken> AuthTokens { get; set; }
        public DbSet<Food> Foods { get; set; }
        public DbSet<Measure> Measures { get; set; }
        public DbSet<Diary> Diaries { get; set; }
        public DbSet<DiaryEntry> DiaryEntries { get; set; }
      
    }
}