namespace RpslsGame.GameService.Choices;

public class Choice
{
    public static readonly Choice Rock = new(0, "Rock");
    public static readonly Choice Paper = new(1, "Paper");
    public static readonly Choice Scissors = new(2, "Scissors");
    public static readonly Choice Lizard = new(3, "Lizard");
    public static readonly Choice Spock = new(4, "Spock");

    private static readonly Choice[] Choices = [Rock, Paper, Scissors, Lizard, Spock];

    static Choice()
    {
        Rock.BeatsChoices = [Scissors, Lizard];
        Paper.BeatsChoices = [Spock, Rock];
        Scissors.BeatsChoices = [Lizard, Paper];
        Lizard.BeatsChoices = [Spock, Paper];
        Spock.BeatsChoices = [Scissors, Rock];
    }

    public int Id { get; init; }
    public string Name { get; init; }
    public Choice[] BeatsChoices { get; private set; } = [];

    private Choice(int id, string name)
    {
        Id = id;
        Name = name;
    }

    public static Choice CreateChoice(int id)
    {
        return Choices[id];
    }

    public static IEnumerable<Choice> ListChoices()
    {
        return Choices;
    }

    public bool Beats(Choice choice)
    {
        return BeatsChoices.Contains(choice);
    }
}
