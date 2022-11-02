//using Microsoft.DotNet.Scaffolding.Shared.CodeModifier.CodeChange;
using RestSharp;

namespace theFoodCampus.Models.Adapter
{
    public class HebCalAdapter
    {
        public string Check()
        {
            string Url = $"http://localhost:5202/api/HebCal?currentDate={DateTime.Now}";

            var client = new RestClient(Url);

            var request = new RestRequest(new Uri(Url), Method.Get);

            RestResponse response = client.Execute(request);

            return response.Content;
            // here we have to connect to the gateway and
            //get information about the HebrewCalander
            //return $"Here we write the Hebrew calander we got from gateway";
        }
    }
}
