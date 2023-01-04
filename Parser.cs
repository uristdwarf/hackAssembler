using static System.Console;

public class Parser
{
    static Dictionary<string, string> compBits = new Dictionary<string, string>()
    {
        ["0"] = "0101010",
        ["1"] = "0111111",
        ["-1"] = "0111010",
        ["D"] = "0001100",
        ["A"] = "0110000",
        ["!D"] = "0001101",
        ["!A"] = "0110001",
        ["-D"] = "0001111",
        ["-A"] = "0110011",
        ["D+1"] = "0011111",
        ["A+1"] = "1101111",
        ["D-1"] = "0001110",
        ["A-1"] = "0110010",
        ["D+A"] = "0000010",
        ["D-A"] = "0010011",
        ["A-D"] = "0000111",
        ["D&A"] = "0000000",
        ["D|A"] = "0010101",

        ["M"] = "1110000",
        ["!M"] = "1110001",
        ["-M"] = "1110111",
        ["M+1"] = "1110111",
        ["M-1"] = "1110010",
        ["D+M"] = "1000010",
        ["D-M"] = "1010011",
        ["M-D"] = "1000111",
        ["D&M"] = "1000000",
        ["D|M"] = "1010101",
    };
    public static string Parse(string filename)
    {
        string output = "";
        using (StreamReader sr = File.OpenText(filename))
        {
            string? currLine;
            int lc = 1;
            while ((currLine = sr.ReadLine()) != null)
            {
                string[] comments = currLine.Split("//", 2);
                // Empty line or comment line
                if (comments[0] == "")
                {
                    lc++;
                    continue;
                }
                else if (isAddress(comments[0]))
                {
                    output += addressBinary(comments[0].Split('@')[1]) + "\n";
                }
                else
                {
                    output += "111" + compCommand(comments[0]) + "000" + "000" + "\n";
                }
                lc++;
            }
        }
        return output;
    }

    static string addressBinary(string val)
    {
        string binary = Convert.ToString(Convert.ToInt16(val), 2);
        return addZeroes(binary);
    }

    static string addZeroes(string binary)
    {
        int toAdd = 16 - binary.Length;
        string s = "0";
        for (int i = 1; i < toAdd; i++)
        {
            s += "0";
        }
        return s + binary;
    }

    static bool isAddress(string line)
    {
        return line.First() == '@';
    }

    static string? destCommand(string line)
    {
        return "";
    }

    static string compCommand(string line)
    {
        var destIndex = line.IndexOf('=');
        if (destIndex != -1)
        {
            line = line.Substring(destIndex + 1);
        }
        var jumpIndex = line.IndexOf(';');
        if (jumpIndex != -1)
        {
            line = line.Substring(0, jumpIndex);
        }
        return compBits[line];
    }

    static string? jumpCommand(string line)
    {
        return "";
    }
}
