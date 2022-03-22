// -----------------------------------------------------------------------
//  <copyright file="ZooAnimalModel.cs" />
// -----------------------------------------------------------------------
using Core.AbstractClasses;
using Core.Utilities;

using Newtonsoft.Json;
using System.Collections.Generic;

namespace Core.Models
{
    public class ZooAnimalModel : AbstractClonableModel
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("active_time")]
        public string ActiveTime { get; set; }
        [JsonProperty("length_min")]
        public float MinLenght { get; set; }
        [JsonProperty("length_max")]
        public float MaxLenght { get; set; }
        [JsonProperty("weight_min")]
        public float MinWeight { get; set; }
        [JsonProperty("weight_max")]
        public float MaxWeight { get; set; }
        [JsonProperty("lifespan")]
        public int Lifespan { get; set; }
        [JsonProperty("image_link")]
        public string ImageUrl { get; set; }
        [JsonProperty("id")]
        public int Id { get; set; }

        /// <summary>
        /// Convert existing model to form, usable in platform implementation
        /// </summary>
        /// <returns>Adapter model, that is available for working on platform side</returns>
        public override AdapterModel ConvertToWorkingModel()
        {
            var factsDetails = new Dictionary<string, string>()
            {
                {DetailsResources.NameTitle, Name },
                {DetailsResources.AnimalLenghtTitle, Utilities.Utilities.ConvertMeasurement(MinLenght, MaxLenght, true) },
                {DetailsResources.AnimalWeightTitle, Utilities.Utilities.ConvertMeasurement(MinWeight, MaxWeight, false) },
                {DetailsResources.AnimalLifespanTitle, Lifespan.ToString() }
            };
            factsDetails[DetailsResources.AnimalDurationTitle] = ActiveTime.Trim().ToLower() == Constants.AnimalTime.ToLower() ?
                DetailsResources.DiurnalDurationValue : DetailsResources.NocturnalDurationValue;

            return new AdapterModel()
            {
                Title = Name,
                ImageUrl = ImageUrl,
                Details = JsonConvert.SerializeObject(factsDetails),
                ModelType = (int)EnumUtils.ModelType.ZooAnimal
            };
        }
    }
}
