namespace T0.Medias
{
    public class Resolution
    {
        public float X;
        public float Y;

        public Resolution(float x, float y)
        {
            this.X = x;
            this.Y = y;
        }
        public override string ToString()
        {
            return X + " " + Y;
        }
    }
}