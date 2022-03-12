// -----------------------------------------------------------------------
//  <copyright file="SelfService.cs" />
// -----------------------------------------------------------------------
using Core.Interfaces;
using Core.Models;
using Core.Utilities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core
{
    // Implemented anti-pattern "Hardcode";
    // But, it's for joke aim, not as real
    public class SelfService : IAbstractContentService
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
        protected override Task HandleResponse(string content)
        {
            return Task.CompletedTask;
        }

        /// <summary>
        /// Abstract function to get content
        /// Called on platform-part
        /// </summary>
        /// <returns>List of models to use</returns>
        protected override List<AdapterModel> GetContent()
        {
            return new List<AdapterModel>()
            {
                new AdapterModel()
                {
                    Title = "It is me!",
                    Details = string.Empty,
                    ImageUrl = string.Empty,
                    ModelType = (int)EnumUtils.ModelType.Self
                }
            };
        }

        /// <summary>
        /// Clean duplicates from received information
        /// </summary>
        protected override void CleanDublicates()
        {
        }
        #endregion

        #region Overridables
        /// <summary>
        /// Prepare content, receive byte array for image
        /// </summary>
        /// <returns></returns>
        protected override Task<List<AdapterModel>> GetPreparedContent()
        {
            return Task.FromResult(GetContent());
        }        

        /// <summary>
        /// Public API to prepare local data
        /// </summary>
        /// <returns></returns>
        protected override Task<bool> InitializeModels()
        {
            return Task.FromResult(true);
        }
        #endregion
    }
}
