// See https://aka.ms/new-console-template for more information
using Libraries;

Console.WriteLine("Hello, World!");

var adb = new AccessDb();

var rdr = adb.Try();
Console.WriteLine(rdr.FieldCount);

while (rdr.Read())
{
    Console.WriteLine(rdr[0]);
    
}