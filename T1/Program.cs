using System;
using System.IO;
using System.Xml.Serialization;
using T1.GiftComponents;
using T1.GiftComponents.EatableComponent;
using T1.Groups;

namespace T1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Gift gift = MakeGift();
            Console.WriteLine(gift);
            SerializeGift(gift);
        }

        static Gift MakeGift()
        {
            
            //================EATABLES================\\
            EatableComponent sourCandy =
                new EatableComponent("Sour Candy XXL", "Candy Unicorn Company", 0.012f,
                    new Taste(0.25f, 0.75f, 0.00f, 0.00f),
                    GiftItemComponent.PriceType.PerKilo, GiftItemComponent.PriceRoundingRule.Mathematical, 25);
            EatableComponent sweetCandy =
                new EatableComponent("Super Sweet Shit", "Default Sweets Company", 0.012f,
                    new Taste(0.90f, 0.10f, 0.00f, 0.00f),
                    GiftItemComponent.PriceType.PerKilo, GiftItemComponent.PriceRoundingRule.Mathematical, 20);
            EatableComponent chocolate = new EatableComponent("Milk Chocolate", "Maestro Pereigral & Unichtozhil INC.", 0.1f,
                new Taste(0.85f, 0.00f, 0.05f, 0.10f), GiftItemComponent.PriceType.PerKilo,
                GiftItemComponent.PriceRoundingRule.Mathematical, 45);
            EatableComponent eatableComponent = new EatableComponent("Five Two Six Ref", "Synthetic Eye", 1.0f,
                new Taste(0.25f, 0.25f, 0.25f, 0.25f), GiftItemComponent.PriceType.PerOne,
                GiftItemComponent.PriceRoundingRule.Mathematical, 52.6f);


            Eatable sourSweet = new Eatable("Sour Sweet", "SOUR MAN INC.", sourCandy);
            Eatable chocolateBar = new Eatable("Chocolate Bar", "Maestro Pereigral & Unichtozhil INC.", chocolate);
            EatableGroup group = new EatableGroup("Sour Sweet Collection", "SOUR MAN INC.", sourSweet, 50);
            EatableGroup chocolateCollection =
                new EatableGroup("MEGA CHOCOLATE TOWER", "CHOCANIACS", chocolateBar, 120);
            //================TOYS================\\

            Gift gift = DeserializeGift();
            //gift.Add(group);
            //gift.Add(chocolateCollection);
            return gift;
        }

        static void SerializeGift(Gift gift)
        {
            if (!gift.IsEmpty())
            {
                XmlSerializer serializer = new XmlSerializer(typeof(Gift));
                FileStream fileStream =
                    new FileStream(Directory.GetCurrentDirectory() + "/database.txt", FileMode.Create);
                serializer.Serialize(fileStream, gift);
            }
        }

        static Gift DeserializeGift()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Gift));
            FileStream fileStream = new FileStream(Directory.GetCurrentDirectory() + "/database.txt", FileMode.Open);
            return (Gift) serializer.Deserialize(fileStream);
        }
    }
}