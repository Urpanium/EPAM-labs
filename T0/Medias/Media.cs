using System;

namespace T0
{
    public abstract class Media
    {
        public string Name { get; set; }

        public Media(string name)
        {
            Name = name;
        }

        public abstract void Play();
    }
}