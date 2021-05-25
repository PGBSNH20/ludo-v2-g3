using Backend.Enum;

namespace Backend.Model
{
    public interface IPawn
    {
        int ID { get; set; }
        int Position { get; set; }
        bool AtFinishLine { get; set; }
        bool IsFinished { get; set; }
        bool IsInNest { get; set; }
        PawnColor Color { get; set; }
        int PlayerId { get; set; }
        Pawn Create();
    }
}