// -----------------------------------------------------------------------
//  <copyright file="AdapterModel.cs" />
// -----------------------------------------------------------------------
using Core.AbstractClasses;
using Core.Utilities;

using System.Net.Http;
using System.Threading.Tasks;

namespace Core.Models
{
    public class AdapterModel : AbstractClonableModel
    {
        /// <summary>
        /// Title of item
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Image Url
        /// </summary>
        public string ImageUrl { get; set; }

        /// <summary>
        /// Downloaded image
        /// </summary>
        public byte[] Image { get; set; } = null;

        /// <summary>
        /// Json-value of details
        /// </summary>
        public string Details { get; set; }

        /// <summary>
        /// Model Type to distinguish UI
        /// </summary>
        public int ModelType { get; set; }

        public override AdapterModel ConvertToWorkingModel()
        {
            return this;
        }

        /// <summary>
        /// Local helper to receive image byte-array
        /// </summary>
        /// <returns></returns>
        public async Task InitializeImageArray()
        {
            if (!string.IsNullOrEmpty(ImageUrl))
            {
                HttpResponseMessage httpResponseMessage = null;
                using (HttpClient client = new HttpClient())
                {
                    client.Timeout = Constants.ClientTimeout;
                    httpResponseMessage = await client.GetAsync(ImageUrl);
                    if (httpResponseMessage?.IsSuccessStatusCode ?? false)
                    {
                        Image = await httpResponseMessage.Content.ReadAsByteArrayAsync();
                    }
                }
            }
            else if (ModelType != (int)EnumUtils.ModelType.Self)
            {
                Utilities.Utilities.LogMessage($"ImageUrl is null or empty. Verify, what is happen. Type: {ModelType}, Title: {Title}");
            }
        }
    }
}
