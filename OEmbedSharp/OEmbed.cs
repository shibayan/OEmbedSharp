using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

using Newtonsoft.Json;

namespace OEmbedSharp
{
    public class OEmbed
    {
        public OEmbed()
        {
            // YouTube
            _providers.Add(new OEmbedProvider
            {
                Schemes = new[] { "http://www.youtube.com/watch?v=*", "http://youtu.be/*" },
                Endpoint = "http://www.youtube.com/oembed"
            });

            // Flickr
            _providers.Add(new OEmbedProvider
            {
                Schemes = new[] { "http://*.flickr.com/photos/*", "http://flic.kr/p/*" },
                Endpoint = "http://www.flickr.com/services/oembed"
            });

            // SlideShare
            _providers.Add(new OEmbedProvider
            {
                Schemes = new[] { "http://www.slideshare.net/*/*" },
                Endpoint = "http://www.slideshare.net/api/oembed/2"
            });
        }

        private readonly List<OEmbedProvider> _providers = new List<OEmbedProvider>();

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

            var endpoint = provider.Endpoint + "?" + url;

            var content = await new HttpClient().GetStringAsync(endpoint);

            var response = JsonConvert.DeserializeObject<OEmbedResponse>(content);

            return response;
        } 
    }
}
