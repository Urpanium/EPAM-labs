using System;
using System.Collections.Generic;
using System.Linq;
using T1.GiftComponents;
using T1.GiftComponents.EatableComponent;

namespace T1.Gift.GIftItems
{
    public class Food : GiftItem
    {
        public new List<GiftItemComponent> Components;

        public Food()
        {
            Components = new List<GiftItemComponent>();
        }

        public Food(string name, string manufacturer, GiftItemComponent initComponent = null) : base(name, manufacturer,
            initComponent)
        {
            Components = new List<GiftItemComponent>() {initComponent};
        }

        public Food(string name, string manufacturer, IEnumerable<GiftItemComponent> initComponentList) : base(name,
            manufacturer)
        {
            if (initComponentList != null)
            {
                Components = initComponentList.ToList();
            }

            /*else
                Components = new List<GiftItemComponent>();*/
        }

        public new GiftItemComponent this[int index]
        {
            get
            {
                if (index < Components.Count && index >= 0)
                {
                    return Components[index];
                }

                throw new IndexOutOfRangeException(
                    $"Component index ({index}) must be less than {Components.Count} and greater or equal to 0.");
            }
            set
            {
                if (index < Components.Count && index >= 0)
                {
                    Components[index] = value;
                }

                throw new IndexOutOfRangeException(
                    $"Component index ({index}) must be less than {Components.Count} and greater or equal to 0.");
            }
        }

        public float GetSweetnessSum()
        {
            var sortedFoodComponents =
                from i in Components
                where i is FoodComponent
                select i;

            float sum = 0.0f;
            foreach (var component in sortedFoodComponents)
            {
                sum += ((FoodComponent) component).Taste.Sweetness;
            }

            return sum;
        }

        public float GetSalinitySum()
        {
            var sortedFoodComponents =
                from i in Components
                where i is FoodComponent
                select i;

            float sum = 0.0f;
            foreach (var component in sortedFoodComponents)
            {
                sum += ((FoodComponent) component).Taste.Salinity;
            }

            return sum;
        }

        public float GetBitternessSum()
        {
            var sortedFoodComponents =
                from i in Components
                where i is FoodComponent
                select i;

            float sum = 0.0f;
            foreach (var component in sortedFoodComponents)
            {
                sum += ((FoodComponent) component).Taste.Bitterness;
            }

            return sum;
        }

        public float GetSournessSum()
        {
            var sortedFoodComponents =
                from i in Components
                where i is FoodComponent
                select i;

            float sum = 0.0f;
            foreach (var component in sortedFoodComponents)
            {
                sum += ((FoodComponent) component).Taste.Sourness;
            }

            return sum;
        }
    }
}