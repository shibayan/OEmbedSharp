using System;

using Newtonsoft.Json;

namespace OEmbedSharp
{
    public class OEmbedResponse
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("version")]
        public string Version { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("author_name")]
        public string AuthorName { get; set; }

        [JsonProperty("author_url")]
        public string AuthorUrl { get; set; }

        [JsonProperty("provider_name")]
        public string ProviderName { get; set; }

        [JsonProperty("provider_url")]
        public string ProviderUrl { get; set; }

        [JsonProperty("cache_age")]
        public int CacheAge { get; set; }

        [JsonProperty("thumbnail_url")]
        public string ThumbnailUrl { get; set; }

        [JsonProperty("thumbnail_width")]
        public int ThumbnailWidth { get; set; }

        [JsonProperty("thumbnail_height")]
        public int ThumbnailHeight { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("html")]
        public string Html { get; set; }

        [JsonProperty("width")]
        public int Width { get; set; }

        [JsonProperty("height")]
        public int Height { get; set; }

        private const string PhotoTemplate = "<img src=\"{0}\" alt=\"{1}\" />";
        private const string LinkTemplate = "<a href=\"{0}\">{1}</a>";

        public string Render()
        {
            switch (Type)
            {
                case OEmbedResponseType.Video:
                    return Html;

                case OEmbedResponseType.Photo:
                    return string.Format(PhotoTemplate, Url, Title);

                case OEmbedResponseType.Link:
                    return string.Format(LinkTemplate, AuthorUrl, Title ?? AuthorName);

                case OEmbedResponseType.Rich:
                    return Html;
            }

            throw new InvalidOperationException();
        }
    }
}
