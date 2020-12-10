using System;

namespace T1.GiftComponents.ToyComponent
{
    public class Composition
    {
        public float Plastic { get; set; }
        public float Paper { get; set; }
        public float Glass { get; set; }
        public float Wood { get; set; }

        public Composition normalized
        {
            get
            {
                Composition normalizedComposition = new Composition(Plastic, Paper, Glass, Wood);
                normalizedComposition.Normalize();
                return normalizedComposition;
            }
        }

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

        void Normalize()
        {
            float sum = (float) (Math.Pow(Plastic, 2) +
                                 Math.Pow(Paper, 2) +
                                 Math.Pow(Glass, 2) +
                                 Math.Pow(Wood, 2));
            Plastic /= sum;
            Paper /= sum;
            Glass /= sum;
            Wood /= sum;
        }
    }
}