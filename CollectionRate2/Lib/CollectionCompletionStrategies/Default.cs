
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollectionRate2.Lib.CollectionCompletionStrategies
{
    public class Default : ICollectionCompletionStrategy
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
            for(var index = 0; index < standard.Length; index++)
            {
                for (; standard[index] > 3; standard[index]--)
                {
                    vials += normalVials;
                }

                for (; animated[index] > 3; animated[index]--)
                {
                    vials += animatedVials;
                }
            }    
            return vials;
        }

        public void CheckFull(Collection curCol)
        {
            curCol.FullSets.Keys.ToList().ForEach(r => {
                for (int counter = 0; counter < curCol.Cards[r].Length; counter++)
                {
                    if (curCol.Cards[r][counter] < 3)
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
