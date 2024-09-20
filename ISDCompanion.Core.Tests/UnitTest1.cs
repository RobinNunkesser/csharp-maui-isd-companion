using System;
using NUnit.Framework;

namespace Mensa.Core.Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            var result = Convert.ToString((sbyte)-9, 2)[8..];
            Assert.AreEqual(result, "-1001");
        }
    }
}
