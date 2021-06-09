﻿using NUnit.Framework;
using SoftAPIClient.Attributes;
using SoftAPIClient.Core;
using SoftAPIClient.Core.Exceptions;
using SoftAPIClient.Core.Interfaces;
using SoftAPIClient.MetaData;
using System;
using System.Linq;

namespace SoftAPIClient.Tests
{
    public class RequestFactoryTests : AbstractTest
    {
        [Test]
        public void VerifyInitializationExceptionWhenNullUrlProvided()
        {
            var targetInterface = typeof(ITestInterface);
            var methodName = "Get";
            var arguments = new []{ "1" };

            var requestFactory = new RequestFactory(targetInterface, targetInterface.GetMethod(methodName), arguments);

            var ex = Assert.Throws<InitializationException>(() => requestFactory.BuildRequest());
            Assert.AreEqual($"The result URL is not defined at the interface '{nameof(ITestInterface)}' and method '{methodName}'", ex.Message);
        }

        [Test]
        public void VerifyInitializationExceptionWhenDifferentArgumentsProvided()
        {
            var targetInterface = typeof(ITestInterface);
            var methodName = "Get";
            var arguments = new[] { "1" , "2"};

            var requestFactory = new RequestFactory(targetInterface, targetInterface.GetMethod(methodName), arguments);

            var ex = Assert.Throws<InitializationException>(() => requestFactory.BuildRequest());
            Assert.AreEqual($"Argument count '{2}' and MethodInfo count '{1}' " +
                    $"is not matched for the method '{methodName}' in type '{nameof(ITestInterface)}'", ex.Message);
        }

        [Test]
        public void VerifyDefaultResponseInterceptors()
        {
            var targetInterface = typeof(ITestInterface);
            var methodName = "Get";
            var arguments = new[] { "1" };

            var requestFactory = new RequestFactory(targetInterface, targetInterface.GetMethod(methodName), arguments);

            Assert.AreEqual(Enumerable.Empty<IResponseInterceptor>(), requestFactory.ResponseInterceptors);
        }

        [Client]
        private interface ITestInterface
        {
            [RequestMapping("GET", Path = "/path")]
            Func<Response> Get([QueryParameter("id")] string id);
        }
    }
}
