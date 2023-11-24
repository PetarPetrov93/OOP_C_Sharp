using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleSnake.GameObjects.Food
{
    public abstract class Food : Point
    {
        private char foodSymbol;
        private Wall wall;
        Random randomPosition;
        protected Food(Wall wall, char foodSymbol, int points) : base(wall.LeftX, wall.TopY)
        {
            this.wall = wall;
            FoodPoints = points;
            this.foodSymbol = foodSymbol;
            randomPosition = new Random();
        }

        public int FoodPoints { get; private set; }

        public void SetRandomPosition(Queue<Point> snakeElements)
        {

            LeftX = randomPosition.Next(2, wall.LeftX - 2);
            TopY = randomPosition.Next(2, wall.TopY - 2);

            bool isPointOfSnake = snakeElements.Any(e => e.LeftX == this.LeftX && e.TopY == this.TopY);

            while (isPointOfSnake)
            {
                LeftX = randomPosition.Next(2, wall.LeftX - 2);
                LeftX = randomPosition.Next(2, wall.TopY - 2);

                isPointOfSnake = snakeElements.Any(e => e.LeftX == this.LeftX && e.TopY == this.TopY);
            }

            Console.BackgroundColor = ConsoleColor.Red;
            Draw(foodSymbol);
            Console.BackgroundColor= ConsoleColor.White;
        }

        public bool IsFoodpoint(Point snake) => snake.TopY == TopY && snake.LeftX == LeftX;
    }
}
