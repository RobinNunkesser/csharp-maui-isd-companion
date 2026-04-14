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
        public void ConvertingNegativeSByteToBinaryYieldsTwosComplementBits()
        {
            var result = Convert.ToString(unchecked((byte)(sbyte)-9), 2).PadLeft(8, '0');
            Assert.AreEqual("11110111", result);
        }
    }
}
