

using T1.Enums;

namespace T1.GiftComponents.ToyComponent
{
    public class ToyComponent : GiftItemComponent
    {
        
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