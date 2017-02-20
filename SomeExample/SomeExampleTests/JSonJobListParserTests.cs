using GalaSoft.MvvmLight.Threading;
using NUnit.Framework;
using SomeExample.Data;
using SomeExample.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SomeExampleTests
{
    [TestFixture]
    public class JSonJobListParserTests
    {
        [Test]
        public async Task IfJobListIsEmpty()
        {
            //Arrange
            JsonJobListParser jp = new JsonJobListParser();

            //Act
            DispatcherHelper.Initialize();
            var fromMethod = await jp.ParseJobListAsync(string.Empty);

            //Assert
            Assert.That(fromMethod, Is.EqualTo(new List<Job>()));
        }
    }
}
