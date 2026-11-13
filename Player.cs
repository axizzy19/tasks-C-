using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1
{
    internal class Player : GameObject
    {
        public bool dropFood { get; set; } = false;
        private string arm = "XXX";
        public Player() : base()
        {
            StringLength = arm.Length;
            StringsAmount = 1;
        }
        public void MoveLeft()
        {
            if (X >=0)
            {
                X--;
            }
        }

        public void MoveRight()
        {
            if (X < Console.WindowWidth - 1)
            {
                X++;
            }
        }

        public void DropFood()
        {
            dropFood = true;
        }

        public override void Draw()
        {
            base.Draw();
            Console.SetCursorPosition((int)Math.Round(X), (int)Math.Round(Y));
            Console.Write(arm);
        }
    }
}
