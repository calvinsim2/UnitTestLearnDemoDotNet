using DotNetUnitTestSelfLearn.Model;
using Microsoft.EntityFrameworkCore;

namespace DotNetUnitTestSelfLearn.Data
{
    public class GameContext : DbContext
    {
        public GameContext(DbContextOptions<GameContext> options) : base(options)
        {

        }
        //game portion
        public DbSet<GameModel> GameModels { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            modelBuilder.Entity<GameModel>().ToTable("Game");
            
        }
    }
}
