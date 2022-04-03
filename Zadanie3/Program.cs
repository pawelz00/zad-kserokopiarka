// See https://aka.ms/new-console-template for more information

using ver1;

IDocument doc = new TextDocument("hej.txt");
IDocument doc2;
var copier = new Copier(new Printer(), new Scanner());
copier.ScannerPowerOn();
copier.PrinterPowerOn();
copier.Print(in doc);
copier.Scan(out doc, IDocument.FormatType.PDF);
copier.Scan(out doc2);
copier.ScannerPowerOff();
copier.PrinterPowerOff();
Console.WriteLine("--------------------------");
var multi = new MultidimensionalDevice(new Printer(), new Scanner(), new Faxer());
multi.FaxerPowerOn();
multi.PrinterPowerOn();
multi.ScannerPowerOn();
multi.Fax(out doc2);
multi.ScanAndPrint();
Console.WriteLine("--------------------------");