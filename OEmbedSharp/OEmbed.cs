using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Caching;
using System.Threading.Tasks;

using Newtonsoft.Json;

namespace OEmbedSharp
{
    public class OEmbed
    {
        static OEmbed()
        {
            // YouTube
            Providers.Add(new OEmbedProvider
            {
                Name = "YouTube",
                Schemes = new[] { @"https?://www\.youtube\.com/watch\?v=.+", @"https?://youtu\.be/.+" },
                Endpoint = "http://www.youtube.com/oembed"
            });

            // Flickr
            Providers.Add(new OEmbedProvider
            {
                Name = "Flickr",
                Schemes = new[] { @"https?://.+\.flickr\.com/photos/.+", @"https?://flic\.kr/p/.+" },
                Endpoint = "http://www.flickr.com/services/oembed"
            });

            // SlideShare
            Providers.Add(new OEmbedProvider
            {
                Name = "SlideShare",
                Schemes = new[] { @"https?://www\.slideshare\.net/.+/.+" },
                Endpoint = "http://www.slideshare.net/api/oembed/2"
            });
        }

        public OEmbed()
            : this(new OEmbedOptions())
        {
        }

        public OEmbed(OEmbedOptions options)
        {
            _options = options;
        }

        private readonly OEmbedOptions _options;

        private static readonly MemoryCache _cache = MemoryCache.Default;
        private static readonly List<OEmbedProvider> _providers = new List<OEmbedProvider>();

        public static ObjectCache Cache
        {
            get { return _cache; }
        }

        public static List<OEmbedProvider> Providers
        {
            get { return _providers; }
        }

        public bool EnableCache { get; set; }

        public bool CanEmbed(string url)
        {
            return _providers.Any(p => p.CanHandleUrl(url));
        }

        public OEmbedResponse Embed(string url)
        {
            var response = EmbedAsync(url).Result;

            return response;
        }

        public async Task<OEmbedResponse> EmbedAsync(string url)
        {
            if (url == null)
            {
                throw new ArgumentNullException("url");
            }

            var provider = _providers.FirstOrDefault(p => p.CanHandleUrl(url));

            if (provider == null)
            {
                throw new ArgumentException("url");
            }

            if (_options.EnableCache && _cache.Contains(url))
            {
                return (OEmbedResponse)_cache.Get(url);
            }

            var endpoint = provider.Endpoint + "?url=" + WebUtility.UrlEncode(url) + "&format=json";

            var content = await new HttpClient().GetStringAsync(endpoint);

            var response = JsonConvert.DeserializeObject<OEmbedResponse>(content);

            if (_options.EnableCache && response.CacheAge > 0)
            {
                _cache.Add(url, response, DateTimeOffset.Now.AddSeconds(response.CacheAge));
            }

            return response;
        } 
    }
}
