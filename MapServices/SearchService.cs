using HprAbstractions.Configuration;
using HprAbstractions.Mapping;
using Newtonsoft.Json;
using System;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace MapServices
{
    public class SearchService
    {
        public SearchService(MapConfigSection config, HttpClient httpClient)
        {
            this.Config = config ?? throw new ArgumentNullException(nameof(config));
            this.HttpClient = HttpClient ?? new HttpClient();
        }

        MapConfigSection Config;
        HttpClient HttpClient;
        CultureInfo NumericFormatProvider = new CultureInfo("en-us");

        public async Task<Address> SearchByPositionAsync(decimal lat, decimal lon)
        {
            var latS = lat.ToString(this.NumericFormatProvider);
            var lonS = lon.ToString(this.NumericFormatProvider);
            var url = $"https://atlas.microsoft.com/search/address/reverse/json?api-version=1.0&subscription-key={this.Config.Key}&query={latS},{lonS}";

            var response = await this.HttpClient.GetAsync(url);
            var json = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<AddressSearchResult>(json);

            var address = result.Addresses.FirstOrDefault()?.Address;
            if(null != address)
            {
                return address;
            }

            return null;
        }

        public Position ParsePosition(string input)
        {
            var arr = input.Split(',');
            if(arr.Length == 2)
            {
                decimal? lat = null, lon = null;

                if (decimal.TryParse(arr[0], NumberStyles.Any, this.NumericFormatProvider, out decimal l1))
                {
                    lat = l1;
                }

                if(decimal.TryParse(arr[1], NumberStyles.Any, this.NumericFormatProvider, out decimal l2))
                {
                    lon = l2;
                }
                
                if(null != lat && null != lon)
                {
                    return new Position { Latitude = lat.Value, Longitude = lon.Value };
                }
            }
            return null;
        }
    }
}
