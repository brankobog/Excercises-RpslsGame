using RpslsGame.GameService.Choices;

namespace RpslsGame.GameService.Games;

public enum GameResult
{
    Win,
    Lose,
    Tie
}

public class Game(Choice playerChoice, Choice opponentChoice)
{
    public GameResult Result => GetResult();

    private GameResult GetResult()
    {
        if (playerChoice == opponentChoice)
        {
            return GameResult.Tie;
        }

        if (playerChoice.Beats(opponentChoice))
        {
            return GameResult.Win;
        }

        return GameResult.Lose;
    }
}
