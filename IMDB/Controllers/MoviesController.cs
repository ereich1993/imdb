using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using IMDB.Data;
using IMDB.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;

namespace IMDB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {

        private readonly IMDBDbContext _context;
        private static readonly HttpClient client = new HttpClient();

        public MoviesController(IMDBDbContext context)
        {
            _context = context;

        }


        [HttpGet("Movies/{id}")]
        public async Task<IList<JObject>> GetMovies(string title)
        {
            List<string> movies = new List<string>() {
                "kill bill",
                "fargo",
                "alien"
            };
            IList<JObject> msg = await ProcessMultipleRepositories(movies);
            return msg;
        }

        // GET: api/Users
        [HttpGet("{title}")]
        public async Task<string> GetUsers(string title)
        {
            while (HttpContext.Session.GetString("Emil")!=null)
            {
                string msg = await ProcessRepositories(title);
                return msg;
            }
            return "You need to register";
            
        }
        private static async Task<IList<JObject>> ProcessMultipleRepositories(List<string> movies)
        {
            List<JObject> jsonmovies = new List<JObject>();
            foreach (var title in movies)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("http://www.omdbapi.com/?t=");
                sb.Append(title);
                sb.Append("&apikey=4d0f0ecd");
                string result = sb.ToString();
                var stringTask = client.GetStringAsync(result);
                var msg = await stringTask;
                JObject json = JObject.Parse(msg);
                jsonmovies.Add(json);
            }
             
            return jsonmovies;
        }


        private static async Task<string> ProcessRepositories(string title)
        {
            //client.DefaultRequestHeaders.Accept.Clear();
            //client.DefaultRequestHeaders.Accept.Add(
            //    new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));
            //client.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository Reporter");
            StringBuilder sb = new StringBuilder();
            sb.Append("http://www.omdbapi.com/?t=");
            sb.Append(title);
            sb.Append("&apikey=4d0f0ecd");
            string cim = "http://www.omdbapi.com/?t=starwars&apikey=4d0f0ecd";
            string result = sb.ToString();

            var stringTask = client.GetStringAsync(result);

            
            var msg = await stringTask;
            JObject json = JObject.Parse(msg);
            return msg;
        }
    }
}