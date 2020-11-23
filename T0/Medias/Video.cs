using System;

namespace T0.Medias
{
    public class Video : Media
    {
        public Resolution Resolution { get; set; }

        public int Bitrate { get; set; }

        public Video(string name, Resolution resolution, int bitrate) : base(name)
        {
            Resolution = resolution;
            Bitrate = bitrate;
        }

        public override void Play()
        {
            Console.WriteLine($"Playing video {Name} with resolution {Resolution} and bitrate {Bitrate}");
        }
    }
}