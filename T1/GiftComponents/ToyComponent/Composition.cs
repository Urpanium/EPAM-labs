using System;
using System.Xml.Serialization;

namespace T1.GiftComponents.ToyComponent
{
    public class Composition
    {
        [XmlElement(ElementName = "Plastic")] public float Plastic { get; set; }
        [XmlElement(ElementName = "Paper")] public float Paper { get; set; }
        [XmlElement(ElementName = "Glass")] public float Glass { get; set; }
        [XmlElement(ElementName = "Wood")] public float Wood { get; set; }

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