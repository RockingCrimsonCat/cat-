using HolidayAPI.Models;
using Newtonsoft.Json;
namespace HolidayAPI
{
   
        public class User
        {
            private HttpClient _httpClient;
            private static string _adress;


            public User()
            {
                _adress = Constants.adress;
                _httpClient = new HttpClient();
                _httpClient.BaseAddress = new Uri(_adress);
            }
            public async Task<List<Holiday>> GetPublicHolidaysAsync(string countryCode)
            {

                var responce = await _httpClient.GetAsync($"/api/v3/NextPublicHolidays/{countryCode}");
                responce.EnsureSuccessStatusCode();
                var content = responce.Content.ReadAsStringAsync().Result;
                
                List<Holiday> holidays = JsonConvert.DeserializeObject<List<Holiday>>(content);

            return holidays;
            }
        public async Task<List<Countries>> GetCountriesAsync()
        {

            var responce = await _httpClient.GetAsync($"/api/v3/AvailableCountries");
            responce.EnsureSuccessStatusCode();
            var content = responce.Content.ReadAsStringAsync().Result;

            List<Countries> countries = JsonConvert.DeserializeObject<List<Countries>>(content);

            return countries;
        }

    }
    
}
