using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;

namespace ConsoleApp1
{
    internal class Game
    {
        private Player player = new Player();
        private List<Food> foods = new List<Food>();
        private List<Fish> fishes = new List<Fish>();
        private int FPS = 60;
        public bool isGameOver = false;
        public double updateTotal;
        public double drawLock;
        public double drawTotal;

        public GameState LastState = new GameState();

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
            Stopwatch timeTotalUpdate = new Stopwatch();
            Stopwatch timeLockUpdate = new Stopwatch();

            Stopwatch gameTime = new Stopwatch();
            gameTime.Start();
            double previousTime = 0;

            while (true)
            {
                double currentTime = gameTime.Elapsed.TotalMilliseconds;
                double deltaTime = (double)(currentTime - previousTime) / 1000;
                previousTime = currentTime;

                timeTotalUpdate.Reset();
                lock (updateLock)
                {
                    timeLockUpdate.Restart();
                    foreach (var fish in fishes)
                    {
                        fish.Update(deltaTime);
                    }

                    foreach(var food in foods)
                    {
                        food.Update(deltaTime);

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

                    if (fishes.Count == 0)
                    {
                        isGameOver = true;
                        return;
                    }
                    timeLockUpdate.Stop();
                }
                

                timeTotalUpdate.Stop();
                updateLock = timeLockUpdate.Elapsed.TotalMilliseconds;
                updateTotal = timeTotalUpdate.Elapsed.TotalMilliseconds;
                
                Thread.Sleep(1000/FPS);
            }
        }
        public void DrawLoop() {
            Stopwatch timeTotalDraw = new Stopwatch();
            Stopwatch timeLockDraw = new Stopwatch();

            while (true)
            {
                timeTotalDraw.Restart();
                if (isGameOver) return;
                lock (locked)
                {
                    timeLockDraw.Restart();

                    foreach (var fish in fishes)
                    {
                        fish.Draw();
                    }
                    
                    player.Draw();
                    foreach (var food in foods)
                    {
                        food.Draw();
                    }

                    timeLockDraw.Stop();
                }

                timeTotalDraw.Stop();
                drawLock = timeLockDraw.Elapsed.TotalMilliseconds;
                drawTotal = timeTotalDraw.Elapsed.TotalMilliseconds;
                Console.SetCursorPosition(0, Console.WindowHeight - 2);
                Console.WriteLine($"UPDATE в локе - {updateLock}");
                Console.SetCursorPosition(0, Console.WindowHeight - 1);
                Console.WriteLine($"UPDATE всего - {updateTotal}");
                Console.SetCursorPosition(0, Console.WindowHeight - 4);
                Console.WriteLine($"DRAW в локе - {drawLock}");
                Console.SetCursorPosition(0, Console.WindowHeight - 3);
                Console.WriteLine($"DRAW всего - {drawTotal}");
                Thread.Sleep(1000 / FPS);
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
            fish2.Y = 15;
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
