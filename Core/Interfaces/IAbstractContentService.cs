// -----------------------------------------------------------------------
//  <copyright file="IAbstractContentService.cs" />
// -----------------------------------------------------------------------
using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public abstract class IAbstractContentService : IContentService
    {
        #region Private Properties
        private string requestUrl = string.Empty;
        #endregion

        #region Constructor
        /// <summary>
        /// Initialize new instance of <see cref="IAbstractContentService"/>
        /// </summary>
        /// <param name="url">Request URL to call</param>
        public IAbstractContentService(string url)
        {
            this.requestUrl = url;
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
        protected abstract void CleanDublicates();
        #endregion

        /// <summary>
        /// Prepare content, receive byte array for image
        /// </summary>
        /// <returns></returns>
        protected virtual async Task<List<AdapterModel>> GetPreparedContent()
        {
            List<AdapterModel> models = GetContent();
            await Task.WhenAll(models.Select(item => item.InitializeImageArray()));
            return models;
        }

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
                string content = await httpResponse.Content.ReadAsStringAsync();
                await HandleResponse(content);
            }
            else
            {
                flowResult = false;
                Console.WriteLine($"Incorrect status code: {httpResponse.StatusCode}; InitializeModels");
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
                returnValue = await GetPreparedContent();
            }
            else
            {
                new List<AdapterModel>();
            }                
            return returnValue;
        }

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
                client.Timeout = new TimeSpan(0, 0, 15);
                try
                {
                    message = await client.GetAsync(requestUrl).ConfigureAwait(false);
                }
                catch (HttpRequestException ex)
                {
                    Console.WriteLine($"HttpRequestException: {ex.Message}; IAbstractContentService.GetResponseMessage");
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Exception: {e.Message}; IAbstractContentService.GetResponseMessage");
                }
            }
            return message;
        }
        #endregion
    }
}
