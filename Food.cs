using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1
{
    internal class Food : GameObject
    {
        private int speed = 1;
        private string food = "*";

        public Food() : base()
        {
            StringLength = food.Length;
            StringsAmount = 1;
        }

        public override void Update(double deltaTime)
        {
            Y += speed;
            if (Y >= Console.WindowHeight - 1)
            {
                Erase();
            }
        }
        public override void Draw()
        {
            base.Draw();
            Console.SetCursorPosition((int)Math.Round(X), (int)Math.Round(Y));
            Console.Write(food);
        }

        public override void Erase()
        {
            base.Erase();
            IsDead = true;
        }
    }
}
