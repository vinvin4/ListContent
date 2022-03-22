// -----------------------------------------------------------------------
//  <copyright file="MuseumModel.cs" />
// -----------------------------------------------------------------------
using Core.AbstractClasses;
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

    public class MuseumItem : AbstractClonableModel
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
            var details = new Dictionary<string, string>()
            {
                {DetailsResources.DisplayDateTitle, DisplayDate },
                {DetailsResources.OriginPlaceTitle, OriginPlace }
            };
            return new AdapterModel()
            {
                Title = Title,
                ModelType = (int)EnumUtils.ModelType.Museum,
                ImageUrl = string.Format("https://www.artic.edu/iiif/2/{0}/full/843,/0/default.jpg", ImageId),
                Details = JsonConvert.SerializeObject(details)
            };
        }
    }
}
