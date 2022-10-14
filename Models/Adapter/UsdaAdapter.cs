//using Microsoft.DotNet.Scaffolding.Shared.CodeModifier.CodeChange;
using RestSharp;

namespace theFoodCampus.Models.Adapter
{
    public class UsdaAdapter
    {
        public string Check(string ingredients/*, string keyword*/)
        {
            //if (keyword == " ")
            //    keyword = "-";
            string Url = $"http://localhost:5202/api/usda?ingredients={ingredients}";

            var client = new RestClient(Url);

            var request = new RestRequest(new Uri(Url), Method.Get);

            RestResponse response = client.Execute(request);

            // here we have to connect to the gateway and
            //get information about the weather
            return response.Content;
        }
    }
}
