namespace TacticalCards
{
    public enum GameFlowState
    {
        Title = 0,
        Battle = 1,
        Result = 2,
    }

    public sealed class GameResultData
    {
        public GameResultData(string title, string summary, TeamType? winningTeam, int playerTurnCount)
        {
            Title = title;
            Summary = summary;
            WinningTeam = winningTeam;
            PlayerTurnCount = playerTurnCount;
        }

        public string Title { get; }
        public string Summary { get; }
        public TeamType? WinningTeam { get; }
        public int PlayerTurnCount { get; }
    }
}
