using AutoMapper;
using DotNetUnitTestSelfLearn.Controllers;
using DotNetUnitTestSelfLearn.Data;
using DotNetUnitTestSelfLearn.Helper;
using DotNetUnitTestSelfLearn.Model;
using FluentAssertions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace UnitTestSelfLearnTest
{
    public class UnitTestGame
    {
        private readonly ITestOutputHelper _testOutputHelper;
        public UnitTestGame (ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }
        List<GameModel> GetTestGames()
        {
            var games = new List<GameModel>();
            games.Add(new GameModel()
            {
                GameID = 1,
                GameName = "Left 4 Dead 2",
                GamePrice = 9.99,
                
            });
            games.Add(new GameModel()
            {
                GameID = 2,
                GameName = "DotA 2",
                GamePrice = 0.00,

            });
            games.Add(new GameModel()
            {
                GameID = 3,
                GameName = "HomeFront The Revolution",
                GamePrice = 39.99,

            });
            games.Add(new GameModel()
            {
                GameID = 4,
                GameName = "Dying Light 2",
                GamePrice = 79.99,

            });
            return games;
        }

        [Fact]
        public async Task GetAllGames_ReturnOk()
        {
            // Arrange
            // Inside our IGeneralRepository, that's where all our ORM methods are in. 
            var mockRepo = new Mock<IGeneralRepository>();
            mockRepo.Setup(_ => _.GetAllGames()).ReturnsAsync(GetTestGames());
            var myProfile = new AutoMapperProfiles();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(myProfile));
            var mockMapper = new Mapper(configuration);

            var mockEnvironment = new Mock<IWebHostEnvironment>();
            //...Setup the mock as needed
            mockEnvironment
                .Setup(m => m.EnvironmentName)
                .Returns("Hosting:UnitTestEnvironment");

            var controller = new GameController(mockRepo.Object, mockMapper, mockEnvironment.Object);

            /// Act
            var result = await controller.GetAllExistingGames();


            // Assert
            
            Assert.IsType<ActionResult<List<GameModel>>>(result);
            // FluentAssertions
            result.Result.Should().BeOfType<OkObjectResult>()
                .Which.StatusCode.Should().Be((int)HttpStatusCode.OK);
            _testOutputHelper.WriteLine("Check all From GameList");

        }

        [Fact]
        public async Task GetIndividualUser_ReturnOk()
        {

            // Arrange
            var mockRepo = new Mock<IGeneralRepository>();
            /*mockRepo.Setup(repo => repo.GetGameByID(1))
                .ReturnsAsync(GetTestGames()[0]);
            mockRepo.Setup(repo => repo.GetGameByID(0))
                .Returns<Task<GameModel>>(null);*/
            mockRepo.Setup(repo => repo.GetGameByID(2))
                .ReturnsAsync(GetTestGames()[1]);
            var myProfile = new AutoMapperProfiles();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(myProfile));
            var mockMapper = new Mapper(configuration);

            var mockEnvironment = new Mock<IWebHostEnvironment>();
            //...Setup the mock as needed
            mockEnvironment
                .Setup(m => m.EnvironmentName)
                .Returns("Hosting:UnitTestEnvironment");

            var controller = new GameController(mockRepo.Object, mockMapper, mockEnvironment.Object);

            // Act
            var actionResult = await controller.GetExistingGameByID(2);

            // Assert
            Assert.IsType<ActionResult<GameModel>>(actionResult);
            actionResult.Result.Should().BeOfType<OkObjectResult>()
                .Which.StatusCode.Should().Be((int)HttpStatusCode.OK);
        }

        [Fact]
        public async Task GetIndividualUserWrongID_ReturnNotFound()
        {

            // Arrange
            var mockRepo = new Mock<IGeneralRepository>();
            mockRepo.Setup(repo => repo.GetGameByID(3))
                .Returns<Task<GameModel>>(null) ;

            var myProfile = new AutoMapperProfiles();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(myProfile));
            var mockMapper = new Mapper(configuration);

            var mockEnvironment = new Mock<IWebHostEnvironment>();
            //...Setup the mock as needed
            mockEnvironment
                .Setup(m => m.EnvironmentName)
                .Returns("Hosting:UnitTestEnvironment");

            var controller = new GameController(mockRepo.Object, mockMapper, mockEnvironment.Object);

            // Act
            var actionResult = await controller.GetExistingGameByID(3);

            // Assert
            Assert.IsType<ActionResult<GameModel>>(actionResult);
            actionResult.Result.Should().BeOfType<NotFoundObjectResult>()
                .Which.StatusCode.Should().Be((int)HttpStatusCode.NotFound);
        }
    }
}