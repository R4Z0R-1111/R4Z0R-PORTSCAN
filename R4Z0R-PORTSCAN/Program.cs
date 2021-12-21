using System.Net;
using System.Net.Sockets;
using Figgle;
using ConsoleTables;
using System.Threading;
using System;
using R4Z0R_PORTSCAN;

Console.ForegroundColor = ConsoleColor.Magenta;
Console.WriteLine(
FiggleFonts.Doom.Render("R4Z0R"));
Console.ForegroundColor = ConsoleColor.DarkMagenta;
Console.WriteLine(
FiggleFonts.Doom.Render("PORTSCAN"));
Console.ForegroundColor = ConsoleColor.White;

Console.WriteLine("+--------------------------------------------------------------+");
Console.WriteLine("");
Console.Write("Enter the IP Adress: ");
var ipadress = Console.ReadLine();
Console.Write("Enter the First Port: ");
int first = Convert.ToInt32(Console.ReadLine());
Console.Write("Enter the Last Port: ");
int last = Convert.ToInt32(Console.ReadLine());
Console.WriteLine("");
Console.WriteLine("+--------------------------------------------------------------+");
Console.WriteLine("");

//Console.WriteLine(String.Format("|{0,5}|{1,2}|{2,5}|{3,5}|", "IP-ADRESS","PORT","IP+PORT","STATUS"));
int scanned = 1;
Console.WriteLine("");
var table = new ConsoleTable("IP-ADRESS","PORT","IP+PORT","STATUS");
table.Configure(o => o.NumberAlignment = Alignment.Right);

for (int i = first; i <= last; i++)
{

    Scann(ipadress.ToString(), i);

}

static void ClearCurrentConsoleLine()
{
    int currentLineCursor = Console.CursorTop;
    Console.SetCursorPosition(0, Console.CursorTop);
    Console.Write(new string(' ', Console.WindowWidth));
    Console.SetCursorPosition(0, currentLineCursor);
}

void Scann(string ipaddress, int port)
{
    IPAddress ipo = IPAddress.Parse(ipaddress);
    IPEndPoint ipEo = new IPEndPoint(ipo, port);
    try
    {
        Socket sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);  // Verbindung vorbereiten
        sock.Connect(ipEo);  // Verbindung aufbauen
        Console.ForegroundColor = ConsoleColor.Green;
        //Console.WriteLine(String.Format("|{0,5}|{1,2}|{2,5}|{3,5}|", ipaddress, "["+port+"]", ipEo, "[OPEN]"));
        table.AddRow(ipaddress,"[-"+port+"-]", ipEo, "OPEN");
        sock.Close();  //Verbindung schließen wird nicht mehr gebraucht
        Console.SetCursorPosition(0, Console.CursorTop - 1);
        ClearCurrentConsoleLine();
        Console.WriteLine("Scanned: " + scanned + " from " + (last-first+1));
        scanned++;
    }
    catch (Exception)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        //Console.WriteLine(String.Format("|{0,5}|{1,2}|{2,5}|{3,5}|", ipaddress, "[" + port + "]", ipEo, "[CLOSED]"));
        table.AddRow(ipaddress,"[-"+port +"-]", ipEo, "CLOSED");
        //Console.WriteLine();
        Console.SetCursorPosition(0, Console.CursorTop - 1);
        ClearCurrentConsoleLine();
        Console.WriteLine("Scanned: " + scanned + " from " + (last-first+1));
        //Kein offener Port da oben ein Fehler aufgetreten, diese wird einfach ignoriert da es keine Ausnahme für geschlossene Ports gibt und nur die offenen interessant sind.
        scanned++;
    }
}

Console.ForegroundColor = ConsoleColor.White;
Console.WriteLine("");
table.Write(Format.Alternative);
Console.ForegroundColor = ConsoleColor.White;
Console.WriteLine("Scanned all the Ports!");
Console.WriteLine("");
Console.WriteLine("IF YOU NEED ANY HELP, ADD ME ON DISCORD - R4Z0R#1111");
var end = Console.ReadLine();