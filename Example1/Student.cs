using dotSpace.BaseClasses;
using dotSpace.Interfaces;
using System;

namespace Example1
{
    public class Student : AgentBase
    {
        public Student(string name, ISpace space) : base(name, space)
        {
        }

        protected override void DoWork()
        {
            ITuple tuple = this.ts.Get(typeof(string));
            Console.WriteLine(tuple[0]);
            this.ts.Put(this.name, "02148");
        }
    }
}
