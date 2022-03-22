// -----------------------------------------------------------------------
//  <copyright file="SelfService.cs" />
// -----------------------------------------------------------------------
using Core.AbstractClasses;
using Core.Models;
using Core.Utilities;

using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Services
{
    public class SelfService : AbstractContentService
    {
        #region Constructors
        /// <summary>
        /// Initialize new instance of <see cref="SelfService"/>
        /// </summary>
        public SelfService() : base(string.Empty)
        {
        }
        #endregion

        #region Interface implamantation
        /// <summary>
        /// Handle responce from Initialize Model request
        /// </summary>
        /// <param name="content"></param>
        protected override Task HandleResponse(string content) => Task.CompletedTask;

        /// <summary>
        /// Abstract function to get content
        /// Called on platform-part
        /// </summary>
        /// <returns>List of models to use</returns>
        protected override List<AdapterModel> GetContent()
        {
            var factDetails = new Dictionary<string, string>()
            {
                {DetailsResources.NameTitle, "Ivan" },
                {DetailsResources.SelfAgeTitle, "23" },
                {DetailsResources.SelfWantWorkTitle, bool.TrueString },
                {DetailsResources.SelfFavoriteAuthorTitle, "Данияр Сугралинов" }
            };
            return new List<AdapterModel>()
            {
                new AdapterModel()
                {
                    Title = DetailsResources.SelfDetailsTitle,
                    Details = JsonConvert.SerializeObject(factDetails),
                    ImageUrl = string.Empty,
                    ModelType = (int)EnumUtils.ModelType.Self
                }
            };
        }
        #endregion

        #region Overridables
        /// <summary>
        /// Public API to prepare local data
        /// </summary>
        /// <returns></returns>
        protected override Task<bool> InitializeModels() => Task.FromResult(true);
        #endregion
    }
}
