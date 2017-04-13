using Connect4.Domain.Entities;
using Connect4.Domain.Interfaces;
using Connect4.Domain.Interfaces.Factories;

namespace Connect4.Domain.Factories
{
    public class FieldFactory : IFieldFactory
    {
        public IField Create(int row, int column)
        {
            return new Field(row, column);
        }
    }
}