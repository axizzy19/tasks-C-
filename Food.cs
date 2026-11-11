using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1
{
    internal class Food : GameObject
    {
        private int speed = 1;
        private string food = "*";
        public bool IsDead { get; set; } = false;

        public Food() : base()
        {
            StringLength = food.Length;
            StringsAmount = 1;
        }

        public override void Update()
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
            Console.SetCursorPosition(X, Y);
            Console.Write(food);
        }

        public override void Erase()
        {
            base.Erase();
            IsDead = true;
        }
    }
}
