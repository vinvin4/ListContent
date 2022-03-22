// -----------------------------------------------------------------------
//  <copyright file="ZooAnimalsService.cs" />
// -----------------------------------------------------------------------
using Core.AbstractClasses;
using Core.Models;

using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Services
{
    public class ZooAnimalsService : AbstractContentService
    {
        #region Properties
        private const string url = "https://zoo-animal-api.herokuapp.com/animals/rand/";
        protected List<ZooAnimalModel> models = null;
        #endregion

        #region Constructors
        /// <summary>
        /// Initialize new instance of <see cref="ZooAnimalsService"/>
        /// </summary>
        public ZooAnimalsService() : base(url + "10")
        {
            models = new List<ZooAnimalModel>();
        }

        /// <summary>
        /// Initialize new instance of <see cref="ZooAnimalsService"/> for MOCK aims
        /// </summary>
        /// <param name="limit">How much items should be received. For MOCK - value should be so small</param>
        public ZooAnimalsService(int limit) : base(url + limit)
        {
            models = new List<ZooAnimalModel>();
        }
        #endregion

        #region Interface Implementation
        /// <summary>
        /// Get content to showon platform part
        /// </summary>
        /// <returns>List of adapted models</returns>
        protected override List<AdapterModel> GetContent()
        {
            CleanDublicates();
            return models
                .Select(item => item.ConvertToWorkingModel())
                .ToList();
        }

        /// <summary>
        /// Handle responce from Initialize Model request
        /// </summary>
        /// <param name="content"></param>
        protected override Task HandleResponse(string content)
        {
            var details = JsonConvert.DeserializeObject<List<ZooAnimalModel>>(content);
            models.AddRange(details);
            return Task.CompletedTask;
        }

        /// <summary>
        /// Clean duplicates from received information
        /// </summary>
        protected override void CleanDublicates()
        {
            models = models
                .GroupBy(item => new { item.Id, item.Name })
                .Select(group => group.ToArray().First())
                .ToList();
        }
        #endregion
    }
}
