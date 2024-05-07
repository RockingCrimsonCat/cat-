using Microsoft.AspNetCore.Mvc;
using static HolidayAPI.User;
using HolidayAPI.Models;
using Microsoft.Extensions.Logging;
using HolidayAPI.Data;

namespace HolidayAPI.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class HolidaysController : ControllerBase
    {

        private readonly ILogger<HolidaysController> _logger;


        public HolidaysController(ILogger<HolidaysController> logger)
        {
            _logger = logger;
        }

        [HttpGet("{countryCode}/publicholidays")]

        public List<Holiday> GetPublicHolidays(string countryCode)
        {
            Database database = new Database();
            User user = new User();
            List<Holiday> publicHolidays = user.GetPublicHolidaysAsync(countryCode).Result;
            database.InsertHolidaysAsync(user.GetPublicHolidaysAsync(countryCode).Result, countryCode);
            return publicHolidays;

        }
        [HttpGet("/countries")]

        public List<Countries> GetCountries()
        {
            User user = new User();
            List<Countries> countries = user.GetCountriesAsync().Result;
            return countries;
        }
        [HttpPut("{date}/{name}/{notes}/putuserevents")]
        public async Task<UserEvents> PutUserEventsAsync(string date, string name, string notes)
        { 
            Database database = new Database();
            UserEvents userEvents = new UserEvents();
            userEvents.Date = date;
            userEvents.Name = name;
            userEvents.Notes = notes;
          
           await database.InsertUserEventsAsync(userEvents, date, name, notes);
            return userEvents;
        }
        [HttpPost("/userpostevent")]
        public async Task<IActionResult> UpdateUsersReview([FromBody] UserEventDB userEventDB)
        {
            Database database = new Database();
            await database.UpdateUserEventAsync(userEventDB);
            return Ok();
        }
        [HttpDelete("/userevent/{name}")]
        public async Task<IActionResult> DeleteUserEvent(string name)
        {
            Database database = new Database();
            await database.DeleteUserEventAsync(name);
            return Ok();
        }

        [HttpGet("/userevents")]

        public List<UserEventDB> UserEvents()
        {
            Database database = new Database();

            return database.SelectUserEventDB().Result;

        }
    }
}
