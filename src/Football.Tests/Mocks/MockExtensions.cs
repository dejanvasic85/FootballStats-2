using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Microsoft.Practices.Unity;
using Moq;

namespace Football.Tests
{
    /// <summary>
    /// Bunch of extension methods that allows the creation of mocks in a fluent coding style
    /// </summary>
    internal static class MockExtensions
    {
        public static Mock<T> CreateMockOf<T>(this MockRepository repository, IUnityContainer container = null, List<Action> verifyList = null)
            where T : class
        {
            var mock = repository.Create<T>();

            if (container != null)
                mock.RegisterWithBuilder(container);

            if (verifyList != null)
                mock.VerifyWith(verifyList);

            return mock;
        }

        public static void RegisterWithBuilder<T>(this Mock<T> mock, IUnityContainer container)
            where T : class
        {
            container.RegisterInstance(mock.Object);
        }

        public static void VerifyWith<T>(this Mock<T> mock, List<Action> verifyList)
            where T : class
        {
            verifyList.Add(mock.Verify);
        }

        public static Mock<T> SetupWithVerification<T>(this Mock<T> mock, Expression<Action<T>> call) where T : class
        {
            mock.Setup(call).Verifiable();
            return mock; // Allows method chaining
        }

        public static Mock<T> SetupWithVerification<T, TResult>(this Mock<T> mock, Expression<Func<T, TResult>> call, TResult result) where T : class
        {
            mock.Setup(call).Returns(result).Verifiable();
            return mock; // Allows method chaining
        }

        public static Mock<T> SetupGetAndVerify<T, TResult>(this Mock<T> mock, Expression<Func<T, TResult>> propertyExpression, TResult result)
            where T : class
        {
            mock.SetupGet(propertyExpression).Returns(result).Verifiable();
            return mock; // Allows method chaining
        }
    }
}