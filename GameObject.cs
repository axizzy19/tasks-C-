using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1
{
    internal class GameObject
    {
        public int X;
        public int Y;
        public int previousX;
        public int previousY;
        protected int StringLength = 1;
        protected int StringsAmount = 1;
        public bool IsDead { get; set; } = false;

        public virtual void Update()
        {

        }

        public virtual void Draw()
        {
            if (previousX != X || previousY != Y)
            {
                for (int i = 0; i < StringsAmount; i++)
                {
                    Console.SetCursorPosition(previousX, previousY + i);
                    Console.Write(new string(' ', StringLength));
                }
                previousX = X;
                previousY = Y;
            }
        }

        public virtual void Erase()
        {
            for (int i = 0; i < StringsAmount; i++)
            {
                Console.SetCursorPosition(X, previousY + i);
                Console.Write(new string(' ', StringLength + 1));
            }
        }
    }
}
