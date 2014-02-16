using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Football.Tests.Extensions
{
    [TestClass]
    public class StringExtensionTests
    {
        [TestMethod]
        public void FromCsv_ValidLineWithDefaultCommaDelimiter_ReturnsStringArray()
        {
            // Arrange 
            const string line = "hello,there,hows,it,going";

            // Act
            var result = line.FromCsv();

            // Assert
            result.IsInstanceOf<string[]>();
            result[0].IsEqualTo("hello");
            result[4].IsEqualTo("going");
        }

        [TestMethod]
        public void FromCsv_ValidLinePipDelimiter_ReturnsStringArray()
        {
            // Arrange 
            const string line = "hello|there|hows|it|going";

            // Act
            var result = line.FromCsv('|');

            // Assert
            result.IsInstanceOf<string[]>();
            result[0].IsEqualTo("hello");
            result[4].IsEqualTo("going");
        }

        [TestMethod]
        public void FromCsv_LineContainsDoubleQuotesWithDelimiter_ReturnsStringArray()
        {
            // Arrange 
            const string line = "hello,there,hows,it,going,\"this is a nice sentence with a comma, hope you like it\"";

            // Act
            var result = line.FromCsv();

            // Assert
            result.IsInstanceOf<string[]>();
            result[0].IsEqualTo("hello");
            result[5].IsEqualTo("this is a nice sentence with a comma, hope you like it");
        }

        [TestMethod]
        public void FromCsv_EmptyString_ThrowsArgumentNullException()
        {
            // Arrange 
            // Act
            // Assert
            Expect.Exception<ArgumentNullException>(() => "".FromCsv());
        }
    }
}
