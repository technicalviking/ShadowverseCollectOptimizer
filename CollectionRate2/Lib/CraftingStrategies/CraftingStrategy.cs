using CollectionRate2.Lib.CraftingStrategies.DupeStrategies;
using CollectionRate2.Lib.CraftingStrategies.RarityStrategies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollectionRate2.Lib.CraftingStrategies
{
    public class CraftingStrategy
    {
        IRarityStrategy rarityStrat;
        IDupeStrategy dupeStrat;

        public CraftingStrategy(IRarityStrategy rs, IDupeStrategy ds)
        {
            rarityStrat = rs;
            dupeStrat = ds;
        }

        public void CraftCards(Collection curCollection, ref int vials)
        {
            List<Rarities> prioritySet = rarityStrat.GetRarityPriority(curCollection);

            for (int pIndex = 0; pIndex < prioritySet.Count(); pIndex++)
            {
                Rarities r = prioritySet[pIndex];
                int targetIndex = -1;
                do
                {
                    targetIndex = dupeStrat.GetPriorityIndex(curCollection, r);
                    if (targetIndex != -1 && vials >= Shop.craftingCosts[r])
                    {
                        curCollection.Cards[r][targetIndex]++;
                        vials -= Shop.craftingCosts[r];
                    }
                } while (vials >= Shop.craftingCosts[r] && targetIndex != -1);
            }

        }
    }
}
