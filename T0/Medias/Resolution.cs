namespace T0.Medias
{
    public class Resolution
    {
        public float x;
        public float y;

        public Resolution(float x, float y)
        {
            this.x = x;
            this.y = y;
        }
        public override string ToString()
        {
            return x + " " + y;
        }
    }
}