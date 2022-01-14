using System;
using NUnit.Framework;
using CameraProject.Logic;

namespace NUnitTestProject1
{
    //[TestFixture]
    public class NUnitTest1
    {
        //[Test]
        public void TestMethod1()
        {
            var newVar = new LoginValidation("admin2", "pass1244");
            bool cred = newVar.isUserInputValid();

            Asse
            AreEqual(cred,fale);
        }
    }
}