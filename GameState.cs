using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1
{
    public class FishState
    {
        public double X;
        public double Y;
        public int Direction;
        public double Health;
        public double MaxHealth;
    }

    public class FoodState
    {
        public double X;
        public double Y;
    }
    public class PlayerState
    {
        public double X;
        public double Y;
    }
    public class GameState
    {
        public List<FishState> Fishes = new List<FishState>();
        public List<FoodState> Foods = new List<FoodState>();
        public PlayerState Player = new PlayerState();
    }
}
