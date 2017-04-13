using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connect4.Domain.Utilities
{
    internal static class Requires
    {
        internal static void IsNotNull(object instance, string paramName)
        {
            if (object.ReferenceEquals(instance, null))
            {
                throw new ArgumentNullException(paramName);
            }
        }

        internal static void IsArrayContentNotNull(Array instance, string paramName)
        {
            for (int i = 0; i < instance.GetLength(0); i++)
            {
                for (int j = 0; j < instance.GetLength(1); j++)
                {
                    object cell = instance.GetValue(i, j);
                    if (object.ReferenceEquals(cell, null))
                    {
                        throw new ArgumentNullException(paramName + $"[{ i },{ j }]");
                    }
                }
            }
        }

        internal static void IsNotNullOrEmpty(string instance, string paramName)
        {
            IsNotNull(instance, paramName);

            if (instance.Length == 0)
            {
                throw new ArgumentException("Value can not be empty.", paramName);
            }
        }
    }
}