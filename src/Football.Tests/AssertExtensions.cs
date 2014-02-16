using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Football.Tests
{
    /// <summary>
    /// Extension methods for assertions that allows for better readability
    /// </summary>
    public static class AssertExtensions
    {
        public static void IsLessThan(this int value, int expected)
        {
            Assert.IsTrue(value < expected);
        }

        public static void IsLargerThan(this int value, int expected)
        {
            Assert.IsTrue(value > expected);
        }

        public static void IsInstanceOf<TType>(this object value)
        {
            Assert.IsInstanceOfType(value, typeof(TType));
        }

        public static void IsEqualTo<T>(this T actual, T expected)
        {
            Assert.AreEqual(expected, actual);
        }
    }
}