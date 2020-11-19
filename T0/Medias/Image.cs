using System;
using System.Text;

namespace T0.Medias
{
    public class Image : Media
    {
        public Resolution Resolution { get; set; }
        public float Compression { get; set; }

        public Image(string name) : base(name)
        {
            
        }
        

        public new void Play()
        {
            Console.WriteLine($"Showing image {Name} with resolution {Resolution} and compression {Compression}");
        }
    }
}