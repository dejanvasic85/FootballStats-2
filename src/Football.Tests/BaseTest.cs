using System;
using System.Collections.Generic;
using Microsoft.Practices.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Football.Tests
{
    /// <summary>
    /// Basic test setup containing mock and verifications
    /// </summary>
    public abstract class BaseTest
    {
        
        protected MockRepository MockRepository;
        protected List<Action> VerifyList;
        protected IUnityContainer Container;

        [TestInitialize]
        public void Initialise()
        {
            Container = new UnityContainer();
            MockRepository = new MockRepository(MockBehavior.Strict);
            VerifyList = new List<Action>();
        }

        [TestCleanup]
        public void TestCleanUp()
        {
            VerifyList.ForEach(action => action());
            VerifyList.Clear();
        }

    }
}