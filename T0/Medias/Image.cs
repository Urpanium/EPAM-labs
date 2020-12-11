using System;

namespace T0.Medias
{
    public class Image : Media
    {
        public Resolution Resolution { get; set; }
        public float Compression { get; set; }

        public Image(string name, Resolution resolution, float compression) : base(name)
        {
            Resolution = resolution;
            Compression = compression;
        }
        

        public override void Play()
        {
            Console.WriteLine($"Showing image {Name} with resolution {Resolution} and compression {Compression}");
        }
    }
}