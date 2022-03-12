// -----------------------------------------------------------------------
//  <copyright file="ServicesAvailability.cs" />
// -----------------------------------------------------------------------
using Core;
using System.Linq;
using Xunit;

namespace CoreTest
{
    public class ServicesAvailability
    {
        /// <summary>
        /// Test, is Self service working
        /// </summary>
        [Fact]
        public async void SelfServiceAvailability()
        {
            var listResult = await new SelfService().GetContentList();
            bool testResult = listResult?.Any() ?? false;
            Assert.True(testResult, "Self Service does not return any content");
        }

        /// <summary>
        /// Test, is Anime service working
        /// </summary>
        [Fact]
        public async void AnimeServiceAvailability()
        {
            var listResult = await new AnimeService().GetContentList();
            bool testResult = listResult?.Any() ?? false;
            Assert.True(testResult, "Anime Service does not return any content");
        }

        /// <summary>
        /// Test, is Zoo service working
        /// </summary>
        [Fact]
        public async void ZooServiceAvailability()
        {
            var listResult = await new ZooAnimalsService(2).GetContentList();
            bool testResult = listResult?.Any() ?? false;
            Assert.True(testResult, "Zoo Animal Service does not return any content");
        }

        /// <summary>
        /// Test, is Museum service working
        /// </summary>
        [Fact]
        public async void MuseumServiceAvailability()
        {
            var listResult = await new MuseumService(2,2).GetContentList();
            bool testResult = listResult?.Any() ?? false;
            Assert.True(testResult, "Museum Service does not return any content");
        }
    }
}
