using System.Xml.Serialization;

namespace T1.GiftComponents.ToyComponent
{
    public class ToyComponent : GiftItemComponent
    {
        [XmlElement(ElementName = "Composition")]
        public Composition Composition{ get; set; }

        public ToyComponent() : base()
        {
            
        }
        public ToyComponent(string name, string manufacturer, float weight, Composition composition, PriceType priceType,
            PriceRoundingRule priceRoundingRule) :
            base(name, manufacturer, weight, priceType, priceRoundingRule)
        {
            Name = name;
            Manufacturer = manufacturer;
            Weight = weight;
            Composition = composition;
            this.priceType = priceType;
            this.priceRoundingRule = priceRoundingRule;
        }
    }
}