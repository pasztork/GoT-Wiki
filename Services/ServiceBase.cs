using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace GoT_Wiki.Services
{
    public class ServiceBase<TClass>
    {
        private readonly Uri _serverUrl = new Uri("https://anapioficeandfire.com");
        private readonly string _apiEndpoint = string.Empty;
        private int _pageSize = 0;
        public int PageSize
        {
            get { return _pageSize; }
            set { _pageSize = value; }
        }

        public ServiceBase(string apiEndpoint)
        {
            _apiEndpoint = apiEndpoint;
        }

        public async Task<IList<TClass>> GetAsync(int pageNumber)
        {
            var uri = new Uri(_serverUrl, $"{_apiEndpoint}?page={pageNumber}&pageSize={_pageSize}");
            IList<TClass> result = null;
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(uri);
                var json = await response.Content.ReadAsStringAsync();
                result = JsonConvert.DeserializeObject<IList<TClass>>(json);
                foreach (var item in result)
                {
                    Process(item);
                }
            }
            return result;
        }

        public async Task<TClass> GetAsync(Uri uri)
        {
            TClass result = default;
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(uri);
                var json = await response.Content.ReadAsStringAsync();
                result = JsonConvert.DeserializeObject<TClass>(json);
                Process(result);
            }
            return result;
        }

        public async Task<TClass> GetAsync(string url)
        {
            var uri = new Uri(url);
            return await GetAsync(uri);
        }

        protected virtual void Process(TClass item) { }
    }
}
