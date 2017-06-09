using dotSpace.BaseClasses;
using dotSpace.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        private int numberFoods;
        private int maxLife;
        private int avgLife;
        private int minLife;
        private int maxVisualRange;
        private int avgVisualRange;
        private int minVisualRange;
        private int maxNrChildren;
        private int avgNrChildren;
        private int minNrChildren;
        private int maxSpeed;
        private int avgSpeed;
        private int minSpeed;
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
                //Thread.Sleep(10);
            }
        }

        private void ShowLifeforms()
        {
            IEnumerable<ITuple> lifeforms = this.ts.QueryAll("lifeform", typeof(string), typeof(int), typeof(int));
            this.maxGenerations = 0;
            this.maxLife = 0;
            this.avgLife = 0;
            this.minLife = 10000;
            this.maxVisualRange = 0;
            this.avgVisualRange = 0;
            this.minVisualRange = 10000;
            this.maxNrChildren = 0;
            this.avgNrChildren = 0;
            this.minNrChildren = 10000;
            this.maxSpeed = 0;
            this.avgSpeed = 0;
            this.minSpeed = 10000;
            foreach (ITuple lifeform in lifeforms)
            {
                int x = (int)lifeform[2];
                int y = (int)lifeform[3];
                this.screenBuffer[x, y] = '¤';
            }
            IEnumerable<ITuple> lifeformProperties = this.ts.QueryAll("lifeformProperties", typeof(string), typeof(long), typeof(long), typeof(long), typeof(int), typeof(int), typeof(int), typeof(int), typeof(int));
            foreach (ITuple lifeformProperty in lifeformProperties)
            {
                this.maxLife = Math.Max(this.maxLife, (int)lifeformProperty[5]);
                this.avgLife += (int)lifeformProperty[5];
                this.minLife = Math.Min(this.minLife, (int)lifeformProperty[5]);
                this.maxGenerations = Math.Max(this.maxGenerations, (int)lifeformProperty[6]);
                this.maxVisualRange = Math.Max(this.maxVisualRange, (int)lifeformProperty[7]);
                this.avgVisualRange += (int)lifeformProperty[7];
                this.minVisualRange = Math.Min(this.minVisualRange, (int)lifeformProperty[7]);
                this.maxNrChildren = Math.Max(this.maxNrChildren, (int)lifeformProperty[8]);
                this.avgNrChildren += (int)lifeformProperty[8];
                this.minNrChildren = Math.Min(this.minNrChildren, (int)lifeformProperty[8]);
                this.maxSpeed = Math.Max(this.maxSpeed, (int)lifeformProperty[9]);
                this.avgSpeed += (int)lifeformProperty[9];
                this.minSpeed = Math.Min(this.minSpeed, (int)lifeformProperty[9]);
            }

            this.numberLifeforms = lifeforms.Count();
            if (this.numberLifeforms > 0)
            {
                this.avgLife = this.avgLife / this.numberLifeforms;
                this.avgVisualRange = this.avgVisualRange / this.numberLifeforms;
                this.avgNrChildren = this.avgNrChildren / this.numberLifeforms;
                this.avgSpeed = this.avgSpeed / this.numberLifeforms;
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
            StringBuilder sb = new StringBuilder();
            sb.Append(this.LeftAlign(string.Format("Number lifeforms: {0,-5} ", this.numberLifeforms),
                string.Format("Max generations count: {0,-5} ", this.maxGenerations),
                string.Format("Number foods: {0,-5}", this.numberFoods)));

            sb.Append(this.LeftAlign(string.Format("Max life: {0,-5} ", this.maxLife),
                string.Format("Average life: {0,-4} ", this.avgLife),
                string.Format("Min life: {0,-5}", this.minLife)));

            sb.Append(this.LeftAlign(string.Format("Max visual range: {0,-5} ", this.maxVisualRange),
                string.Format("Average visual range: {0,-5} ", this.avgVisualRange),
                string.Format("Min visual range: {0,-5}", this.minVisualRange)));

            sb.Append(this.LeftAlign(string.Format("Max number of children: {0,-5}", this.maxNrChildren),
                string.Format("Average number of children: {0,-5} ", this.avgNrChildren),
                string.Format("Min number of children: {0,-5}", this.minNrChildren)));

            sb.Append(this.LeftAlign(string.Format("Max speed: {0,-5} ", this.maxSpeed),
                string.Format("Average Speed: {0,-5} ", this.avgSpeed),
                string.Format("Min speed: {0,-5}", this.minSpeed)));
            return sb.ToString();
        }

        private string LeftAlign(string text1, string text2, string text3)

        {
            return string.Format("{0,-35} {1,-35} {2,-35}\n", text1, text2, text3);
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
                    Console.SetCursorPosition(x, y);
                    this.SetForegroundColor(screenBuffer[x, y]);
                    Console.Write(screenBuffer[x, y]);
                }
            }
            Console.SetCursorPosition(0, this.height);
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
