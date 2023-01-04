// See https://aka.ms/new-console-template for more information
using static System.Console;

class Asm
{
    public static void Main(string[] args)
    {
        if (args.Length != 1)
        {
            WriteLine("USAGE: asm <FILE PATH>");
            Environment.Exit(1);
        }
        string filename = args[0];
        if (!File.Exists(filename))
        {
            WriteLine("{0} not found", filename);
            Environment.Exit(1);
        }
        string output = Parser.Parse(filename);
        string outputName = OutputName(filename);
        File.WriteAllText(outputName, output);
    }

    private static string OutputName(string filename)
    {
        string outputName;

        string[] extensions = filename.Split('.');
        if (extensions.Length < 2)
        {
            // No extension, use whole filename for output
            outputName = filename;
        }
        else
        {
            outputName = String.Join('.', extensions.SkipLast(1));
        }
        outputName = outputName + ".hack";
        // Deal with forward slashes
        return Path.GetFileName(outputName);
    }
}
