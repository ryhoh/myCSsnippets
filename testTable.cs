using Microsoft.VisualStudio.TestTools.UnitTesting;

using TableLib;


namespace TableTests
{
    [TestClass]
    public class TableTests
    {
        [TestMethod]
        public void TestReadCsv()
        {
            TableLib.Table sampleTable = TableLib.Table.ReadCsv("asset/sample.csv");
            Assert.AreEqual(sampleTable.header[0], "digit");
            Assert.AreEqual(sampleTable.header[1], "string");

            Assert.AreEqual(sampleTable.data[0][0], "1");
            Assert.AreEqual(sampleTable.data[0][1], "one");
            Assert.AreEqual(sampleTable.data[1][0], "2");
            Assert.AreEqual(sampleTable.data[1][1], "two");
            Assert.AreEqual(sampleTable.data[2][0], "3");
            Assert.AreEqual(sampleTable.data[2][1], "three");
        }
    }
}
