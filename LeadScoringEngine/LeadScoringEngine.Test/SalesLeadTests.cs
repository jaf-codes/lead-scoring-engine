using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LeadScoringEngine.Test
{
    [TestClass]
    public class SalesLeadTests
    {
        #region Edge cases
        [TestMethod]
        [ExpectedException(typeof(FormatException), SalesLead.NO_DATA_MESSAGE)]
        public void Serialize_Null()
        {
            //Arrange
            string fileLine = null;

            //Act
            SalesLead target = SalesLead.Deserialize(fileLine);
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException), SalesLead.NO_DATA_MESSAGE)]
        public void Serialize_Nothing()
        {
            //Arrange
            string fileLine = string.Empty;

            //Act
            SalesLead target = SalesLead.Deserialize(fileLine);
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException), SalesLead.NO_DELIMETER_MESSAGE)]
        public void Serialize_NoDelimiter()
        {
            //Arrange
            string fileLine = "1email20.3";

            //Act
            SalesLead target = SalesLead.Deserialize(fileLine);
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException), SalesLead.INSUFFICIENT_DATA_MESSAGE)]
        public void Serialize_InsufficientData()
        {
            //Arrange
            string fileLine = "1,email";

            //Act
            SalesLead target = SalesLead.Deserialize(fileLine);
        }
        #endregion

        [TestMethod]
        public void Serialize_WhiteSpace()
        {
            //Arrange
            string fileLine = "1, email, 23.5";

            SalesLead expected = new SalesLead()
            {
                Id = 1,
                Event = "email",
                Score = 23.5m
            };

            //Act
            SalesLead actual = SalesLead.Deserialize(fileLine);

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Serialize_NoWhiteSpace()
        {
            //Arrange
            string fileLine = "1,email,23.5";

            SalesLead expected = new SalesLead()
            {
                Id = 1,
                Event = "email",
                Score = 23.5m
            };

            //Act
            SalesLead actual = SalesLead.Deserialize(fileLine);

            //Assert
            Assert.AreEqual(expected, actual);
        }
    }
}
