// -----------------------------------------------------------------------
//  <copyright file="Utilities.cs" />
// -----------------------------------------------------------------------
using System;

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
            float measureDifference = isLenght ? Constants.MInFeet : Constants.KGInPound;

            initialMinValue *= measureDifference;
            initialMaxValue *= measureDifference;
            string animalParameter = string.Format(Constants.DataFormat,
                string.Format(Constants.FloatFormat, initialMinValue),
                string.Format(Constants.FloatFormat, initialMaxValue));

            animalParameter += isLenght ? "Meters" : "Kg";

            return animalParameter;
        }

        /// <summary>
        /// Wrapper to log message;
        /// Not a good point to use Console Logs in Release mode
        /// </summary>
        /// <param name="message">Message to be logged</param>
        /// <param name="category">Message category</param>
        public static void LogMessage(string message, string category = Constants.INFO)
        {
#if DEBUG
            Console.WriteLine("{0}, {1}", message, category);
#endif
        }
    }

    public static class Constants
    {
        public static float MInFeet = 0.3048F;
        public static float KGInPound = 0.4536F;
        public static string AnimalTime = "Diurnal";

        public static int BitmapSize = 100;

        public static TimeSpan ClientTimeout = new TimeSpan(0, 0, 15);

        public static string DataFormat = "{0}-{1} ";
        public static string FloatFormat = "{0:f3}";

        public const string INFO = "INFO";
        public const string EXCEPTION = "EXCEPTION";
        public const string RESPONSE = "RESPONSE";
    }
}
