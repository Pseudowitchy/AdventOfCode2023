using System.Text.RegularExpressions;

int redMax = 12;
int greenMax = 13;
int blueMax = 14;
int sumOfIDs = 0;

int gameCount = File.ReadLines("Input.txt").Count();

for (int i = 0; i < gameCount; i++)
{
    string content = File.ReadLines("Input.txt").Skip(i).Take(1).First();
    string trimmedContent = content.Remove(0, content.IndexOf(':') + 2);
    string[] games = trimmedContent.Split(';');

    bool isPossible = true;

    foreach (string game in games)
    {
        string[] colors = game.Split(',');
        foreach (string color in colors)
        {
            if (color.EndsWith(" red"))
            {
                if (int.Parse(Regex.Match(color, @"\d+").Value) > redMax) { isPossible = false;  break; }
            }
            if (color.EndsWith(" blue"))
            {
                if (int.Parse(Regex.Match(color, @"\d+").Value) > blueMax) { isPossible = false; break; }
            }
            if (color.EndsWith(" green"))
            {
                if (int.Parse(Regex.Match(color, @"\d+").Value) > greenMax) { isPossible = false; break; }
            }            
        }
    }
    if (isPossible == true) { sumOfIDs += GameID(content); }
}

Console.WriteLine($"The total of the possible games is: {sumOfIDs}");

static int GameID(string content)
{
    string gameIDString = content.Substring(5, 3);
    if (gameIDString.Contains(':')) { gameIDString = gameIDString.Remove(gameIDString.IndexOf(':')).Trim(); }
    int gameID = int.Parse(gameIDString);
    return gameID;
}
