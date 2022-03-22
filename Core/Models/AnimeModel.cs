// -----------------------------------------------------------------------
//  <copyright file="AnimeModel.cs" />
// -----------------------------------------------------------------------
using Core.AbstractClasses;
using Core.Utilities;

using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace Core.Models
{
    public class AnimeModel
    {
        [JsonProperty("success")]
        public bool IsSuccess { get; set; }
        [JsonProperty("data")]
        public List<AnimeItem> Items { get; set; }
    }

    public class AnimeItem
    {
        [JsonProperty("anime_name")]
        public string Name { get; set; }
    }

    public class AnimeFactModel : AbstractClonableModel
    {
        [JsonProperty("success")]
        public string IsSuccess { get; set; }
        [JsonProperty("img")]
        public string ImageUrl { get; set; }
        [JsonProperty("data")]
        public List<AnimeFactItemModel> Data { get; set; }

        public override AdapterModel ConvertToWorkingModel()
        {
            return new AdapterModel()
            {
                ModelType = (int)EnumUtils.ModelType.Anime,
                ImageUrl = ImageUrl,
                Details = JsonConvert.SerializeObject(Data.Select(fact => fact.Fact).ToList())
            };
        }
    }

    public class AnimeFactItemModel
    {
        [JsonProperty("fact")]
        public string Fact { get; set; }
    }
}
