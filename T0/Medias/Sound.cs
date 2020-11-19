using System;

namespace T0.Medias
{
    public class Sound : Media
    {
        public float Length { get; set; }
        public int Bitrate { get; set; }

        public Sound(string name) : base(name)
        {
            
        }

        public new void Play()
        {
            Console.WriteLine($"Playing sound {Name} with length {Length} and bitrate {Bitrate}");
        }
    }
}