// -----------------------------------------------------------------------
//  <copyright file="AnimeService.cs" />
// -----------------------------------------------------------------------
using Core.Interfaces;
using Core.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core
{
    public class AnimeService : IAbstractContentService
    {
        #region Private properties
        private const string url = "https://anime-facts-rest-api.herokuapp.com/api/v1";
        private const string mockUrl = "https://anime-facts-rest-api.herokuapp.com/api/v1";
        private List<AdapterModel> models = null;
        #endregion

        #region Constructors
        /// <summary>
        /// Initialize new isntance of <see cref="AnimeService"/>
        /// </summary>
        public AnimeService() : base(url)
        {
            models = new List<AdapterModel>();
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
            return models ?? new List<AdapterModel>();
        }

        /// <summary>
        /// Handle responce from Initialize Model request
        /// </summary>
        /// <param name="content"></param>
        protected override async Task HandleResponse(string content)
        {
            var details = JsonConvert.DeserializeObject<AnimeModel>(content);
            var animeServices = details.Items
                .Select(item => item.Name)
                .Select(name => new AnimeFactService(name))
                .ToList();
            foreach (var animeItem in animeServices)
            {
                models.AddRange(await animeItem.GetContentList());
            }
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

        /// <summary>
        /// Lical class to fetch anime facts
        /// </summary>
        private class AnimeFactService : IAbstractContentService
        {
            #region Private properties
            private string animeName = string.Empty;
            private AnimeFactModel fact = null;
            #endregion

            #region Constructors
            /// <summary>
            /// Initialize new instance of <see cref="AnimeFactService"/>
            /// </summary>
            /// <param name="animeName"></param>
            public AnimeFactService(string animeName) : base(AnimeService.url + $"/{animeName}")
            {
                this.animeName = animeName;
            }
            #endregion

            #region Interface Implementation
            /// <summary>
            /// Abstract function to get content
            /// Called to get complex solution
            /// </summary>
            /// <returns>List of models to use</returns>
            protected override List<AdapterModel> GetContent()
            {
                var outputResult = new List<AdapterModel>();
                if (fact != null)
                {
                    AdapterModel model = fact.ConvertToWorkingModel();
                    model.Title = animeName;
                    outputResult.Add(model);
                }                
                return outputResult; 
            }

            /// <summary>
            /// Clean duplicates from received information
            /// </summary>
            protected override void CleanDublicates()
            {
            }

            /// <summary>
            /// Handle responce from Initialize Model request
            /// </summary>
            /// <param name="content"></param>
            protected override Task HandleResponse(string content)
            {
                fact = JsonConvert.DeserializeObject<AnimeFactModel>(content);
                return Task.CompletedTask;
            }
            #endregion
        }
    }
}
