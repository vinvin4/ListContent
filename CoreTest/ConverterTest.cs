// -----------------------------------------------------------------------
//  <copyright file="ConverterTest.cs" />
// -----------------------------------------------------------------------
using Core.Models;
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
            string testData = Core.Utilities.Utilities.ConvertMeasurement(testMeasurements.Item1, testMeasurements.Item2, true);

            bool result = testData == expectedMeasurements.Item1;
            Assert.True(result, "Utilities.ConvertMeasurement for Lenght is not working correctly");
        }

        /// <summary>
        /// Test, how Animal weight converter works
        /// </summary>
        [Fact]
        public void TestWeight()
        {
            string testData = Core.Utilities.Utilities.ConvertMeasurement(testMeasurements.Item1, testMeasurements.Item2, false);

            bool result = testData == expectedMeasurements.Item2;
            Assert.True(result, "Utilities.ConvertMeasurement for Weight is not working correctly");
        }

        /// <summary>
        /// Test, how Animal Model convert received data
        /// </summary>
        [Fact]
        public void TestModelConverter()
        {
            var testZooModel = new ZooAnimalModel()
            {
                Name = "Animal",
                ActiveTime = "dIURNAL",
                Lifespan = 10
            };
            testZooModel.MinLenght = testZooModel.MinWeight = testMeasurements.Item1;
            testZooModel.MaxLenght = testZooModel.MaxWeight = testMeasurements.Item2;
            var testModel = testZooModel.ConvertToWorkingModel();

            string expectedZooDetails = new ZooAnimalDetails()
            {
                Name = "Animal",
                IsDiurnal = true,
                Lenght = expectedMeasurements.Item1,
                Weight = expectedMeasurements.Item2,
                Lifespan = 10
            }.ToString();

            var expectedModel = new AdapterModel()
            {
                ModelType = 1,
                Details = expectedZooDetails
            };

            bool result = testModel.ModelType == expectedModel.ModelType
                && testModel.Details == expectedModel.Details;
            Assert.True(result, "ZooAnimalModel.ConvertToWorkingModel is not working correctly");
        }
    }
}
