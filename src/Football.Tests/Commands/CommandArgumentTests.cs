using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Football.Commands;

namespace Football.Tests.Commands
{
    [TestClass]
    public class CommandArgumentsTests
    {
        [TestMethod]
        public void FromArray_OneItemInArray_ThrowsArgumentException()
        {
            // Arrange
            var args = new[] { "Value" };

            // Act and Assert
            Expect.Exception<ArgumentException>(() => CommandArguments.FromArray(args), "Number of arguments is invalid.");
        }

        [TestMethod]
        public void FromArray_ThreeItemsInArray_ThrowsArgumentException()
        {
            // Arrange
            var args = new[] { CommandArguments.CommandFullArgName, "RandomArg", "-FirstCmdArg" };

            // Act and Assert
            Expect.Exception<ArgumentException>(() => CommandArguments.FromArray(args), "Number of arguments is invalid.");
        }

        [TestMethod]
        public void FromArray_NoTaskNameInArray_ThrowsArgumentExeption()
        {
            // Arrange
            var args = new[] { "Arg1", "Val" };

            // Act and Assert
            Expect.Exception<ArgumentException>(
                () => CommandArguments.FromArray(args),
                string.Format("Command name argument must be supplied in format of {0} <Name>.", CommandArguments.CommandFullArgName));
        }

        [TestMethod]
        public void FromArray_NoArgs_ThrowsArgumentExeption()
        {
            // Arrange
            var args = new string[] { };

            // Act and Assert
            Expect.Exception<ArgumentException>(() => CommandArguments.FromArray(args), "Number of arguments is invalid.");
        }

        [TestMethod]
        public void FromArray_NullArgs_ThrowsArgumentExeption()
        {
            // Arrange
            string[] args = null;

            // Act and Assert
            // ReSharper disable ExpressionIsAlwaysNull
            Expect.Exception<ArgumentException>(() => CommandArguments.FromArray(args), "Number of arguments is invalid.");
            // ReSharper restore ExpressionIsAlwaysNull
        }

        [TestMethod]
        public void FromArray_TaskNameValueNotSupplied_ThrowsArgumentException()
        {
            // Arrange
            var args = new[] { CommandArguments.CommandFullArgName, "" };

            // Act and Assert
            Expect.Exception<ArgumentException>(
                () => CommandArguments.FromArray(args),
                "Command name has no value supplied.");
        }

        [TestMethod]
        public void FromArray_TaskWithNoOtherArgs_IsEmpty()
        {
            // Arrange 
            var args = new[] { CommandArguments.CommandFullArgName, "CoolTaskName" };

            // Act
            var arguments = CommandArguments.FromArray(args);

            // Assert
            Assert.AreEqual("CoolTaskName", arguments.CommandName);
            Assert.AreEqual(0, arguments.Count);
        }

        [TestMethod]
        public void FromArray_TaskNameWithOneArg_HasOneItem()
        {
            // Arrange 
            var args = new[] { CommandArguments.CommandFullArgName, "CoolTaskName", "-Arg1", "Random" };

            // Act
            var arguments = CommandArguments.FromArray(args);

            // Assert
            Assert.AreEqual("CoolTaskName", arguments.CommandName);
            Assert.AreEqual(1, arguments.Count);
        }

        [TestMethod]
        public void FromArray_ArgNameNotPrefixed_ThrowsArgumentException()
        {
            // Arrange
            var args = new[] { CommandArguments.CommandFullArgName, "CoolTaskName", "BadArgWithNoDashPrefix", "RandomValue" };

            // Act and Assert
            Expect.Exception<ArgumentException>(
                () => CommandArguments.FromArray(args),
                string.Format("Bad argument {0}. Please specify arguments with a preceding [{1}] and value after it.", "BadArgWithNoDashPrefix", CommandArguments.ArgumentPrefix));
        }

        [TestMethod]
        public void FromArray_TaskNameNotFirstItem_HasOneItem()
        {
            // Arrange 
            var args = new[] { "-Arg1", "Random", CommandArguments.CommandFullArgName, "CoolTaskName" };

            // Act
            var arguments = CommandArguments.FromArray(args);

            // Assert
            Assert.AreEqual("CoolTaskName", arguments.CommandName);
            Assert.AreEqual(1, arguments.Count);
        }

        [TestMethod]
        public void FromArray_TenArgumentsWith_HasFourItems()
        {
            // Arrange 
            var args = new[]
                {
                    CommandArguments.CommandFullArgName, "RunSomethingSweet",
                    "-RandomArgName", "{ChainedArg}-SweetMan",
                    "-ChainedArg", "",
                    "-Arg3", "value 3",
                    "-Arg4", "value 4"
                };

            // Act
            var arguments = CommandArguments.FromArray(args);

            // Assert
            Assert.AreEqual("RunSomethingSweet", arguments.CommandName);
            Assert.AreEqual(4, arguments.Count);
        }

        [TestMethod]
        public void ReadArgument_RequiredArgNotExistsNoDefaultSupplied_ThrowsArgumentException()
        {
            // Arrange
            var arguments = CommandArguments.FromArray(new[]
                {
                    CommandArguments.CommandFullArgName, "RunSomethingSweet",
                    "-RandomArgName", "SweetValue"
                });

            // Act 
            // Assert
            Expect.Exception<ArgumentException>(() => arguments.ReadArgument("blah", isRequired: true));
        }

