using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollectionRate2.Lib.CraftingStrategies.DupeStrategies
{
    public class AvoidDupes : IDupeStrategy
    {
        public int GetPriorityIndex(Collection curCollection, Rarities targetRarity)
        {
            int[] cardList = curCollection.Cards[targetRarity];

            int targetIndex = -1;
            for (int i = 0; i < cardList.Length; i++)
            {
                if (cardList[i] < 2)
                {
                    targetIndex = i;
                    break;
                }
                else if (targetIndex == -1 && cardList[i] < 3)
                {
                    targetIndex = i;
                }
            }

            return targetIndex;
        }
    }
}
