using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1
{
    internal class HealthBar : GameObject
    {
        private Fish fish;
        public HealthBar(Fish fish)
        {
            this.fish = fish;
            StringLength = 12;
            StringsAmount = 1;
        }
        public override void Draw()
        {
            X = fish.X + fish.fishLength - StringLength;
            Y = fish.Y-1;

            base.Draw();

            int totalBlocks = 10;
            int currentBlocks = (int)(fish.health / fish.maxHealth * totalBlocks);
            string bar = new string('#', currentBlocks) + new string('-', totalBlocks - currentBlocks);

            Console.SetCursorPosition(X, Y);
            Console.Write($"[{bar}]");
        }
    }
}
