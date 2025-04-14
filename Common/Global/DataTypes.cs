using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemnantOfTheAncientsMod.Common.Global
{
    public class CustomDataTypes
    {
        public struct Chain<T>
        {
            T Value { get; set; }
            T Next { get; set; }
            T Previous { get; set; }

            public Chain(T value, T next, T previous)
            {
                Value = value;
                Next = next;
                Previous = previous;
            }

        }

        public class Node<T>
        {
            public T Value;
            public Node<T> Next;
            public Node<T> Previous;
        }
    }
}
