using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tulpep.CentralNetSend.Net;

namespace Tulpep.CentralNetSend.Test
{
    [TestClass]
    public class TestOfWorkingDavivienda
    {
        [TestMethod]
        public void Test1()
        {
            string[] arguments = new string[] { "send", "AD9075W702.DAVIVIENDA.LOC", "Hola" };
            int result = Program.Main(arguments);
            Assert.AreEqual(0, result);
        }
    }
}
