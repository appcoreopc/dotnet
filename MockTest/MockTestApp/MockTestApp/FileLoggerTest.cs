using ApplicationLogger;
using Microsoft.QualityTools.Testing.Fakes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MockTestApp
{

    [TestClass]
    public class FileLoggerTest
    {

        [TestMethod]
        public void Log_InGeneral_Ok()
        {
            using (ShimsContext.Create())
            {
                System.IO.Fakes.ShimFile.AppendAllTextStringString = (a, b) =>
                {
                };

                var filLogger = new FileLogger();
                var result = filLogger.Log("testing");

                Assert.IsTrue(result);
            }
        }

        [TestMethod]
        public void Log_ExceptionTookPlace_HandleOk()
        {
            using (ShimsContext.Create())
            {
                System.IO.Fakes.ShimFile.AppendAllTextStringString = (a, b) =>
                {
                    a = "c:\\test.log";
                    b = "fakefilecontents";
                    throw new ArgumentException("Ops!");
                };

                var filLogger = new FileLogger();
                var result = filLogger.Log("testing");

                Assert.IsFalse(result);
            }
        }


    }
}
