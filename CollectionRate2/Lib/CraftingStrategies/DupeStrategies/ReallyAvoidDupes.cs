using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollectionRate2.Lib.CraftingStrategies.DupeStrategies
{
    public class ReallyAvoidDupes : IDupeStrategy
    {
        public int GetPriorityIndex(Collection curCollection, Rarities targetRarity)
        {
            int[] cardList = curCollection.Cards[targetRarity];

            int targetIndex = -1;
            for(int numCopies = 0; numCopies < 3; numCopies++)
            {
                for (int i = 0; i < cardList.Length; i++)
                {
                    if(cardList[i] == numCopies)
                    {
                        targetIndex = i;
                        break;
                    }
                    
                }

                if(targetIndex > -1)
                {
                    break;
                }
            }

            return targetIndex;
        }
    }
}
