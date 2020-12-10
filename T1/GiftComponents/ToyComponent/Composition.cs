namespace T1.GiftComponents.ToyComponent
{
    public class Composition
    {
        
        public Composition()
        {
        }

        public Composition(float plastic, float paper, float glass, float wood)
        {
            Plastic = plastic;
            Paper = paper;
            Glass = glass;
            Wood = wood;
        }
        public float Plastic { get; set; }
        public float Paper { get; set; }
        public float Glass { get; set; }
        public float Wood { get; set; }

        
    }
}