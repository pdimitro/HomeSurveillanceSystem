﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CameraProject.Logic;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var newAtt = new CameraProject.Logic.LoginValidation("admin2","123456");
        }
    }
}
