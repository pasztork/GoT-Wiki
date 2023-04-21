using GoT_Wiki.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace GoT_Wiki.Services
{
    public class ServiceBase<TClass>
    {
        private static ServiceBase<TClass> _instance;
        public static ServiceBase<TClass> Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new ServiceBase<TClass>();
                }
                return _instance;
            }
        }

        private static readonly Dictionary<Type, string> _typeToEndpointDictionary = new Dictionary<Type, string>
        {
            { typeof(Book), "api/books" },
            { typeof(Character), "api/characters" },
            { typeof(House), "api/houses" },
        };

        private static readonly Dictionary<Type, Action<object>> _typeToActionDictionary = new Dictionary<Type, Action<object>>
        {
            { typeof(Book), (obj) => { } },
            { typeof(Character),
                (obj) =>
                {
                    var item = obj as Character;
                    item.Name = string.IsNullOrEmpty(item.Name) ? item.Aliases[0] : item.Name;
                }
            },
            { typeof(House), (obj) => { } },
        };

        private readonly Uri _serverUrl = new Uri("https://anapioficeandfire.com");
        private readonly string _apiEndpoint = string.Empty;
        private int _pageSize = 0;
        public int PageSize
        {
            get { return _pageSize; }
            set { _pageSize = value; }
        }

        private readonly Action<TClass> _process = null;

        private ServiceBase()
        {
            _apiEndpoint = _typeToEndpointDictionary[typeof(TClass)];
            _process = _typeToActionDictionary[typeof(TClass)] as Action<TClass>;
        }

        public async Task<IList<TClass>> GetAsync(int pageNumber)
        {
            var uri = new Uri(_serverUrl, $"{_apiEndpoint}?page={pageNumber}&pageSize={_pageSize}");
            return await GetAllMatching(uri);
        }

        public async Task<TClass> GetAsync(Uri uri)
        {
            TClass result = default;
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(uri);
                var json = await response.Content.ReadAsStringAsync();
                result = JsonConvert.DeserializeObject<TClass>(json);
                _process(result);
            }
            return result;
        }

        public async Task<TClass> GetAsync(string url)
        {
            var uri = new Uri(url);
            return await GetAsync(uri);
        }

        public async Task<IList<TClass>> GetByNameAsync(string name)
        {
            var uri = new Uri(_serverUrl, $"{_apiEndpoint}?name={name}");
            return await GetAllMatching(uri);
        }

        private async Task<IList<TClass>> GetAllMatching(Uri uri)
        {
            IList<TClass> result = null;
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(uri);
                var json = await response.Content.ReadAsStringAsync();
                result = JsonConvert.DeserializeObject<IList<TClass>>(json);
                foreach (var item in result)
                {
                    _process(item);
                }
            }
            return result;
        }
    }
}
