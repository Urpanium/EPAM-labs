using T1.Enums;

namespace T1.GiftComponents.ToyComponent
{
    public class ToyComponent : GiftItemComponent
    {
        public ToyComponent()
        {
            Composition = new Composition(0.0f, 0.0f, 0.0f, 0.0f);
        }

        public ToyComponent(string name, string manufacturer, float weight, Composition composition,
            PriceType priceType,
            PriceRoundingRule priceRoundingRule, float rawPrice) :
            base(name, manufacturer, weight, priceType, priceRoundingRule, rawPrice)
        {
            Composition = composition;
        }

        public Composition Composition { get; set; }
    }
}