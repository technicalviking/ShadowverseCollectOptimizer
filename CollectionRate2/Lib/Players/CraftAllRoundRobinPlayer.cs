using CollectionRate2.Lib.CollectionCompletionStrategies;
using CollectionRate2.Lib.CraftingStrategies;
using CollectionRate2.Lib.CraftingStrategies.DupeStrategies;
using CollectionRate2.Lib.CraftingStrategies.RarityStrategies;
using CollectionRate2.Lib.SetBreakdowns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollectionRate2.Lib.Players
{
    public class CraftAllRoundRobinPlayer : IPlayer
    {
        public int curVials;
        public int totalVials;

        public int maxVials;
        public int neededVials;

        public Dictionary<string, int> numPacks { get; set; }

        public Dictionary<string, Collection> cardSets;

        public ICollectionCompletionStrategy compStrat;

        public string name { get; set; }

        public CraftingStrategy craftStrat;

        public CraftAllRoundRobinPlayer(ICollectionCompletionStrategy ccs, IRarityStrategy rs, IDupeStrategy ds)
        {
            compStrat = ccs;

            craftStrat = new CraftingStrategy(rs, ds);

            string rarityStrat = rs.GetType().ToString().Split('.').Last();
            string dupeStrat = ds.GetType().ToString().Split('.').Last();
            string completenessStrat = ccs.ToString().Split('.').Last();
            name = string.Format("CARRP_{0}_{1}_{2}", rarityStrat, dupeStrat, completenessStrat);
        }

        public void InitPlayer()
        {
            curVials = 0;
            totalVials = 0;
            maxVials = 0;
            numPacks = new Dictionary<string, int>();
            cardSets = new Dictionary<string, Collection>();

        }

        public void AddCollection(IBreakdown targetSet)
        {
            Collection newCollection = new Collection(targetSet, compStrat);
            newCollection.initCollection();

            int numAdd = 1;
            if(targetSet.GetType() == typeof(BaseSetBreakdown))
            {
                numAdd = 3;
            }

            for(int x = 0; x < numAdd; x++)
            {
                cardSets.Add(targetSet.NAME + x, newCollection);
                try
                {
                    numPacks.Add(targetSet.NAME, 0);
                }
                catch (Exception) { }
                
            }

            //increment maxVials to check for a bad test.
            int maxVialIncrement = 0;
            maxVialIncrement += targetSet.BRONZE * Shop.craftingCosts[Rarities.Bronze];
            maxVialIncrement += targetSet.SILVER * Shop.craftingCosts[Rarities.Silver];
            maxVialIncrement += targetSet.GOLD * Shop.craftingCosts[Rarities.Gold];
            maxVialIncrement += targetSet.LEGENDARY * Shop.craftingCosts[Rarities.Legendary];

            maxVials += (maxVialIncrement * 3);
        }

        public void Collect()
        {
            do
            {
                if (totalVials > maxVials)
                {
                    throw new Exception("MAX VIAL LIMIT REACHED, HOW IS TEST NOT DONE");
                }

                cardSets.Values.ToList().ForEach(curCollection => {

                    setNeededVials();
                    if (curVials >= neededVials)
                    {
                        return;
                    }

                    BoosterPack newBooster = Shop.buyPack(curCollection.curSet, numPacks[curCollection.curSet.NAME]++);

                    curCollection.AddBooster(newBooster);

                    int newVials = 0;

                    newVials += compStrat.GetVials(curCollection);

                    curVials += newVials;
                    totalVials += newVials;
                });

                //Collection curCollection = cardSets.Values.First(col => !col.IsFull());

                
            } while (!CollectionComplete());

        }

        private void setNeededVials()
        {
            int currentWorth = 0;
            cardSets.Values.ToList().ForEach(col =>
            {
                col.FullSets.ToList().ForEach(r =>
                {
                    foreach (var i in col.Cards[r.Key])
                    {
                        currentWorth += (i * Shop.craftingCosts[r.Key]);
                    }
                });
            });

            neededVials = maxVials - currentWorth;
        }

        private bool CollectionComplete()
        {
            return (curVials >= neededVials);
        }
    }
}
