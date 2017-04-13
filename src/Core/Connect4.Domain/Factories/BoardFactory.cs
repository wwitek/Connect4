using Connect4.Domain.Interfaces.Factories;
using Connect4.Domain.Interfaces;
using Connect4.Domain.Entities;

namespace Connect4.Domain.Factories
{
    public class BoardFactory : IBoardFactory
    {
        private IFieldFactory FieldFactory { get; }

        public BoardFactory(IFieldFactory fieldFactory)
        {
            FieldFactory = fieldFactory;
        }

        public IBoard Create(int height, int width)
        {
            IField[,] fields = new IField[height, width];
            for (int i = 0; i < fields.GetLength(0); i++)
            {
                for (int j = 0; j < fields.GetLength(1); j++)
                {
                    fields[i, j] = FieldFactory.Create(i, j);
                }
            }
            return new Board(fields);
        }
    }
}