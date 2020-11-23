using System;

namespace T0.Medias
{
    public class Sound : Media
    {
        public float Length { get; set; }
        public int Bitrate { get; set; }

        public Sound(string name, float length, int bitrate) : base(name)
        {
            Length = length;
            Bitrate = bitrate;
        }

        public override void Play()
        {
            Console.WriteLine($"Playing sound {Name} with length {Length} and bitrate {Bitrate}");
        }
    }
}