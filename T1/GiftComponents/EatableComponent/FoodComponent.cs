using System.Text;
using T1.Enums;

namespace T1.GiftComponents.EatableComponent
{
    public class FoodComponent : GiftItemComponent
    {
        public FoodComponent()
        {
            Taste = new Taste(0.0f, 0.0f, 0.0f, 0.0f);
        }

        public FoodComponent(string name, string manufacturer, float weight, Taste taste, PriceType priceType,
            PriceRoundingRule priceRoundingRule, float rawPrice) : base(name, manufacturer, weight, priceType,
            priceRoundingRule, rawPrice)
        {
            Taste = taste;
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