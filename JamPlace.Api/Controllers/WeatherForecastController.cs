using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JamPlace.DataLayer.Entities;
using JamPlace.DomainLayer.Interfaces.Repositories;
using JamPlace.DomainLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace JamPlace.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    
    public class WeatherForecastController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IJamUserRepository _jamUserRepo;
        private readonly IBasicJamEventRepository repo;
        private readonly ICommentRepository _commentRepo;
        public WeatherForecastController(IJamUserRepository jamUserRepo,IBasicJamEventRepository repo, ILogger<WeatherForecastController> logger, ICommentRepository commentRepo)
        {
            _jamUserRepo = jamUserRepo;
            this.repo = repo;
            _logger = logger;
            _commentRepo = commentRepo;
        }
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };


        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            //var user = new JamUser()
            //{
            //    UserIdentityId = "abc-543",
            //    UserName = "Zenek Zombi",
            //    PersonalEquipment = new List<Equipment>()
            //    {
            //        new Equipment()
            //        {
            //            Name="Gitara",
            //            Description="8 strunowa gitara elektryczna"

            //        },
            //        new Equipment()
            //        {
            //            Name="Keyboard",
            //            Description="Disco polo casio"

            //        }
            //    }
            //};
            var user = _jamUserRepo.Get(3);
            var list = user.PersonalEquipment.Skip(1).ToList();
            //list.Add(new EquipmentDo() { Name = "trolololo", Description = "eeeeeeeeeeeeee" });
            //user.PersonalEquipment = list;
            _jamUserRepo.Update(user);
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
            
        }
    }
}
