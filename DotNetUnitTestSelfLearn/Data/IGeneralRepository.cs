using DotNetUnitTestSelfLearn.Model;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace DotNetUnitTestSelfLearn.Data
{
    public interface IGeneralRepository
    {
        
        Task<List<GameModel>> GetAllGames();
        Task<GameModel?> GetGameByGameName(string gameName);
        Task<GameModel?> GetGameByID(int id);

        ValueTask<EntityEntry<GameModel>> AddGameAsync(GameModel gameObj);


        // generic methods
        Task<int> SaveChangesAsync();
    }
}
