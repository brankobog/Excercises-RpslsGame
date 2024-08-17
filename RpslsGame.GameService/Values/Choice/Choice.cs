namespace RpslsGame.GameService.Values.Choice;

public class Choice
{
    private static readonly Dictionary<int, Choice> Choices = new()
    {
        { 0, Rock },
        { 1, Paper },
        { 2, Scissors },
        { 3, Lizard },
        { 4, Spock }
    };

    public static Choice Rock => new(0, "Rock");
    public static Choice Paper => new(1, "Paper");
    public static Choice Scissors => new(2, "Scissors");
    public static Choice Lizard => new(3, "Lizard");
    public static Choice Spock => new(4, "Spock");

    public int Id { get; init; }
    public string Name { get; init; }

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
        return Choices.Values;
    }
}
