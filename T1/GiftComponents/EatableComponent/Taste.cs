using System.Text;


namespace T1.GiftComponents.EatableComponent
{
    public class Taste
    {
        public Taste(float sweetness, float sourness, float bitterness, float salinity)
        {
            Sweetness = sweetness;
            Sourness = sourness;
            Bitterness = bitterness;
            Salinity = salinity;
        }

       

        public float Sweetness { get; set; }

        public float Sourness { get; set; }


        public float Bitterness { get; set; }

        public float Salinity { get; set; }


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