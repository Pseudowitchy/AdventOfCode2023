int lineCount = File.ReadLines("Input.txt").Count();
int first, last;
int total = 0;

for (int i = 0; i < lineCount; i++)
{
    first = 0;
    last = 0;
    string content = File.ReadLines("Input.txt").Skip(i).Take(1).First().Replace("one", "o1e")
        .Replace("two", "t2o").Replace("three","t3e").Replace("four", "f4r").Replace("five", "f5e")
        .Replace("six", "s6x").Replace("seven", "s7n").Replace("eight","e8t").Replace("nine", "n9e")
        .Replace("zero", "z0o");

    for (int j = 0; j < content.Length; j++)
    {
        if (Char.IsDigit(content[j]))
        {
            first = int.Parse(content[j].ToString());
            break;
        }
    }

    for (int j = content.Length - 1; j >= 0; j--)
    {
        if (char.IsDigit(content[j]))
        {
            last = int.Parse(content[j].ToString());
            break;
        }
    }

    Console.WriteLine($"The line {i} translates to: {first + "" + last}");

    total += (first * 10) + last;
}
Console.WriteLine($"The total is: {total}");