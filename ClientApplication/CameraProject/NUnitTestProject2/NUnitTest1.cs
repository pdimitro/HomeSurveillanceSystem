using System;
using NUnit.Framework;
using CameraProject.Logic;

namespace NUnitTestProject2
{
    //[TestFixture]
    [TestFixture]
    public class NUnitTest1
    {
        [Test]
        public void TestMethod1()
        {
            String user = "admin";
            String pass = "12245";
            var vr = new LoginValidation(user, pass);
            bool isValid = vr.isUserInputValid();
            Assert.AreEqual(isValid, false);
        }
    }
}