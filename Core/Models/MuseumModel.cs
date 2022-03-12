// -----------------------------------------------------------------------
//  <copyright file="MuseumModel.cs" />
// -----------------------------------------------------------------------
using Core.Interfaces;
using Core.Utilities;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Core.Models
{
    public class MuseumModel
    {
        [JsonProperty("data")]
        public List<MuseumItem> Data { get; set; }
    }

    public class MuseumItem : IClonableModel
    {
        [JsonProperty("title")]
        public string Title { get; set; }
        [JsonProperty("date_display")]
        public string DisplayDate { get; set; }
        [JsonProperty("place_of_origin")]
        public string OriginPlace { get; set; }
        [JsonProperty("image_id")]
        public string ImageId { get; set; }

        public override AdapterModel ConvertToWorkingModel()
        {
            var details = new MuseumItemDetails()
            {
                DisplayDate = DisplayDate,
                OriginPlace = OriginPlace
            };
            return new AdapterModel()
            {
                Title = Title,
                ModelType = (int)EnumUtils.ModelType.Museum,
                ImageUrl = string.Format("https://www.artic.edu/iiif/2/{0}/full/843,/0/default.jpg", ImageId),
                Details = details.ToString()
            };
        }
    }

    public class MuseumItemDetails : IClonableModel
    {
        /// <summary>
        /// Date, when art was presented
        /// </summary>
        public string DisplayDate { get; set; }

        /// <summary>
        /// Place, where art was presented
        /// </summary>
        public string OriginPlace { get; set; }

        public override AdapterModel ConvertToWorkingModel()
        {
            return null;
        }
    }
}
