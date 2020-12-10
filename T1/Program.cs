using System;
using T1.Enums;
using T1.GiftComponents.EatableComponent;

namespace T1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Gift gift = MakeGift();
            
        }

        static Gift MakeGift()
        {
            Gift gift = new Gift();
            //================EATABLES================\\
            FoodComponent sourCandy =
                new FoodComponent("Sour Candy XXL", "Candy Unicorn Company", 0.012f,
                    new Taste(0.25f, 0.75f, 0.00f, 0.00f),
                    PriceType.PerKilo, PriceRoundingRule.Mathematical, 25);
            FoodComponent sweetCandy =
                new FoodComponent("Super Sweet Shit", "Default Sweets Company", 0.012f,
                    new Taste(0.90f, 0.10f, 0.00f, 0.00f),
                    PriceType.PerKilo, PriceRoundingRule.Mathematical, 20);
            FoodComponent chocolate = new FoodComponent("Milk Chocolate", "Maestro Pereigral & Unichtozhil INC.", 0.1f,
                new Taste(0.85f, 0.00f, 0.05f, 0.10f), PriceType.PerKilo,
                PriceRoundingRule.Mathematical, 45);
            FoodComponent foodComponent = new FoodComponent("Five Two Six Ref", "Synthetic Eye", 1.0f,
                new Taste(0.25f, 0.25f, 0.25f, 0.25f), PriceType.PerOne,
                PriceRoundingRule.Mathematical, 52.6f);


            Food sourSweet = new Food("Sour Sweet", "SOUR MAN INC.", sourCandy);
            Food chocolateBar = new Food("Chocolate Bar", "Maestro Pereigral & Unichtozhil INC.", chocolate);
            return gift;
        }

        

        
    }
}