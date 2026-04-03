namespace TacticalCards
{
    public enum TeamType
    {
        Player = 0,
        Enemy = 1,
    }

    public enum CardType
    {
        Move = 0,
        Strike = 1,
        Guard = 2,
        Push = 3,
    }

    public enum TargetType
    {
        Self = 0,
        Tile = 1,
        EnemyUnit = 2,
    }
}
