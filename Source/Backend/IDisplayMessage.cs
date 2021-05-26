namespace Backend
{
    public interface IDisplayMessage
    {
        string MustRollToMove();
        string NoGameId();
        string HasRolled();
        string SessionNotFound();
        string DbError();
        string MustRollDice();
        string PawnNotFound();
        string NoAvailablePawns();
        string PawnHasMoved();
        string OccupiedPosition();
    }
}