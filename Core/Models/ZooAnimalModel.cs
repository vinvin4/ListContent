// -----------------------------------------------------------------------
//  <copyright file="ZooAnimalModel.cs" />
// -----------------------------------------------------------------------
using Core.Interfaces;
using Core.Utilities;
using Newtonsoft.Json;
using System;

namespace Core.Models
{
    public class ZooAnimalModel : IClonableModel
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("animal_type")]
        public string AnimalType { get; set; }
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
            string animalLength = Utilities.Utilities.ConvertMeasurement(MinLenght, MaxLenght, true);
            string animalWeight = Utilities.Utilities.ConvertMeasurement(MinWeight, MaxWeight, false);
            bool isDiurnal = ActiveTime.Trim().ToLower() == Constants.AnimalTime.ToLower();

            var details = new ZooAnimalDetails()
            {
                Name = Name,
                Lenght = animalLength,
                Weight = animalWeight,
                Lifespan = Lifespan,
                IsDiurnal = isDiurnal
            };

            return new AdapterModel()
            {
                Title = Name,
                ImageUrl = ImageUrl,
                Details = details.ToString(),
                ModelType = (int)EnumUtils.ModelType.ZooAnimal
            };
        }
    }

    public class ZooAnimalDetails : IClonableModel
    {
        /// <summary>
        /// Name of animal
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Animal lenght duration
        /// </summary>
        public string Lenght { get; set; }
        /// <summary>
        /// Animal weight duration
        /// </summary>
        public string Weight { get; set; }
        /// <summary>
        /// Animal lifespan
        /// </summary>
        public int Lifespan { get; set; }
        /// <summary>
        /// Animal activity time
        /// </summary>
        public bool IsDiurnal { get; set; }

        public override AdapterModel ConvertToWorkingModel()
        {
            return null;
        }
    }
}
