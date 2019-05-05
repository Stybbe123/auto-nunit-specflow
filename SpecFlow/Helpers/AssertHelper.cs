using NUnit.Framework;

namespace SpecFlow.Helpers
{
    public class AssertHelper
    {
        public AssertHelper()
        {
        }

        public void TwoStringsMatch(string Expected, string Actual)
        {
            Assert.AreEqual(Expected, Actual);
        }

        public void StringContainPart(string Expected, string Actual)
        {
            StringAssert.Contains(Expected, Actual);
        }
    }
}
