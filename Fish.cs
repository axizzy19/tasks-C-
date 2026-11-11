using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1
{
    internal class Fish : GameObject
    {
        private int direction = -1;
        public int fishLength = 19;
        private static string[] fishArray = ["      /`·.¸        ", "     /¸...¸`:·     ", " ¸.·´  ¸   `·.¸.·´)", ": © ):´;      ¸  { ", " `·.¸ `·  ¸.·´\\`·¸) ", "     `\\\\´´\\¸.·´    "];
        public int fishHeight = fishArray.Length;
        private static string[] reversedFishArray = GetReversedFish(fishArray);
        public int maxHealth = 100;
        public double health = 100;
        private double healthDecreasedSpeed = 0.5;
        private HealthBar healthBar;

        public Fish()
        {
            StringLength = fishLength;
            StringsAmount = fishHeight;
            healthBar = new HealthBar(this);
        }

        public override void Draw()
        {
            base.Draw();
            if (direction == -1)
            {
                for (int i = 0; i < fishArray.Length; i++)
                {
                    Console.SetCursorPosition(X, Y+i);
                    Console.Write(fishArray[i]);
                }
            } else 
            {
                for (int i = 0; i < reversedFishArray.Length; i++)
                {
                    Console.SetCursorPosition(X, Y + i);
                    Console.Write(reversedFishArray[i]);
                }
            }
            healthBar.Draw();
        }

        public override void Update()
        {
            X = X + direction;
            if (X < 0)
            {
                direction = 1;
                X = 0;
            }
            if (X > Console.WindowWidth - StringLength)
            {
                direction = -1;
            }
            health -= healthDecreasedSpeed;
            if (health < 0)
            {
                IsDead = true;
            }
        }

        public override void Erase()
        {
            base.Erase();
            healthBar.Erase();
        }

        private static string[] GetReversedFish(string[] fishArray)
        { 
            return fishArray.Select(line =>
            {
                var reversed = new string(line.Reverse().ToArray());
                var mirrored = reversed.Select(c =>
                {
                    if (c == '/') return '\\';
                    if (c == '\\') return '/';
                    if (c == '(') return ')';
                    if (c == ')') return '(';
                    if (c == '{') return '}';
                    if (c == '}') return '{';
                    return c;
                }).ToArray();

                return new string(mirrored);
            }).ToArray();
        }

    }
}
