using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace ConsoleApp1
{
    internal class Game
    {
        private Player player = new Player();
        private List<Food> foods = new List<Food>();
        private List<Fish> fishes = new List<Fish>();
        private object locked = new object();

        private void Clean()
        {
            foreach (var fish in fishes)
            {
                if (fish.IsDead) fish.Erase();
            }
            fishes.RemoveAll(f => f.IsDead);

            foreach (var food in foods)
            {
                if (food.IsDead) food.Erase();
            }
            foods.RemoveAll(f => f.IsDead);
        }
        public void UpdateLoop() {
            while (true)
            {
                lock (locked)
                {
                    
                    foreach (var fish in fishes)
                    {
                        fish.Update();
                    }

                    foreach(var food in foods)
                    {
                        food.Update();

                        foreach (var fish in fishes)
                        {
                            if (food.X >= fish.X && food.X <= fish.X + fish.fishLength &&
                            food.Y >= fish.Y && food.Y <= fish.Y + fish.fishHeight)
                            {
                                food.IsDead = true;
                                fish.health += 10;

                                if (fish.health > fish.maxHealth)
                                {
                                    fish.health = fish.maxHealth;
                                }
                                break;
                            }
                        }
                        
                    }

                    if (player.dropFood)
                    {
                        var newFood = new Food();
                        newFood.X = player.X;
                        newFood.Y = player.Y + 1;
                        foods.Add(newFood);
                        player.dropFood = false;
                    }

                    Clean();
                }
                
                Thread.Sleep(100);
            }
        }
        public void DrawLoop() {
            while (true)
            {
                lock (locked)
                {
                    foreach(var fish in fishes)
                    {
                        fish.Draw();
                    }
                    
                    player.Draw();
                    foreach (var food in foods)
                    {
                        food.Draw();
                    }
                }
                Thread.Sleep(100);
            }
        }

        public void HandleInput(ConsoleKeyInfo key)
        {
           
            lock (locked)
            {
                if (key.Key == ConsoleKey.LeftArrow)
                {
                    player.MoveLeft();
                }
                if (key.Key == ConsoleKey.RightArrow)
                {
                    player.MoveRight();
                }
                if (key.Key == ConsoleKey.Spacebar)
                {
                    player.DropFood();
                }
            }
                
        }

        public void Start() {
            Fish fish1 = new Fish();
            fish1.X = 30;
            fish1.Y = 4;
            fishes.Add(fish1);

            Fish fish2 = new Fish();
            fish2.X = 0;
            fish2.Y = 10;
            fishes.Add(fish2);


            player.X = 2;
            player.Y = 1;

            Thread updateThread = new Thread(UpdateLoop);
            Thread drawThread = new Thread(DrawLoop);
            updateThread.Start();
            drawThread.Start();
        }    

    }
}
