using System;

namespace T0.Medias
{
    public class Video : Media
    {
        public Resolution Resolution { get; set; }

        public int Bitrate { get; set; }

        public Video(string name) : base(name)
        {
            
        }

        public new void Play()
        {
            Console.WriteLine($"Playing video {Name} with resolution {Resolution} and bitrate {Bitrate}");
        }
    }
}