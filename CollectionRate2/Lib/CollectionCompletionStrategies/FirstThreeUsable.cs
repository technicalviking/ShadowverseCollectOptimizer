
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollectionRate2.Lib.CollectionCompletionStrategies
{
    public class FirstThreeUsable : ICollectionCompletionStrategy
    {
        public int GetVials(Collection curCol)
        {
            int vials = 0;
            vials += liquifyExtra(curCol.Cards[Rarities.Bronze], curCol.Cards[Rarities.BronzeAnimated], 10, 30);
            vials += liquifyExtra(curCol.Cards[Rarities.Silver], curCol.Cards[Rarities.SilverAnimated], 50, 120);
            vials += liquifyExtra(curCol.Cards[Rarities.Gold], curCol.Cards[Rarities.GoldAnimated], 250, 600);
            vials += liquifyExtra(curCol.Cards[Rarities.Legendary], curCol.Cards[Rarities.LegendaryAnimated], 1000, 2500);

            return vials;
        }

        private int liquifyExtra(int[] standard, int[] animated, int normalVials, int animatedVials)
        {
            int vials = 0;

            for (var index = 0; index < standard.Length; index++)
            {
                for(int numCopies = animated[index] + standard[index]; numCopies > 3; numCopies--)
                {
                    if (animated[index] > 0)
                    {
                        animated[index]--;
                        vials += animatedVials;
                    }
                    else
                    {
                        standard[index]--;
                        vials += normalVials;
                    }
                }
            }
            return vials;
        }

        public void CheckFull(Collection curCol)
        {
            curCol.FullSets.Keys.ToList().ForEach(r => {
                int rarityIndex = curCol.Cards.Keys.ToList().IndexOf(r);
                Rarities animRarity = curCol.Cards.Keys.ElementAt(rarityIndex + 1);

                int[] cardList = curCol.Cards[r];
                int[] animList = curCol.Cards[animRarity];

                for (int counter = 0; counter < curCol.Cards[r].Length; counter++)
                {
                    if (cardList[counter] + animList[counter] < 3)
                    {
                        curCol.FullSets[r] = false;
                        return;
                    }
                }

                curCol.FullSets[r] = true;
            });
        }


    }
}
