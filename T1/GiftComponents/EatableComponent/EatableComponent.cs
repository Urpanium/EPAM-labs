using System.Text;
using System.Xml.Serialization;

namespace T1.GiftComponents.EatableComponent
{
    public class EatableComponent : GiftItemComponent
    {
        
        
        
        
        // Additional parameters
        [XmlElement(ElementName = "Taste")] public Taste Taste { get; set; }

        public EatableComponent() : base()
        {
        }

        public EatableComponent(string name, string manufacturer, float weight, Taste taste, PriceType priceType,
            PriceRoundingRule priceRoundingRule, float rawPrice) : base(name, manufacturer, weight, priceType,
            priceRoundingRule)
        {
            Name = name;
            Manufacturer = manufacturer;
            Weight = weight;
            Taste = taste;
            this.priceType = priceType;
            this.priceRoundingRule = priceRoundingRule;
            RawPrice = rawPrice;
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(
                $"{Name}, manufactured by {Manufacturer}. Weight: {Weight}.\nTaste parameters: \n{Taste}");
            return stringBuilder.ToString();
        }
    }
}