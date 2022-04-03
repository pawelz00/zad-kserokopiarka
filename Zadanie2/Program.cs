using ver1;

var xerox = new Copier();
xerox.PowerOn();
IDocument doc1 = new PDFDocument("aaa.pdf");
xerox.Print(in doc1);

IDocument doc2;
xerox.Scan(out doc2);

xerox.ScanAndPrint();
System.Console.WriteLine(xerox.Counter);
System.Console.WriteLine(xerox.PrintCounter);
System.Console.WriteLine(xerox.ScanCounter);
Console.WriteLine("-----");
var multi = new MultifunctionalDevice();

multi.PowerOn();
IDocument doc;
IDocument docfax;
multi.Fax(out doc);
multi.PowerOff();
multi.Fax(out docfax);
multi.Fax(out doc);
multi.Fax(out docfax);
multi.PowerOn();
Console.WriteLine("-----");
multi.Fax(out docfax);
multi.Fax(out doc);