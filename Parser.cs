using static System.Console;

public class Parser
{
    public static string Parse(string filename)
    {
        string output = "";
        using (StreamReader sr = File.OpenText(filename))
        {
            string currLine;
            while ((currLine = sr.ReadLine()) != null)
            {
                string[] comments = currLine.Split("//", 2);
                if (comments[0] == "") {
                    continue;
                }
                if (comments[0].First() == '@') {
                    output += AddressBinary(comments[0].Split('@')[1]) + "\n";
                    continue;
                }
                output += comments[0] + "\n";
            }
        }
        return output;
    }

    private static string AddressBinary(string val)
    {
        string binary = Convert.ToString(Convert.ToInt16(val), 2);
        return addZeroes(binary);
    }

    private static string addZeroes(string binary) {
        int toAdd = 16 - binary.Length;
        string s = "0";
        for (int i = 1; i < toAdd; i++) {
            s += "0";
        }
        return s + binary;
    }
}
