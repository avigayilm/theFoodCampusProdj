using RestSharp;

namespace theFoodCampus.Models.Adapter
{
    public class BigMLAdapter
    {
        public string Check(BigMLData data)
        {

            string Url = $"http://localhost:5202/api/ML?Weather={data.Weather}&Holiday={data.Holiday}&LastCategory={data.LastCategory}";

            var client = new RestClient(Url);

            var request = new RestRequest(new Uri(Url), Method.Get);

            RestResponse response = client.Execute(request);

            return response.Content;
            // here we have to connect to the gateway and
            //get information for the imagga
            //return $"Here we write the weather we got from gateway";
        }

    }
}
