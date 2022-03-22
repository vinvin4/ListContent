// -----------------------------------------------------------------------
//  <copyright file="MuseumService.cs" />
// -----------------------------------------------------------------------
using Core.AbstractClasses;
using Core.Models;

using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Services
{
    public class MuseumService : AbstractContentService
    {
        #region Private properties
        private const string url = "https://api.artic.edu/api/v1/artworks?fields=id,title,date_display,image_id,place_of_origin&limit=10&page=";
        private const string mockUrl = "https://api.artic.edu/api/v1/artworks?fields=id,title,date_display,image_id,place_of_origin&page=";
        private const string limitSuffix = "&limit=";
        private List<MuseumItem> models = null;
        #endregion

        #region Constructors
        /// <summary>
        /// Initialize new instance of <see cref="MuseumService"/>
        /// </summary>
        public MuseumService(int page) : base(url + page)
        {
            models = new List<MuseumItem>();
        }

        /// <summary>
        /// Initialize new instance of <see cref="MuseumService"/> for MOCK aims
        /// </summary>
        public MuseumService(int page, int limit) : base(mockUrl + page + limitSuffix + limit)
        {
            models = new List<MuseumItem>();
        }
        #endregion

        #region Interface implementation
        /// <summary>
        /// Abstract function to get content
        /// Called on platform-part
        /// </summary>
        /// <returns>List of models to use</returns>
        protected override List<AdapterModel> GetContent()
        {
            CleanDublicates();
            return models
                .Select(model => model.ConvertToWorkingModel())
                .ToList();
        }

        /// <summary>
        /// Handle responce from Initialize Model request
        /// </summary>
        /// <param name="content"></param>
        protected override Task HandleResponse(string content)
        {
            var details = JsonConvert.DeserializeObject<MuseumModel>(content).Data;
            models.AddRange(details);
            return Task.CompletedTask;
        }

        /// <summary>
        /// Clean duplicates from received information
        /// </summary>
        protected override void CleanDublicates()
        {
            models = models
                .GroupBy(item => item.Title)
                .Select(group => group.ToArray().First())
                .ToList();
        }
        #endregion
    }
}
