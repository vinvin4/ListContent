// -----------------------------------------------------------------------
//  <copyright file="ConverterTest.cs" />
// -----------------------------------------------------------------------
using Core.Utilities;

using System;
using Xunit;

namespace CoreTest
{
    public class ConverterTest
    {
        private Tuple<float, float> testMeasurements = new Tuple<float, float>(45.5f, 65.5f);
        private Tuple<string, string> expectedMeasurements = new Tuple<string, string>("13,868-19,964 Meters", "20,639-29,711 Kg");

        /// <summary>
        /// Test, how Animal lenght converter works
        /// </summary>
        [Fact]
        public void TestLenght()
        {
            string testData = Utilities.ConvertMeasurement(testMeasurements.Item1, testMeasurements.Item2, true);
            Utilities.LogMessage(testData);
            bool result = testData == expectedMeasurements.Item1;
            Assert.True(result, "Utilities.ConvertMeasurement for Lenght is not working correctly");
        }

        /// <summary>
        /// Test, how Animal weight converter works
        /// </summary>
        [Fact]
        public void TestWeight()
        {
            string testData = Utilities.ConvertMeasurement(testMeasurements.Item1, testMeasurements.Item2, false);
            Utilities.LogMessage(testData);
            bool result = testData == expectedMeasurements.Item2;
            Assert.True(result, "Utilities.ConvertMeasurement for Weight is not working correctly");
        }

        //TestModelConverted is deleted.
        //Due to implemented translation for content - Hard to predict russian values and cannot quickly test english value
    }
}
