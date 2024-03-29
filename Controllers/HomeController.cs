﻿using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.AspNetCore.Mvc;
using ProjectAPI.Models;
using System.Diagnostics;
using System.Net;
using System;
using System.Text.Json;
using System.Reflection;


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
        public IActionResult PokemonUsingAjax()
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

            ViewBag.CurrentPokemonCount = pokemonViewModels.Count;
            ViewBag.TotalPokemon = countNo;
            ViewBag.ListName = pokemonViewModels;
            ViewBag.TotalTypePokemon = PokemonType.count;
            ViewBag.TotalAbilityPokemon = PokemonAbility.count;

            return View();
        }
        public IActionResult PokemonListing()
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

            //deserializer - convert json string to c#
            PokemonDetailViewModel model = JsonSerializer.Deserialize<PokemonDetailViewModel>(pokemonJson);
            
            PokemonSpeciesViewModel speciesModel = JsonSerializer.Deserialize<PokemonSpeciesViewModel>(pokemonSpecies);
            model.PokemonSpeciesViewModel = speciesModel;

            List<Moves> moves = model.moves.Select(r => new Moves
            {
               move = r.move,
            }).ToList();

            ViewBag.Moves = moves.Count;

            return View(model);
        }


        #region Get Json Data
        public string getPokemonSpeciesJson(string pokemonName)
        {

            string url1 = $"https://pokeapi.co/api/v2/pokemon-species/{pokemonName}";

            using (WebClient client = new WebClient())
            {
                //get the data from website
                string json = client.DownloadString(url1);   
                                                             
                return json;
            }

        }
        public string getPokemonDetailJson(string pokemonName)
        {


            string url1 = $"https://pokeapi.co/api/v2/pokemon/{pokemonName}";

            using (WebClient client = new WebClient())
            {
                string json = client.DownloadString(url1);   
                return json;
            }

        }
        public string getPokemon()
        {
            string pokemonName;

            string url = $"https://pokeapi.co/api/v2/pokemon/";  //offset,limit - request to API how much data we want to retrieve


            using (WebClient client = new WebClient())
            {
                string json = client.DownloadString(url);  
                return json;
            }

        }
        public string getPokemonType()
        {
            string url = $"https://pokeapi.co/api/v2/type/";  


            using (WebClient client = new WebClient())
            {
                string json = client.DownloadString(url);   
                return json;
            }
        }
        public string getPokemonAbility()
        {
            string url = $"https://pokeapi.co/api/v2/ability/";  


            using (WebClient client = new WebClient())
            {
                string json = client.DownloadString(url);   
                return json;
            }
        }

        #endregion Get Json Data

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


    }
}