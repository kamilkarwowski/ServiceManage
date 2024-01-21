namespace ServiceManagement.Services
{
    using System;
    using System.Net.Http;
    using System.Threading.Tasks;

    public class GoogleGeocodingService
    {
        private readonly string apiKey;
        private readonly HttpClient httpClient;

        public GoogleGeocodingService(string apiKey)
        {
            this.apiKey = apiKey;
            this.httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://maps.googleapis.com/maps/api/geocode/"),
            };
        }

        public async Task<string> GeocodeAddressAsync(string address)
        {
            // Build the request URL
            string requestUri = $"json?address={Uri.EscapeDataString(address)}&key={apiKey}";

            // Send the request to the Geocoding API
            HttpResponseMessage response = await httpClient.GetAsync(requestUri);

            if (response.IsSuccessStatusCode)
            {
                // Read the response content
                string content = await response.Content.ReadAsStringAsync();
                return content;
            }

            return null; // Handle errors as needed
        }
    }

}
