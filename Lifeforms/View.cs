using dotSpace.BaseClasses;
using dotSpace.BaseClasses.Space;
using dotSpace.Interfaces;
using dotSpace.Interfaces.Space;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Lifeforms
{
    /// <summary>
    /// This class presents the lifeform game.
    /// </summary>
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

        public View(ISpace ts) : base("view", ts)
        {
            this.width = TerminalInfo.GameboardColumns;
            this.height = TerminalInfo.GameboardRows;
            this.screenBuffer = new char[this.width, this.height];
            Console.CursorVisible = false;           
            Console.SetWindowSize(this.width, this.height + 6);
        }

        protected override void DoWork()
        {
            // Wait until we can start
            this.Query(EntityType.SIGNAL, "start");

            // Keep running while the state is 'running'
            while (this.Query(EntityType.SIGNAL, "running", true) != null)
            {
                this.ShowEntities();
                this.CalculateStats();
                this.Show();
                Thread.Sleep(10);
            }
        }

        private void CalculateStats()
        {
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

            IEnumerable<LifeformStats> lifeformProperties = this.QueryAll(EntityType.LIFEFORM_STATS, typeof(string), typeof(long), typeof(long), typeof(long), typeof(int), typeof(int), typeof(int), typeof(int), typeof(int), typeof(int)).Cast<LifeformStats>();
            foreach (LifeformStats lifeformStat in lifeformProperties)
            {
                this.maxLife = Math.Max(this.maxLife, lifeformStat.InitialLife);
                this.avgLife += lifeformStat.InitialLife;
                this.minLife = Math.Min(this.minLife, lifeformStat.InitialLife);
                this.maxGenerations = Math.Max(this.maxGenerations, lifeformStat.Generation);
                this.maxVisualRange = Math.Max(this.maxVisualRange, lifeformStat.VisualRange);
                this.avgVisualRange += lifeformStat.VisualRange;
                this.minVisualRange = Math.Min(this.minVisualRange, lifeformStat.VisualRange);
                this.maxNrChildren = Math.Max(this.maxNrChildren, lifeformStat.MaxNrChildren);
                this.avgNrChildren += lifeformStat.MaxNrChildren;
                this.minNrChildren = Math.Min(this.minNrChildren, lifeformStat.MaxNrChildren);
                this.maxSpeed = Math.Max(this.maxSpeed, lifeformStat.Speed);
                this.avgSpeed += lifeformStat.Speed;
                this.minSpeed = Math.Min(this.minSpeed, lifeformStat.Speed);
            }

            if (this.numberLifeforms > 0)
            {
                this.avgLife = this.avgLife / this.numberLifeforms;
                this.avgVisualRange = this.avgVisualRange / this.numberLifeforms;
                this.avgNrChildren = this.avgNrChildren / this.numberLifeforms;
                this.avgSpeed = this.avgSpeed / this.numberLifeforms;
            }
        }

        private void ShowEntities()
        {
            IEnumerable<Position> lifeforms = this.QueryAll(EntityType.POSITION, typeof(string), typeof(int), typeof(int)).Cast<Position>();
            foreach (Position lifeform in lifeforms)
            {
                this.screenBuffer[lifeform.X, lifeform.Y] = '¤';
            }

            IEnumerable<Food> foods = this.QueryAll(EntityType.FOOD, typeof(int), typeof(int), typeof(int), typeof(int)).Cast<Food>();
            foreach (Food food in foods)
            {
                this.screenBuffer[food.X, food.Y] = '@';
            }
            this.numberLifeforms = lifeforms.Count();
            this.numberFoods = foods.Count();
        }

        private string ShowInfo()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(this.LeftAlign(string.Format("#Lifeforms: {0,-5} ", this.numberLifeforms),
                string.Format("#Generation: {0,-5} ", this.maxGenerations),
                string.Format("#Foods: {0,-5}", this.numberFoods)));

            sb.AppendLine(this.LeftAlign(string.Format("Max life: {0,-5} ", this.maxLife),
                string.Format("Average life: {0,-4} ", this.avgLife),
                string.Format("Min life: {0,-5}", this.minLife)));

            sb.AppendLine(this.LeftAlign(string.Format("Max visual range: {0,-3} ", this.maxVisualRange),
                string.Format("Average visual range: {0,-3} ", this.avgVisualRange),
                string.Format("Min visual range: {0,-3}", this.minVisualRange)));

            sb.AppendLine(this.LeftAlign(string.Format("Max #children: {0,-5}", this.maxNrChildren),
                string.Format("Average #children: {0,-5} ", this.avgNrChildren),
                string.Format("Min #children: {0,-5}", this.minNrChildren)));
            
            sb.Append(this.LeftAlign(string.Format("Max speed: {0,-5} ", this.maxSpeed),
                string.Format("Average Speed: {0,-5} ", this.avgSpeed),
                string.Format("Min speed: {0,-5}", this.minSpeed)));
            return sb.ToString();
        }

        private string LeftAlign(string text1, string text2, string text3)

        {
            return string.Format("{0,-26} {1,-26} {2,-26}", text1, text2, text3);
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
