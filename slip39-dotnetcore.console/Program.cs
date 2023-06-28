using superenrico.slip39_dotnetcore;

// See https://aka.ms/new-console-template for more information
 
Console.WriteLine("Hello, World!");
Class1 a = new Class1();
foreach(var key in a.hello)
{
    System.Console.WriteLine(Convert.ToHexString(key));
}

