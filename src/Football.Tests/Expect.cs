using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Football.Tests
{
    /// <summary>
    /// Performs the action invocation and catches any exceptions to ensure the right one is thrown
    /// </summary>
    public static class Expect
    {
        public static void Exception<T>(Action action, string expectedMessage = "")
            where T : Exception
        {
            try
            {
                action();
            }
            catch (AssertFailedException)
            {
                throw;
            }
            catch (Moq.MockException)
            {
                throw;
            }
            catch (Exception ex)
            {
                Assert.AreEqual(typeof(T), ex.GetType(), "Exception of type {0} was thrown instead of {1}\n{2}", ex.GetType(), typeof(T), ex);

                if (!string.IsNullOrEmpty(expectedMessage))
                    Assert.AreEqual(expectedMessage, ex.Message, "Expected exception to have message: \"{0}\" but received message \"{1}\"", expectedMessage, ex.Message);

                return; // We are happy with this failure :)
            }

            Assert.Fail("Exception of type {0} was not thrown.", typeof(T));
        }
    }
}