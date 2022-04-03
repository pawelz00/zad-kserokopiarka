using Microsoft.VisualStudio.TestTools.UnitTesting;
using ver1;
using System;
using System.IO;

namespace ver1UnitTests
{
    [TestClass]
    public class UnitTestMultifunctionalDevice
    {
        [TestMethod]
        public void MultifunctionalDevice_GetState_StateOff()
        {
            var mfd = new MultifunctionalDevice();
            mfd.PowerOff();

            Assert.AreEqual(IDevice.State.off, mfd.GetState());
        }

        [TestMethod]
        public void MultifunctionalDevice_GetState_StateOn()
        {
            var mfd = new MultifunctionalDevice();
            mfd.PowerOn();

            Assert.AreEqual(IDevice.State.on, mfd.GetState());
        }

        [TestMethod]
        public void MultifunctionalDevice_Print_DeviceOn()
        {
            var mfd = new MultifunctionalDevice();
            mfd.PowerOn();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                IDocument doc1 = new PDFDocument("aaa.pdf");
                mfd.Print(in doc1);
                Assert.IsTrue(consoleOutput.GetOutput().Contains("Print"));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }

        [TestMethod]
        public void MultifunctionalDevice_Print_DeviceOff()
        {
            var mfd = new MultifunctionalDevice();
            mfd.PowerOff();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                IDocument doc1 = new PDFDocument("aaa.pdf");
                mfd.Print(in doc1);
                Assert.IsFalse(consoleOutput.GetOutput().Contains("Print"));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }

        [TestMethod]
        public void MultifunctionalDevice_Scan_DeviceOff()
        {
            var mfd = new MultifunctionalDevice();
            mfd.PowerOff();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                IDocument doc1;
                mfd.Scan(out doc1);
                Assert.IsFalse(consoleOutput.GetOutput().Contains("Scan"));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }

        [TestMethod]
        public void MultifunctionalDevice_Scan_DeviceOn()
        {
            var mfd = new MultifunctionalDevice();
            mfd.PowerOn();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                IDocument doc1;
                mfd.Scan(out doc1);
                Assert.IsTrue(consoleOutput.GetOutput().Contains("Scan"));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }

        [TestMethod]
        public void MultifunctionalDevice_Scan_FormatTypeDocument()
        {
            var mfd = new MultifunctionalDevice();
            mfd.PowerOn();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                IDocument doc1;
                mfd.Scan(out doc1, formatType: IDocument.FormatType.JPG);
                Assert.IsTrue(consoleOutput.GetOutput().Contains("Scan"));
                Assert.IsTrue(consoleOutput.GetOutput().Contains(".jpg"));

                mfd.Scan(out doc1, formatType: IDocument.FormatType.TXT);
                Assert.IsTrue(consoleOutput.GetOutput().Contains("Scan"));
                Assert.IsTrue(consoleOutput.GetOutput().Contains(".txt"));

                mfd.Scan(out doc1, formatType: IDocument.FormatType.PDF);
                Assert.IsTrue(consoleOutput.GetOutput().Contains("Scan"));
                Assert.IsTrue(consoleOutput.GetOutput().Contains(".pdf"));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }

        [TestMethod]
        public void MultifunctionalDevice_PrintCounter()
        {
            var mfd = new MultifunctionalDevice();
            mfd.PowerOn();

            IDocument doc1 = new PDFDocument("aaa.pdf");
            mfd.Print(in doc1);
            IDocument doc2 = new TextDocument("aaa.txt");
            mfd.Print(in doc2);
            IDocument doc3 = new ImageDocument("aaa.jpg");
            mfd.Print(in doc3);

            mfd.PowerOff();
            mfd.Print(in doc3);
            mfd.Scan(out doc1);
            mfd.PowerOn();

            Assert.AreEqual(3, mfd.PrintCounter);
        }

        [TestMethod]
        public void MultifunctionalDevice_ScanCounter()
        {
            var mfd = new MultifunctionalDevice();
            mfd.PowerOn();

            IDocument doc1;
            mfd.Scan(out doc1);
            IDocument doc2;
            mfd.Scan(out doc2);

            IDocument doc3 = new ImageDocument("aaa.jpg");
            mfd.Print(in doc3);

            mfd.PowerOff();
            mfd.Print(in doc3);
            mfd.Scan(out doc1);
            mfd.PowerOn();

            Assert.AreEqual(2, mfd.ScanCounter);
        }

        [TestMethod]
        public void MultifunctionalDevice_PowerOnCounter()
        {
            var mfd = new MultifunctionalDevice();
            mfd.PowerOn();
            mfd.PowerOn();
            mfd.PowerOn();

            IDocument doc1;
            mfd.Scan(out doc1);
            IDocument doc2;
            mfd.Scan(out doc2);

            mfd.PowerOff();
            mfd.PowerOff();
            mfd.PowerOff();
            mfd.PowerOn();

            IDocument doc3 = new ImageDocument("aaa.jpg");
            mfd.Print(in doc3);

            mfd.PowerOff();
            mfd.Print(in doc3);
            mfd.Scan(out doc1);
            mfd.PowerOn();

            // 3 włączenia
            Assert.AreEqual(3, mfd.Counter);
        }

        //Testy fax
        [TestMethod]
        public void MultifunctionalDevice_Fax_DeviceOn()
        {
            var mfd = new MultifunctionalDevice();
            mfd.PowerOn();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                IDocument doc1;
                mfd.Fax(out doc1);
                Assert.IsTrue(consoleOutput.GetOutput().Contains("Fax"));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }

        [TestMethod]
        public void MultifunctionalDevice_Fax_DeviceOff()
        {
            var mfd = new MultifunctionalDevice();
            mfd.PowerOff();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                IDocument doc1;
                mfd.Fax(out doc1);
                Assert.IsFalse(consoleOutput.GetOutput().Contains("Fax"));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }

        [TestMethod]
        public void MultifunctionalDevice_FaxCounter()
        {
            var mfd = new MultifunctionalDevice();
            mfd.PowerOn();

            IDocument doc1;
            mfd.Fax(out doc1);
            IDocument doc2;
            mfd.Fax(out doc2);
            IDocument doc3;
            mfd.Fax(out doc3);

            mfd.PowerOff();
            mfd.Print(in doc3);
            mfd.Scan(out doc1);
            mfd.Fax(out doc1);
            mfd.PowerOn();

            Assert.AreEqual(3, mfd.FaxCounter);
        }

    }
}
