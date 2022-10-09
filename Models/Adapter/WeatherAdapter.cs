//using Microsoft.DotNet.Scaffolding.Shared.CodeModifier.CodeChange;
using RestSharp;

namespace theFoodCampus.Models.Adapter
{
    public class WeatherAdapter
    {
        public string Check()
        {
            // var location = Utilities.Location.GetLocationProperty();
            string city = "Jerusalem"; // to be changed!!! max make your own api
            string Url = $"http://localhost:5202/api/Weather?city={city}";

            var client = new RestClient(Url);

            var request = new RestRequest(new Uri(Url), Method.Get);

            RestResponse response = client.Execute(request);

            return response.Content;
            // here we have to connect to the gateway and
            //get information about the HebrewCalander
            //return $"Here we write the weather we got from gateway";
        }
    }
}
