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
        ["A+1"] = "0110111",
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
    static Dictionary<string, string> destBits = new Dictionary<string, string>()
    {
        ["M"] = "001",
        ["D"] = "010",
        ["MD"] = "011",
        ["A"] = "100",
        ["AM"] = "101",
        ["AD"] = "110",
        ["AMD"] = "111",
    };
    static Dictionary<string, string> jumpBits = new Dictionary<string, string>()
    {
        ["JGT"] = "001",
        ["JEQ"] = "010",
        ["JGE"] = "011",
        ["JLT"] = "100",
        ["JNE"] = "101",
        ["JLE"] = "110",
        ["JMP"] = "111",
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
                string cmd = comments[0];
                // Empty line or comment line
                if (cmd == "")
                {
                    lc++;
                    continue;
                }
                else if (isAddress(cmd))
                {
                    output += addressBinary(cmd.Split('@')[1]) + "\n";
                }
                else
                {
                    output += "111" + compCommand(cmd) + destCommand(cmd) + jumpCommand(cmd) + "\n";
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

    static string destCommand(string line)
    {
        var destIndex = line.IndexOf('=');
        if (destIndex != -1)
        {
            return destBits[line.Substring(0, destIndex)];
        }
        return "000";
    }

    static string compCommand(string line)
    {
        var destIndex = line.IndexOf('=');
        if (destIndex != -1)
        {
            line = line.Substring(destIndex + 1);
        }
        var jmpIndex = line.IndexOf(';');
        if (jmpIndex != -1)
        {
            line = line.Substring(0, jmpIndex);
        }
        return compBits[line];
    }

    static string jumpCommand(string line)
    {
        var jmpIndex = line.IndexOf(';');
        if (jmpIndex != -1)
        {
            return jumpBits[line.Substring(jmpIndex + 1)];
        }
        return "000";
    }
}
