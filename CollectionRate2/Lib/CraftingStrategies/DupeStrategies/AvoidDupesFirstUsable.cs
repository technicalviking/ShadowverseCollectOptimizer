using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollectionRate2.Lib.CraftingStrategies.DupeStrategies
{
    public class AvoidDupesFirstUsable : IDupeStrategy
    {
        public int GetPriorityIndex(Collection curCollection, Rarities targetRarity)
        {
            int rarityIndex = curCollection.Cards.Keys.ToList().IndexOf(targetRarity);
            Rarities animRarity = curCollection.Cards.Keys.ElementAt(rarityIndex + 1);

            int[] cardList = curCollection.Cards[targetRarity];
            int[] animList = curCollection.Cards[animRarity];

            int targetIndex = -1;
            for (int i = 0; i < cardList.Length; i++)
            {
                if (cardList[i] + animList[i] < 2)
                {
                    targetIndex = i;
                    break;
                }
                else if (targetIndex == -1 && cardList[i] + animList[i] < 3)
                {
                    targetIndex = i;
                }
            }

            return targetIndex;
        }
    }
}
