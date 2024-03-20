
namespace ProjectAPI.Models
{
    public class RootObject
    {
        public int count { get; set; }
        public string next { get; set; }
        public string previous { get; set; }
        public List<Result> results { get; set; }
    }
    public class Result
    {
        public string name { get; set; }
        public string url { get; set; }
    }
    public class PokemonViewModel
    {
        public string Name { get; set; }
        public string Url { get; set; }
    }
    public class PokemonDetailViewModel
    {
        public string name { get; set; }
        public int height { get; set; }
        public int id { get; set; }
        public int weight { get; set; }
        public int base_experience { get; set; }
        public sprites sprites { get; set; }
        public PokemonSpeciesViewModel PokemonSpeciesViewModel { get; set; }
        public List<Moves> moves { get; set; }

    }
    public class PokemonSpeciesViewModel
    {
        public int base_happiness { get; set; }
        public int capture_rate { get; set; }

        public habitat habitat { get; set; } //object not array
        public color color { get; set; } //object not array
    }
    public class habitat
    {
        public string name { get; set; }
    }
    public class color
    {
        public string name { get; set; }
    }
    public class sprites
    {
        public string front_default { get; set; }
    }
    public class PokemonTypeViewModel
    {
        public int count { get; set; }

    }
    public class PokemonAbilityViewModel
    {
        public int count { get; set; }


    }
    public class Move
    {
       public string name { get;set; }

    }
    public class Moves
    {
        public Move move { get; set; }

    }
}
