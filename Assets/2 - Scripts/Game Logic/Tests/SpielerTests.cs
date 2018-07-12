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
    public class SpielerTests
    {
        [TestMethod()]
        [ExpectedException(typeof(System.ArgumentNullException))]
        public void SetSteineTest()
        {
            new Spieler().SetSteine(null);
        }

        [TestMethod()]
        public void GetSteineTest()
        {
            var spieler = new Spieler();
            spieler.SetSteine(new[] { new Stein(), new Stein() });
            Assert.IsNotNull(spieler.GetSteine());
        }
    }
}
*/