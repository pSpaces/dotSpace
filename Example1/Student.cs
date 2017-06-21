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
            // Retrieve the tuple.
            ITuple tuple = this.Get(typeof(string));
            
            // Print it.
            Console.WriteLine(tuple[0]);
            
            // Create a new tuple, and put it in the tuple space.
            this.Put(this.name, "02148");
        }
    }
}
