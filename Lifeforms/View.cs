using dotSpace.BaseClasses;
using dotSpace.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Lifeforms
{
    public class View : AgentBase
    {
        private char[,] screenBuffer;
        private readonly int width;
        private readonly int height;
        private ConsoleColor currentColor;
        private int numberLifeforms;
        private int maxGenerations;
        private int maxLife;
        private int avgLife;
        private int numberFoods;
        private int maxVisualRange;

        public View(int width, int height, ISpace ts) : base("view", ts)
        {
            this.width = width;
            this.height = height;
            this.screenBuffer = new char[this.width, this.height];
            Console.CursorVisible = false;
        }

        protected override void DoWork()
        {
            // Wait until we can start
            this.ts.Query("start");

            // Keep iterating while the state is 'running'
            while (this.ts.Query("running", true) != null)
            {
                this.ShowLifeforms();
                this.ShowFood();
                this.Show();
                Thread.Sleep(40);
            }
        }

        private void ShowLifeforms()
        {
            IEnumerable<ITuple> lifeforms = this.ts.QueryAll("lifeform", typeof(long), typeof(long), typeof(long), typeof(int), typeof(int), typeof(int), typeof(int), typeof(int));
            this.maxGenerations = 0;
            this.avgLife = 0;
            this.maxVisualRange = 0;
            foreach (ITuple lifeform in lifeforms)
            {
                long genom = (long)lifeform[1];
                int x = (int)lifeform[5];
                int y = (int)lifeform[6];
                this.screenBuffer[x, y] = '¤'; // (char)((genom % 27) + 96);
                this.maxGenerations = Math.Max(this.maxGenerations, (int)lifeform[7]);
                this.maxLife = Math.Max(this.maxLife, (int)lifeform[4]);
                this.maxVisualRange = Math.Max(this.maxVisualRange, (int)lifeform[8]);
                this.avgLife += (int)lifeform[4];
            }
            this.numberLifeforms = lifeforms.Count();
            if (this.numberLifeforms > 0)
            {
                this.avgLife = this.avgLife / this.numberLifeforms;
            }
        }

        private void ShowFood()
        {
            IEnumerable<ITuple> foods = this.ts.QueryAll("food", typeof(int), typeof(int), typeof(int), typeof(int));
            foreach (ITuple food in foods)
            {
                int x = (int)food[3];
                int y = (int)food[4];
                this.screenBuffer[x, y] = '@';
            }
            this.numberFoods = foods.Count();
        }

        private string ShowInfo()
        {
            return string.Format(" Number lifeforms: {0,-4} - Max generations count: {1,-4} - Number foods: {2,-4}\n Max life: {3,-4} - Average life: {4,-4} - Max visual range: {5,-4}", this.numberLifeforms, this.maxGenerations, this.numberFoods, this.maxLife, this.avgLife, this.maxVisualRange);
        }

        private void SetForegroundColor(char c)
        {
            ConsoleColor color;

            if (c == '@') color = ConsoleColor.DarkGreen;
            else if (c == '_') color = ConsoleColor.White;
            else if (c == '│') color = ConsoleColor.White;
            else color = ConsoleColor.Magenta;

            if (currentColor != color)
            {
                Console.ForegroundColor = color;
                currentColor = color;
            }
        }
        private void Show()
        {
            for (int y = 0; y < this.height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    Console.SetCursorPosition(x, y + 1);
                    this.SetForegroundColor(screenBuffer[x, y]);
                    Console.Write(screenBuffer[x, y]);
                }
            }
            Console.SetCursorPosition(0, this.height+1);
            Console.Write(this.ShowInfo());
            for (int y = 0; y < this.height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    screenBuffer[x, y] = ' ';
                    if (y == 0 || y == this.height - 1)
                    {
                        screenBuffer[x, y] = '_';
                    }
                    if (x == 0 || x == this.width - 1)
                    {
                        screenBuffer[x, y] = '│';
                    }
                }
            }
        }
    }
}
