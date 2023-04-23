using GoT_Wiki.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace GoT_Wiki.Services
{
    /// <summary>
    /// Class <c>Service</c> is used to retrieve 
    /// data  from An API of Ice And Fire.
    /// </summary>
    /// <typeparam name="TClass">The of model retrieved.</typeparam>
    public class Service<TClass>
    {
        private static Service<TClass> _instance;

        /// <summary>
        /// There is only one instance of service for each model type.
        /// One can access the instance with this property.
        /// </summary>
        /// <remarks>
        /// Uses lazy initialization.
        /// </remarks>
        public static Service<TClass> Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new Service<TClass>();
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

        /// <summary>
        /// Used for setting the page size.
        /// By default it has a value of 0.
        /// </summary>
        public int PageSize
        {
            private get { return _pageSize; }
            set { _pageSize = value; }
        }

        private readonly Action<TClass> _process = null;

        private Service()
        {
            _apiEndpoint = _typeToEndpointDictionary[typeof(TClass)];
            _process = _typeToActionDictionary[typeof(TClass)] as Action<TClass>;
        }

        /// <summary>
        /// Used for fetching a list of entities from the API.
        /// </summary>
        /// <param name="pageNumber">
        /// The page number we want to get.
        /// </param>
        /// <returns>
        /// A list of the model items on that page.
        /// </returns>
        public async Task<IList<TClass>> GetAsync(int pageNumber)
        {
            var uri = new Uri(_serverUrl, $"{_apiEndpoint}?page={pageNumber}&pageSize={_pageSize}");
            return await GetAllMatching(uri);
        }

        /// <summary>
        /// Used for fetching a single entity from the API.
        /// </summary>
        /// <param name="uri">
        /// The identifier of the entity.
        /// </param>
        /// <returns>
        /// The entity, if it exists. 
        /// A model instance with none of its properties initialized.
        /// </returns>
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

        /// <summary>
        /// Used for fetching a single entity from the API.
        /// </summary>
        /// <param name="url">
        /// The unified resource location of the entity.
        /// </param>
        /// <returns>
        /// The entity, if it exists. 
        /// A model instance with none of its properties initialized.
        /// </returns>
        public async Task<TClass> GetAsync(string url)
        {
            var uri = new Uri(url);
            return await GetAsync(uri);
        }

        /// <summary>
        /// Used for fetching a list of entities, 
        /// whose name matches the parameter.
        /// </summary>
        /// <param name="name">
        /// The name of the entity.
        /// </param>
        /// <returns>
        /// Returns a list of entities,
        /// whose name is <para>name</para>.
        /// </returns>
        /// <remarks>
        /// Fetching is not case sensitive.
        /// </remarks>
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
