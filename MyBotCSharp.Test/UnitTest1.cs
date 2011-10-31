using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MyBotCSharp.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            int expected = 1;
            int actual = 1;

            Assert.AreEqual(expected, actual, "Test harness is proven to be functioning, but we are in big trouble if this test fails.");
        }
    }
}
