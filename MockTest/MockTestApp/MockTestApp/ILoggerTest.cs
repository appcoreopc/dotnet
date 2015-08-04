using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using ApplicationLogger;

namespace MockTestApp
{
    [TestClass]
    public class ILoggerTest
    {        
        [TestMethod]
        public void Log_InGeneral_Ok()
        {
            var fileLogger = Substitute.For<ApplicationLogger.ILogger<string>>();
            fileLogger.Log("test").Returns(true);
            var result = fileLogger.Log("test");
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Log_InGeneralAnyParameter_Ok()
        {
            var fileLogger = Substitute.For<ApplicationLogger.ILogger<string>>();
            fileLogger.Log(Arg.Any<string>()).Returns(true);
            fileLogger.DidNotReceive().GetLogs(10);
 
            var result = fileLogger.Log("test111");
            Assert.IsTrue(result);
           
        }
    }
}
