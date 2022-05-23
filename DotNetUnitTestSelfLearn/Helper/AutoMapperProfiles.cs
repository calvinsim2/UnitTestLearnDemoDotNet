using AutoMapper;
using DotNetUnitTestSelfLearn.DTO;
using DotNetUnitTestSelfLearn.Model;

namespace DotNetUnitTestSelfLearn.Helper
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<GameDTO, GameModel>();
            CreateMap<GameModel, GameDTO>();
        }
            
    }
}
