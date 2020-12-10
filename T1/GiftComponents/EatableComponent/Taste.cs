using System;
using System.Text;


namespace T1.GiftComponents.EatableComponent
{
    public class Taste
    {
        private Taste()
        {
            
        }

        public Taste(float sweetness, float sourness, float bitterness, float salinity)
        {
            Sweetness = sweetness;
            Sourness = sourness;
            Bitterness = bitterness;
            Salinity = salinity;
            if (!Check())
                Normalize();
        }

        public void Normalize()
        {
            
            float sum = GetSum();
            if (sum > 0)
            {
                Sweetness /= sum;
                Sourness /= sum;
                Bitterness /= sum;
                Salinity /= sum;
            }
            else
            {
                //TODO: throw custom exception
                //ah fuck this
            }
        }

        public float Sweetness { get; set; }

        public float Sourness { get; set; }


        public float Bitterness { get; set; }

        public float Salinity { get; set; }

        public Taste normalized
        {
            get
            {
                Taste normalizedTaste = new Taste(Sweetness, Sourness, Bitterness, Salinity);
                normalizedTaste.Normalize();
                return normalizedTaste;
            }
        }

        public static Taste Randomize()
        {
            Taste taste = new Taste();
            Random random = new Random();
            taste.Sweetness = (float) random.NextDouble();
            taste.Sourness = (float) random.NextDouble();
            taste.Bitterness = (float) random.NextDouble();
            taste.Salinity = (float) random.NextDouble();
            taste.Normalize();
            return taste;
        }


        private bool Check()
        {
            float sum = GetSum();
            return !(sum > 1) && !(sum < 1);
        }

        private float GetSum()
        {
            return Sweetness + Sourness + Salinity + Bitterness;
        }


        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("Taste: \n");
            stringBuilder.Append(
                $"Sweetness: {Sweetness}, Sourness: {Sourness}, Bitterness: {Bitterness}, Salinity: {Salinity}.");
            return stringBuilder.ToString();
        }
    }
}