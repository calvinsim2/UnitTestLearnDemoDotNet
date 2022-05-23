using System.ComponentModel.DataAnnotations;

namespace DotNetUnitTestSelfLearn.Model
{
    public class GameModel
    {
        [Key]
        public int GameID { get; set; }

        [Required]
        public string GameName { get; set; }
        public double GamePrice  { get; set; }


    }
}
