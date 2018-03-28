using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LeadScoringEngine;
using LeadScoringEngine.FileLoading;
using LeadScoringEngine.Test.MockObjects;
using System.Linq;
using System.Collections.Generic;

namespace LeadScoringEngine.Test
{
    public class TestType
    {
        public string Id { get; set; }

        public TestType Deserialize(string fileLine)
        {
            return new TestType() { Id = fileLine };
        }

        public override bool Equals(object obj)
        {
            TestType other = obj as TestType;
            return (obj != null &&
                    this.Id == other.Id);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }

    [TestClass]
    public class FileLoaderTests
    {
        private FileLoader<TestType> target;

        [TestInitialize]
        public void InitializeTest()
        {
            target = new FileLoader<TestType>(new TestTextDisplayer());
        }

        private void CheckErrorMessages(TestTextDisplayer displayer)
        {
            if (displayer.Messages.Count() > 0)
            {
                string allMessages = "Errors found: ";
                foreach (string message in displayer.Messages)
                {
                    allMessages = string.Concat(allMessages, message, " ");
                }
                allMessages = allMessages.TrimEnd();
                throw new InternalApplicationException(allMessages);
            }
        }

        private bool AreEqual(IEnumerable<TestType> listA, IEnumerable<TestType> listB)
        {
            if (listA == null ^ listB == null)
            {
                return false;
            }

            if (listA.Count() != listB.Count())
            {
                return false;
            }

            foreach (TestType member in listA)
            {
                if (!listB.Contains(member))
                {
                    return false;
                }
            }

            foreach (TestType member in listB)
            {
                if (!listA.Contains(member))
                {
                    return false;
                }
            }

            return true;
        }

        [TestMethod]
        public void FileLoader_LoadFromFile_Null()
        {
            //Arrange
            target = new FileLoader<TestType>(null);

            //Act
            target.LoadFromFile(null, new TestType().Deserialize);

            //Assert
            //Shouldn't throw an exception, no way of knowing otherwise
        }

        [TestMethod]
        [ExpectedException(typeof(InternalApplicationException))]
        public void FileLoader_LoadFromFile_DirectoryNotFound()
        {
            //Arrange
            TestTextDisplayer displayer = new TestTextDisplayer();
            target = new FileLoader<TestType>(displayer);
            string bogusDirectoryFile = "BestFiles\\empty.txt";

            //Act
            target.LoadFromFile(bogusDirectoryFile, new TestType().Deserialize);

            //Assert
            CheckErrorMessages(displayer);
        }

        [TestMethod]
        [ExpectedException(typeof(InternalApplicationException))]
        public void FileLoader_LoadFromFile_FileNotFound()
        {
            //Arrange
            TestTextDisplayer displayer = new TestTextDisplayer();
            target = new FileLoader<TestType>(displayer);
            string bogusDirectoryFile = "TestFiles\\notFound.txt";

            //Act
            target.LoadFromFile(bogusDirectoryFile, new TestType().Deserialize);

            //Assert
            CheckErrorMessages(displayer);
        }

        [TestMethod]
        public void FileLoader_LoadFromFile_InvalidRow()
        {
            //Arrange
            TestTextDisplayer displayer = new TestTextDisplayer();
            target = new FileLoader<TestType>(displayer);
            string bogusDirectoryFile = "TestFiles\\invalidRow.txt";

            IEnumerable<TestType> expected = new List<TestType>();

            //Act
            IEnumerable<TestType> actual = target.LoadFromFile(bogusDirectoryFile, new TestType().Deserialize);

            //Assert
            CheckErrorMessages(displayer);
            Assert.IsTrue(AreEqual(expected, actual));

        }

        [TestMethod]
        public void FileLoader_LoadFromFile_ValidRow()
        {
            //Arrange
            TestTextDisplayer displayer = new TestTextDisplayer();
            target = new FileLoader<TestType>(displayer);
            string bogusDirectoryFile = "TestFiles\\validRow.txt";

            IEnumerable<TestType> expected = new List<TestType>()
            {
                new TestType(){ Id = "1" }
            };

            //Act
            IEnumerable<TestType> actual = target.LoadFromFile(bogusDirectoryFile, new TestType().Deserialize);

            //Assert
            CheckErrorMessages(displayer);
            Assert.IsTrue(AreEqual(expected, actual));
        }
    }
}
