using System;

namespace ver1
{
    public interface IDevice
    {
        enum State {on, off};

        void PowerOn(); // uruchamia urządzenie, zmienia stan na `on`
        void PowerOff(); // wyłącza urządzenie, zmienia stan na `off
        State GetState(); // zwraca aktualny stan urządzenia

        int Counter {get;}  // zwraca liczbę charakteryzującą eksploatację urządzenia,
                            // np. liczbę uruchomień, liczbę wydrukow, liczbę skanów, ...
    }

    public abstract class BaseDevice : IDevice
    {
        protected IDevice.State state = IDevice.State.off;
        public IDevice.State GetState() => state;
        
        public void PowerOff()
        {
            if (state == IDevice.State.off) { }
            else 
            { 
                state = IDevice.State.off;
                Console.WriteLine("... Device is off !");
            }
        }

        public void PowerOn()
        {
            if (state == IDevice.State.on) { }
            else if (state == IDevice.State.off)
            {
                state = IDevice.State.on;
                Console.WriteLine("Device is on ...");
                Counter++;
            }
        }

        public int Counter { get; private set; } = 0;
    }

    public class Copier : BaseDevice, IPrinter, IScanner
    {
        public int PrintCounter { get; private set; } = 0;
        public int ScanCounter { get; private set;} = 0;

        public DateTime localDate = DateTime.Now;
        public void Print(in IDocument document)
        {
            if (state == IDevice.State.off) { return; }
            else
            {
                Console.WriteLine($"{localDate} Print: {document.GetFileName()}");
                PrintCounter++;
            }
        }

        public void Scan(out IDocument document, IDocument.FormatType formatType)
        {
            if (formatType == IDocument.FormatType.PDF)
            {
                document = new PDFDocument($"PDFScan{ScanCounter}.pdf");
            }
            else if (formatType == IDocument.FormatType.JPG)
            {
                document = new ImageDocument($"ImageScan{ScanCounter}.jpg");
            }
            else
            {
                document = new TextDocument($"TextScan{ScanCounter}.txt");
            }

            if (state == IDevice.State.on)
            {
                ScanCounter++;
                Console.WriteLine($"{localDate} Scan: {document.GetFileName()}");
            }
        }

        public void Scan(out IDocument document)
        {
            string filename = $"ScannedDocument{ScanCounter}.jpg";
            document = new ImageDocument(filename);

            if (state == IDevice.State.on)
            {
                ScanCounter++;
                Console.WriteLine($"{localDate} Scan: {filename}");
            }
            else return;
        }

        public void ScanAndPrint()
        {
            if (state == IDevice.State.on)
            {
                IDocument doc;
                Scan(out doc);
                Print(doc);
            }
        }
    }

    public interface IPrinter : IDevice
    {
        /// <summary>
        /// Dokument jest drukowany, jeśli urządzenie włączone. W przeciwnym przypadku nic się nie wykonuje
        /// </summary>
        /// <param name="document">obiekt typu IDocument, różny od `null`</param>
        void Print(in IDocument document);
    }

    public interface IScanner : IDevice
    {
        // dokument jest skanowany, jeśli urządzenie włączone
        // w przeciwnym przypadku nic się dzieje
        void Scan(out IDocument document, IDocument.FormatType formatType);
    }

}
