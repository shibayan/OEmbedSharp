using System.Threading.Tasks;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace OEmbedSharp.Tests
{
    [TestClass]
    public class OEmbedTest
    {
        [TestMethod]
        public void CanEmbedForYouTube()
        {
            var oembed = new OEmbed();

            var canEmbed = oembed.CanEmbed("http://www.youtube.com/watch?v=GYZRWdne5Uo");

            Assert.IsTrue(canEmbed);
        }

        [TestMethod]
        public async Task EmbedAsyncForYouTube()
        {
            var oembed = new OEmbed();

            var actual = await oembed.EmbedAsync("http://www.youtube.com/watch?v=GYZRWdne5Uo");

            Assert.IsNotNull(actual);
        }

        [TestMethod]
        public void CanEmbedForFlickr()
        {
            var oembed = new OEmbed();

            var canEmbed = oembed.CanEmbed("http://www.flickr.com/photos/gsfc/6760135001/");

            Assert.IsTrue(canEmbed);
        }

        [TestMethod]
        public async Task EmbedAsyncForFlickr()
        {
            var oembed = new OEmbed();

            var actual = await oembed.EmbedAsync("http://www.flickr.com/photos/gsfc/6760135001/");

            Assert.IsNotNull(actual);
        }

        [TestMethod]
        public void CanEmbedForSlideShare()
        {
            var oembed = new OEmbed();

            var canEmbed = oembed.CanEmbed("http://www.slideshare.net/shibayan/knockout-11523371");

            Assert.IsTrue(canEmbed);
        }

        [TestMethod]
        public async Task EmbedAsyncForSlideShare()
        {
            var oembed = new OEmbed();

            var actual = await oembed.EmbedAsync("http://www.slideshare.net/shibayan/knockout-11523371");

            Assert.IsNotNull(actual);
        }

        [TestMethod]
        public void CanEmbedForHatenaBlog()
        {
            var oembed = new OEmbed();

            var canEmbed = oembed.CanEmbed("http://staff.hatenablog.com/entry/2014/09/03/153938");

            Assert.IsTrue(canEmbed);
        }

        [TestMethod]
        public async Task EmbedAsyncForHatenaBlog()
        {
            var oembed = new OEmbed();

            var actual = await oembed.EmbedAsync("http://staff.hatenablog.com/entry/2014/09/03/153938");

            Assert.IsNotNull(actual);
        }
    }
}
