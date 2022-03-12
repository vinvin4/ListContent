// -----------------------------------------------------------------------
//  <copyright file="Utilities.cs" />
// -----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities
{
    public static class Utilities
    {
        /// <summary>
        /// Local wrapped to convert float values to string fact
        /// Usage: ZooAnimalModel
        /// </summary>
        /// <param name="initialMinValue">Min value in duration</param>
        /// <param name="initialMaxValue">Max value in duration</param>
        /// <param name="isLenght">Is Lenght duration processed</param>
        /// <returns></returns>
        public static string ConvertMeasurement(float initialMinValue, float initialMaxValue, bool isLenght)
        {
            string dataFormat = "{0}-{1} ";
            string floatFormat = "{0:f3}";

            float measureDifference = isLenght ? Constants.MInFeet : Constants.KGInPound;

            initialMinValue *= measureDifference;
            initialMaxValue *= measureDifference;
            string animalParameter = string.Format(dataFormat,
                string.Format(floatFormat, initialMinValue),
                string.Format(floatFormat, initialMaxValue));

            animalParameter += isLenght ? "Meters" : "Kg";

            return animalParameter;
        }
    }
}
