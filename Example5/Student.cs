using dotSpace.BaseClasses;
using dotSpace.Interfaces;
using System;

namespace Example5
{
    public class Student : AgentBase
    {
        public Student(string name, ISpace space) : base(name, space)
        {
        }

        protected override void DoWork()
        {
            ITuple tuple = this.Get(typeof(string));
            Console.WriteLine(tuple[0]);
            this.Put(this.name, "02148");
        }
    }
}
