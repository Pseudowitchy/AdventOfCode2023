
using System.Text.RegularExpressions;

int totalPower = 0;

int gameCount = File.ReadLines("Input.txt").Count();

for (int i = 0; i < gameCount; i++)
{
    string content = File.ReadLines("Input.txt").Skip(i).Take(1).First();
    string trimmedContent = content.Remove(0, content.IndexOf(':') + 2);
    string[] games = trimmedContent.Split(';');

    int redMin = 0;
    int blueMin = 0;
    int greenMin = 0;

    foreach (string game in games)
    {
        string[] colors = game.Split(',');
        foreach (string color in colors)
        {
            int value = int.Parse(Regex.Match(color, @"\d+").Value);
            if (color.EndsWith(" red"))
            {
                if (value > redMin) { redMin = value; }
            }
            if (color.EndsWith(" blue"))
            {
                if (value > blueMin) { blueMin = value; }
            }
            if (color.EndsWith(" green"))
            {
                if (value > greenMin) { greenMin = value; }
            }
        }
    }
    int power = redMin * blueMin * greenMin;
    totalPower += power;
}

Console.WriteLine($"The total power required to make the games possible is: {totalPower}");