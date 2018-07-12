/*
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Logik;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Logik.Tests
{
    [TestCase()]
    public class SteinTests
    {
        [TestMethod()]
        [ExpectedException(typeof(System.ArgumentOutOfRangeException))]
        public void SetPositionXTest()
        {
            new Stein().SetPositionX(-1);
        }

        [TestMethod()]
        [ExpectedException(typeof(System.ArgumentOutOfRangeException))]
        public void SetPositionYTest()
        {
            new Stein().SetPositionY(-3);
        }

        [TestMethod()]
        public void SetIsDameTest()
        {
            var stein = new Stein();
            stein.SetIsDame(true);
            Assert.AreEqual(stein.IsDame(), true);
        }

        [TestMethod()]
        public void GetPositionYTest()
        {
            var stein = new Stein();
            stein.SetPositionY(3);
            Assert.AreEqual(stein.GetPositionY(), 3);
        }

        [TestMethod()]
        public void GetPositionXTest()
        {
            var stein = new Stein();
            stein.SetPositionX(10);
            Assert.AreEqual(stein.GetPositionX(), 10);
        }
    }
}
*/