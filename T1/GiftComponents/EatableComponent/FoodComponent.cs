using System.Text;
using T1.Enums;

namespace T1.GiftComponents.EatableComponent
{
    public class FoodComponent : GiftItemComponent
    {
        public FoodComponent() : base()
        {
            
        }

        public FoodComponent(string name, string manufacturer, float weight, Taste taste, PriceType priceType,
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

        public Taste Taste { get; set; }


        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(
                $"{Name}, manufactured by {Manufacturer}. Weight: {Weight}.\nTaste parameters: \n{Taste}");
            return stringBuilder.ToString();
        }
    }
}