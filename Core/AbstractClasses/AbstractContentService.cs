// -----------------------------------------------------------------------
//  <copyright file="IAbstractContentService.cs" />
// -----------------------------------------------------------------------
using Core.Interfaces;
using Core.Models;
using Core.Utilities;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Core.AbstractClasses
{
    public abstract class AbstractContentService : IContentService
    {
        #region Private Properties
        private readonly string requestUrl = string.Empty;
        private readonly int sessionId;
        #endregion

        #region Constructor
        /// <summary>
        /// Initialize new instance of <see cref="AbstractContentService"/>
        /// </summary>
        /// <param name="url">Request URL to call</param>
        public AbstractContentService(string url)
        {
            this.requestUrl = url;
            sessionId = new Random().Next(1, 9999);
        }
        #endregion

        #region Abstract Methods
        /// <summary>
        /// Handle response from Initialize model request
        /// </summary>
        /// <param name="content">Received content</param>
        protected abstract Task HandleResponse(string content);

        /// <summary>
        /// Abstract function to get content
        /// Called on platform-part
        /// </summary>
        /// <returns>List of models to use</returns>
        protected abstract List<AdapterModel> GetContent();

        /// <summary>
        /// Clean duplicates from received information
        /// </summary>
        protected virtual void CleanDublicates()
        {
        }
        #endregion

        /// <summary>
        /// Public API to prepare local data
        /// </summary>
        /// <returns></returns>
        protected virtual async Task<bool> InitializeModels()
        {
            bool flowResult = true;
            var httpResponse = await GetResponseMessage();
            if (httpResponse?.IsSuccessStatusCode ?? false)
            {
                var content = await httpResponse.Content.ReadAsStringAsync();
                Utilities.Utilities.LogMessage(content, Constants.RESPONSE);
                await HandleResponse(content);
            }
            else
            {
                flowResult = false;
                Utilities.Utilities.LogMessage($"Incorrect status code: {httpResponse.StatusCode}; InitializeModels",
                    Constants.EXCEPTION);
            }
            return flowResult;
        }

        /// <summary>
        /// Get Content API for all services
        /// </summary>
        /// <returns>Content list</returns>
        public async Task<List<AdapterModel>> GetContentList()
        {
            List<AdapterModel> returnValue = null;
            if (await InitializeModels())
            {
                returnValue = GetContent();
                await Task.WhenAll(returnValue.Select(item => item.InitializeImageArray()));
            }
            else
            {
                returnValue = new List<AdapterModel>();
            }                
            return returnValue;
        }

        /// <summary>
        /// Session ID property. Added to clear initialize instance of service
        /// </summary>
        public int SessionId => sessionId;

        #region Private Methods
        /// <summary>
        /// Execute Http-request
        /// </summary>
        /// <returns>HttpResponseMessage</returns>
        private async Task<HttpResponseMessage> GetResponseMessage()
        {
            HttpResponseMessage message = null;
            using (HttpClient client = new HttpClient())
            {
                ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;
                client.Timeout = Constants.ClientTimeout;
                try
                {
                    message = await client.GetAsync(requestUrl).ConfigureAwait(false);
                }
                catch (HttpRequestException ex)
                {
                    Utilities.Utilities.LogMessage($"HttpRequestException: {ex.Message}; IAbstractContentService.GetResponseMessage",
                        Constants.EXCEPTION);
                }
                catch (Exception e)
                {
                    Utilities.Utilities.LogMessage($"Exception: {e.Message}; IAbstractContentService.GetResponseMessage",
                        Constants.EXCEPTION);
                }
            }
            return message;
        }
        #endregion
    }
}
