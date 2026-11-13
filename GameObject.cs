using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1
{
    internal class GameObject
    {
        public double X;
        public double Y;
        public double previousX;
        public double previousY;
        protected int StringLength = 1;
        protected int StringsAmount = 1;
        public bool IsDead { get; set; } = false;
        protected double Speed = 1;

        public virtual void Update(double deltaTime)
        {

        }

        public virtual void Draw()
        {
            if (previousX != X || previousY != Y)
            {
                for (int i = 0; i < StringsAmount; i++)
                {
                    Console.SetCursorPosition((int)Math.Round(previousX), (int)Math.Round(previousY) + i);
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
                Console.SetCursorPosition((int)Math.Round(X), (int)Math.Round(previousY) + i);
                Console.Write(new string(' ', StringLength + 2));
            }
        }
    }
}
