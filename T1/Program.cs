using System;
using System.Collections.Generic;
using System.Linq;
using T1.Enums;
using T1.GiftComponents.EatableComponent;
using T1.GiftComponents.ToyComponent;

namespace T1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Gift gift = MakeGift();

            //Hans, bring sortierung
            var sortedFood =
                from i in gift.Components
                where i is Food
                orderby i.GetPrice()
                select i;

            float minSweetness = 0;
            float maxSweetness = 0.5f;
            
            //Hans, bring zuckerkrankheit
            var sweetnessRangeFood =
                from i in sortedFood
                where ((Food) i).GetSweetnessSum() <= maxSweetness
                      && ((Food) i).GetSweetnessSum() >= minSweetness
                select i;
            
        }

        static Gift MakeGift()
        {
            Gift gift = new Gift();
            FoodComponent sourCandy =
                new FoodComponent("Sour Candy XXL", "Candy Unicorn Company", 0.012f,
                    new Taste(0.25f, 0.75f, 0.00f, 0.00f),
                    PriceType.PerKilo, PriceRoundingRule.Mathematical, 25);
            FoodComponent sweetCandy =
                new FoodComponent("Super Sweet Shi(r)t", "Default Sweets Company", 0.012f,
                    new Taste(0.90f, 0.10f, 0.00f, 0.00f),
                    PriceType.PerKilo, PriceRoundingRule.Mathematical, 20);
            FoodComponent chocolate = new FoodComponent("Milk Chocolate", "Maestro Pereigral & Unichtozhil INC.", 0.1f,
                new Taste(0.85f, 0.00f, 0.05f, 0.10f), PriceType.PerKilo,
                PriceRoundingRule.Mathematical, 45);
            FoodComponent foodComponent = new FoodComponent("Five Two Six Ref", "Synthetic Eye", 1.0f,                                                                                                                                          //lol not using this
                new Taste(0.25f, 0.25f, 0.25f, 0.25f), PriceType.PerOne,
                PriceRoundingRule.Mathematical, 52.6f);

            ToyComponent plastic = new ToyComponent("Bio Resin", "Google", 200.0f,
                new Composition(1.0f, 0.0f, 0.0f, 0.0f), PriceType.PerKilo, PriceRoundingRule.Mathematical, 200.0f);
            ToyComponent smartness = new ToyComponent("Smartness", "Big Brain Pigeon", 200.0f,
                new Composition(0.0f, 0.0f, 1.0f, 0.0f), PriceType.PerKilo, PriceRoundingRule.Mathematical, 9999.99f);

            Food sourSweet = new Food("Sour Sweet", "SOUR MAN INC.", sourCandy);
            Food chocolateBar = new Food("Chocolate Bar", "Maestro Pereigral & Unichtozhil INC.", chocolate);

            Toy toyeca = new Toy("Toyeca", "Toy-Volt", plastic);
            Toy pigeon = new Toy("Smart Pigeon", "Urururu URC.", smartness);

            gift.Add(sourSweet);
            gift.Add(chocolateBar);
            gift.Add(toyeca);
            gift.Add(pigeon);


            return gift;
        }
    }
}