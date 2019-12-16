using System;
using StockHouse.Src;
using Xunit;

namespace StockHouseTest.Src
{
    public class UnitTest1
    {
        [Fact]
        public void TestHelloWorld()
        {
            var helloWorld = new HelloWorld();
            Assert.Equal("Hello", helloWorld.Hello());
        }
    }
}
