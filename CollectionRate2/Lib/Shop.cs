using CollectionRate2.Lib.SetBreakdowns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollectionRate2
{
    class Shop
    {
        public static Random rng = new Random();
        protected static Dictionary<string, List<BoosterPack>> packHistory;

        public static Dictionary<Rarities, int> craftingCosts = new Dictionary<Rarities, int>()
        {
            {Rarities.Bronze, 50},
            {Rarities.Silver, 200},
            {Rarities.Gold, 800},
            {Rarities.Legendary, 3500}
        };

        public static void resetPackHistory()
        {
            packHistory = new Dictionary<string, List<BoosterPack>>();
        }

        public static BoosterPack buyPack(IBreakdown targetSet, int index)
        {
            if (!packHistory.ContainsKey(targetSet.NAME)){
                packHistory.Add(targetSet.NAME, new List<BoosterPack>());
            }

            if(packHistory[targetSet.NAME].Count() - 1 < index)
            {
                for(int counter = packHistory[targetSet.NAME].Count(); counter <= index; counter++)
                {
                    packHistory[targetSet.NAME].Add(new BoosterPack(targetSet));
                }
            }

            return packHistory[targetSet.NAME][index];
        }
    }
}
