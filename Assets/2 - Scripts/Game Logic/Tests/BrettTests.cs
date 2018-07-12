/*
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Logik;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logik.Tests
{
    [TestClass()]
    public class BrettTests
    {
        [TestMethod()]
        [ExpectedException(typeof(System.ArgumentOutOfRangeException))]
        public void SetBreiteTest()
        {
            new Brett().SetBreite(-1);
        }

        [TestMethod()]
        [ExpectedException(typeof(System.ArgumentOutOfRangeException))]
        public void SetHöheTest()
        {
            new Brett().SetHöhe(-1);
        }

        [TestMethod()]
        public void GetBreiteTest()
        {
            Brett brett = new Brett();
            brett.SetBreite(2);
            Assert.AreEqual(brett.GetBreite(), 2);
        }

        [TestMethod()]
        public void GetHöheTest()
        {
            Brett brett = new Brett();
            brett.SetHöhe(4);
            Assert.AreEqual(brett.GetHöhe(), 4);
        }
    }
}
*/