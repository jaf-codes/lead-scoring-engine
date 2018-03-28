using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace LeadScoringEngine.Test
{
    [TestClass]
    public class PercentileCalculatorTests
    {
        [TestMethod]
        public void CalculatePercentile()
        {
            //Arrange
            List<decimal> elements = new List<decimal>()
            {
                0, 1, 2, 3, 4, 5, 6, 7, 8, 9
            };

            PercentileCalculator target = new PercentileCalculator(elements);

            int percentile = 9;
            int expected = 100;

            //Act
            int actual = target.CalculatePercentile(percentile);

            //Assert
            Assert.AreEqual(expected, actual);

        }
    }
}
