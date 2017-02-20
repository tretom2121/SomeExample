using NUnit.Framework;
using SomeExample.Helpers;
using SomeExample.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SomeExampleTests
{
    [TestFixture]
    public class ListToTxtConverterTests
    {
        [Test]
        public void IfJobListIsNull()
        {
            //Arrange
            ListToTxtConverter conv = new ListToTxtConverter();
            List<Job> stubJobList = null;

            //Act
            string strFromMethod = conv.ConvertList(stubJobList);

            //Assert
            Assert.That(strFromMethod, Is.EqualTo(string.Empty));
        }

        [Test]
        public void TwoEmptyLinesBeforeEachJob()
        {
            //Arrange
            ListToTxtConverter conv = new ListToTxtConverter();
            
            #region stubJobList
            Job stubJob1 = new Job()
            {
                title = "Test title1"
            };
            Job stubJob2 = new Job()
            {
                title = "Test title2"
            };
            #endregion //stubJobList

            List<Job> stubJobList = new List<Job>()
            { stubJob1, stubJob2 };


            string strExpected = Environment.NewLine + "Test title1" + Environment.NewLine + Environment.NewLine + "Test title2" + Environment.NewLine;

            //Act
            string strFromMethod = conv.ConvertList(stubJobList);

            //Assert
            Assert.That(strFromMethod, Is.EqualTo(strExpected));
        }

        [Test]
        public void IfTitleIsEmptyEnterKA()
        {
            //Arrange
            ListToTxtConverter conv = new ListToTxtConverter();

            #region stubJobList
            Job stubJob1 = new Job()
            {
                refNum = ""
            };
            Job stubJob2 = new Job()
            {
                title = "Test title2"
            };
            #endregion //stubJobList

            List<Job> stubJobList = new List<Job>()
            { stubJob1, stubJob2 };


            string strExpected = Environment.NewLine + "Not specified" + Environment.NewLine + Environment.NewLine + "Test title2" + Environment.NewLine;

            //Act
            string strFromMethod = conv.ConvertList(stubJobList);

            //Assert
            Assert.That(strFromMethod, Is.EqualTo(strExpected));
        }

    }
}