        [TestMethod]
        public void ReadArgument_RequiredArgNotExistsUseDefault_ThrowsArgumentException()
        {
            // Arrange
            var arguments = CommandArguments.FromArray(new[]
                {
                    CommandArguments.CommandFullArgName, "RunSomethingSweet",
                    "-RandomArgName", "SweetValue"
                });

            // Act 
            Expect.Exception<ArgumentException>(() => arguments.ReadArgument("blah", isRequired: true, readDefault: () => "defaultValue"));
        }

        [TestMethod]
        public void ReadArgument_ArgNotExistsUseDefault_ReturnsDefault()
        {
            // Arrange
            var arguments = CommandArguments.FromArray(new[]
                {
                    CommandArguments.CommandFullArgName, "RunSomethingSweet",
                    "-RandomArgName", "SweetValue"
                });

            // Act 
            var value = arguments.ReadArgument("blah", readDefault: () => "defaultValue");

            // Assert
            Assert.AreEqual("defaultValue", value);
        }

        [TestMethod]
        public void ReadArgument_IntegerArgNotExistsUseDefault_ReturnsDefault()
        {
            // Arrange
            var arguments = CommandArguments.FromArray(new[]
                {
                    CommandArguments.CommandFullArgName, "RunSomethingSweet",
                    "-RandomArgName", "SweetValue"
                });

            // Act 
            var value = arguments.ReadArgument("blah", readDefault: () => 1);

            // Assert
            Assert.AreEqual(1, value);
        }

        [TestMethod]
        public void ReadArgument_ArgExists_ReturnsArgValue()
        {
            // Arrange
            var arguments = CommandArguments.FromArray(new[]
                {
                    CommandArguments.CommandFullArgName, "RunSomethingSweet",
                    "-RandomArgName", "SweetValue"
                });

            // Act 
            var value = arguments.ReadArgument("RandomArgName");

            // Assert
            Assert.AreEqual("SweetValue", value);
        }

        [TestMethod]
        public void ReadArgument_NonRequiredArgNotExists_ReturnsNull()
        {
            // Arrange
            var arguments = CommandArguments.FromArray(new[]
                {
                    CommandArguments.CommandFullArgName, "RunSomethingSweet",
                    "-RandomArgName", "SweetValue"
                });

            // Act 
            var value = arguments.ReadArgument("DoesNotExistAndOptional");

            // Assert
            Assert.IsNull(value);
        }

        [TestMethod]
        public void ReadArgument_ArgIsIntegerIsAvailable_ReturnsInteger()
        {
            // Arrange
            var arguments = CommandArguments.FromArray(new[]
                {
                    CommandArguments.CommandFullArgName, "RunSomethingSweet",
                    "-Number", "1"
                });

            // Act 
            var value = arguments.ReadArgument<int>("Number");

            // Assert
            Assert.AreEqual(1, value);
        }

        [TestMethod]
        public void ReadArgument_ArgIsIntegerNotExistsNoDefault_ThrowsArgumentException()
        {
            // Arrange
            var arguments = CommandArguments.FromArray(new[]
                {
                    CommandArguments.CommandFullArgName, "RunSomethingSweet",
                    "-Number", "1"
                });

            // Act 
            Expect.Exception<ArgumentException>(() => arguments.ReadArgument<int>("NotValidArgName"));
        }

        [TestMethod]
        public void ReadArgument_ArgValueCannotCast_ThrowsFormatException()
        {
            // Arrange
            var arguments = CommandArguments.FromArray(new[]
                {
                    CommandArguments.CommandFullArgName, "RunSomethingSweet",
                    "-Number", "bullshit"
                });

            // Act 
            Expect.Exception<FormatException>(() => arguments.ReadArgument<int>("Number"));
        }

        [TestMethod]
        public void ReadArgument_WithOneEmbeddedValue_DoesSubsitution()
        {
            // Arrange
            var arguments = CommandArguments.FromArray(new[]
                {
                    CommandArguments.CommandFullArgName, "RunSomethingSweet",
                    "-Arg1", "America",
                    "-Arg2", "Captain - {Arg1}"
                });

            // Act 
            var value = arguments.ReadArgument("Arg2");

            // Assert
            Assert.AreEqual("Captain - America", value);
        }

        [TestMethod]
        public void ReadArgument_WithMultipleEmbeddedValues_DoesSubsitution()
        {
            // Arrange
            var arguments = CommandArguments.FromArray(new[]
                {
                    CommandArguments.CommandFullArgName, "RunSomethingSweet",
                    "-Arg1", "America",
                    "-Arg2", "Captain - {Arg1} {Final}",
                    "-Final", "Is Number One"
                });

            // Act 
            var value = arguments.ReadArgument("Arg2");

            // Assert
            Assert.AreEqual("Captain - America Is Number One", value);
        }

        [TestMethod]
        public void ReadArgument_WithOneEmbeddedValueCaseSensitive_DoesNotDoSubsitution()
        {
            // Arrange
            var arguments = CommandArguments.FromArray(new[]
                {
                    CommandArguments.CommandFullArgName, "RunSomethingSweet",
                    "-Arg1", "America",
                    "-Arg2", "Captain - {arg1}"
                });

            // Act 
            var value = arguments.ReadArgument("Arg2");

            // Assert
            Assert.AreEqual("Captain - {arg1}", value);
        }
    }
}
