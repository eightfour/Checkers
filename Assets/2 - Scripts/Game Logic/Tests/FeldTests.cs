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
    public class FeldTests
    {
        [TestMethod()]
        [ExpectedException(typeof(System.ArgumentOutOfRangeException))]
        public void SetPositionXTest()
        {
            new Feld().SetPositionX(-1);
        }

        [TestMethod()]
        [ExpectedException(typeof(System.ArgumentOutOfRangeException))]
        public void SetPositionYTest()
        {
            new Feld().SetPositionY(-1);
        }

        [TestMethod()]
        public void SetFarbeTest()
        {
            new Feld().SetFarbe(Farbe.Schwarz);
        }

        [TestMethod()]
        public void GetFarbeTest()
        {
            var feld = new Feld();
            feld.SetFarbe(Farbe.Schwarz);
            Assert.AreEqual(feld.GetFarbe(), Farbe.Schwarz);
        }

        [TestMethod()]
        public void GetPositionYTest()
        {
            var feld = new Feld();
            feld.SetPositionY(5);
            Assert.AreEqual(feld.GetPositionY(), 5);
        }

        [TestMethod()]
        public void GetPositionXTest()
        {
            var feld = new Feld();
            feld.SetPositionX(7);
            Assert.AreEqual(feld.GetPositionX(), 7);
        }
    }
}
*/