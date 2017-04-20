namespace Connect4.Domain.Interfaces
{
    public interface IField
    {
        int PlayerId { get; set; }
        int Row { get; set; }
        int Column { get; set; }
    }
}