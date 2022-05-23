using DotNetUnitTestSelfLearn.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace DotNetUnitTestSelfLearn.Data
{
    public class GeneralRepository : IGeneralRepository, IDisposable
    {
        private GameContext _context;
        public GeneralRepository(GameContext gameContext)
        {
            _context = gameContext;
        }
        public Task<List<GameModel>> GetAllGames()
        {
            return _context.GameModels.ToListAsync();
        }

        // get game by id
        public Task<GameModel?> GetGameByID(int id)
        {
            
            return _context.GameModels.AsNoTracking().FirstOrDefaultAsync(a => a.GameID == id);
        }

        // get game by game name
        public Task<GameModel?> GetGameByGameName(string gameName)
        {
            return _context.GameModels.AsNoTracking().FirstOrDefaultAsync(game => game.GameName == gameName);
        }


        public ValueTask<EntityEntry<GameModel>> AddGameAsync(GameModel gameObj)
        {
            return _context.GameModels.AddAsync(gameObj);
        }

        public void SetGameEntityToModified(GameModel game)
        {
            _context.Entry(game).State = EntityState.Modified;
        }

        public void RemoveGame(GameModel game)
        {
            _context.GameModels.Remove(game);
        }



        //============ Generic functions ==============================
        public Task<int> SaveChangesAsync()
        {
            return _context.SaveChangesAsync();
        }

        //============ dispose functions ==============================
        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
