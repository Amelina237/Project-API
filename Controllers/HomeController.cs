using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.AspNetCore.Mvc;
using ProjectAPI.Models;
using System.Diagnostics;
using System.Net;
using System;
using System.Text.Json;


namespace ProjectAPI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult Pokemon()
        {
            return View();
        }
        public IActionResult Dashboard()
        {
            string pokemon = getPokemon();
            string pokemonType = getPokemonType();
            string pokemonAbility = getPokemonAbility();

            RootObject rootObject = JsonSerializer.Deserialize<RootObject>(pokemon);

            List<PokemonViewModel> pokemonViewModels = rootObject.results.Select(r => new PokemonViewModel
            {
                Name = r.name,
                Url = r.url,
            }).ToList();

            int countNo = rootObject.count;

            PokemonTypeViewModel PokemonType  = JsonSerializer.Deserialize<PokemonTypeViewModel>(pokemonType);
            PokemonAbilityViewModel PokemonAbility = JsonSerializer.Deserialize<PokemonAbilityViewModel>(pokemonAbility);
            // Set the total number of Pokémon in the ViewBag
            ViewBag.CurrentPokemonCount = pokemonViewModels.Count;
            ViewBag.TotalPokemon = countNo;
            ViewBag.ListName = pokemonViewModels;
            ViewBag.TotalTypePokemon = PokemonType.count;
            ViewBag.TotalAbilityPokemon = PokemonAbility.count;

            return View();
        }
        public IActionResult PokemonTwo()
        {
            string pokemon = getPokemon();

            RootObject rootObject = JsonSerializer.Deserialize<RootObject>(pokemon);

            List<PokemonViewModel> pokemonViewModels = rootObject.results.Select(r => new PokemonViewModel
            {
                Name = r.name,
                Url = r.url,
            }).ToList();
           

            return View(pokemonViewModels);
        }
        public IActionResult getPokemonDetail(string pokemonName)
        {
            // Retrieve the JSON data from the getPokemonDetailJson action method
            string pokemonJson = getPokemonDetailJson(pokemonName);
            string pokemonSpecies = getPokemonSpeciesJson(pokemonName);

            PokemonDetailViewModel model = JsonSerializer.Deserialize<PokemonDetailViewModel>(pokemonJson);
            
            PokemonSpeciesViewModel speciesModel = JsonSerializer.Deserialize<PokemonSpeciesViewModel>(pokemonSpecies);
            model.PokemonSpeciesViewModel = speciesModel;
            
            return View(model);
        }


        #region Get Json Data
        public string getPokemonSpeciesJson(string pokemonName)
        {


            string url1 = $"https://pokeapi.co/api/v2/pokemon-species/{pokemonName}";

            using (WebClient client = new WebClient())
            {
                string json = client.DownloadString(url1);   //get the data from website
                                                             //RootObject rootObject = JsonSerializer.Deserialize<RootObject>(json);
                return json;
            }

        }
        public string getPokemonDetailJson(string pokemonName)
        {


            string url1 = $"https://pokeapi.co/api/v2/pokemon/{pokemonName}";

            using (WebClient client = new WebClient())
            {
                string json = client.DownloadString(url1);   //get the data from website
                                                             //RootObject rootObject = JsonSerializer.Deserialize<RootObject>(json);

                //List<PokemonViewModel> pokemonViewModels = rootObject.results.Select(r => new PokemonViewModel
                //{
                //    Name = r.name,
                //    Url = r.url,
                //}).ToList();

                ////Converting OBJECT to JSON String   (return as array)
                //var jsonstring = JsonSerializer.Serialize(pokemonViewModels);

                //Return JSON string

                return json;
            }

        }
        public string getPokemon()
        {
            string pokemonName;

            string url = $"https://pokeapi.co/api/v2/pokemon/";  //request to API how much data we want to retrieve


            using (WebClient client = new WebClient())
            {
                string json = client.DownloadString(url);   //get the data from website
                //Return JSON string.  
                return json;
            }

        }
        public string getPokemonType()
        {
            string url = $"https://pokeapi.co/api/v2/type/";  //request to API how much data we want to retrieve


            using (WebClient client = new WebClient())
            {
                string json = client.DownloadString(url);   //get the data from website
                //Return JSON string.  
                return json;
            }
        }
        public string getPokemonAbility()
        {
            string url = $"https://pokeapi.co/api/v2/ability/";  //request to API how much data we want to retrieve


            using (WebClient client = new WebClient())
            {
                string json = client.DownloadString(url);   //get the data from website
                //Return JSON string.  
                return json;
            }
        }
        [HttpPost]
        public String GetWeatherDetail(string City)
        {


            //assign API key get from website
            string appId = "5626c0c749392da5f423d117f585a5a5";

            //API path with CITY parameter and other parameters.  
            string url = $"https://api.openweathermap.org/data/2.5/weather?q={City}&appid={appId}";
            //string url = string.Format(https://api.openweathermap.org/data/2.5/weather?q=City&appid=appId);

            //using (WebClient client = new WebClient())
            //{
            //    string json = client.DownloadString(url);   //get the data from website

            //    RootObject rootObject = JsonSerializer.Deserialize<RootObject>(json);

            //   List<PokemonViewModel> pokemonViewModels = rootObject.results.Select(r=>new PokemonViewModel
            //   {
            //       Name = r.Name,
            //       Url = r.Url,
            //   }).ToList();

            //    //Converting OBJECT to JSON String   
            //    var jsonstring =  JsonSerializer.Serialize(pokemonViewModels);

            //Return JSON string.  
            return url;




        }

        #endregion Get Json Data

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


    }
}