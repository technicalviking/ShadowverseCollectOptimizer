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
    public class Player : IPlayer
    {
        public int curVials;
        public int totalVials;

        public int maxVials;

        public Dictionary<string, int> numPacks { get; set; }

        public Dictionary<string, Collection> cardSets;

        public ICollectionCompletionStrategy compStrat;

        public string name { get; set; }

        public CraftingStrategy craftStrat;

        public Player(ICollectionCompletionStrategy ccs, IRarityStrategy rs, IDupeStrategy ds ) {
            compStrat = ccs;

            craftStrat = new CraftingStrategy(rs, ds);

            string rarityStrat = rs.GetType().ToString().Split('.').Last();
            string dupeStrat = ds.GetType().ToString().Split('.').Last();
            string completenessStrat = ccs.ToString().Split('.').Last();
            name = string.Format("{0}_{1}_{2}", rarityStrat, dupeStrat, completenessStrat);
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

            cardSets.Add(targetSet.NAME, newCollection);
            numPacks.Add(targetSet.NAME, 0);

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
                if(totalVials > maxVials)
                {
                    throw new Exception("MAX VIAL LIMIT REACHED, HOW IS TEST NOT DONE");
                }
                Collection curCollection = cardSets.Values.First(col => !col.IsFull());

                BoosterPack newBooster = Shop.buyPack(curCollection.curSet, numPacks[curCollection.curSet.NAME]++);

                curCollection.AddBooster(newBooster);

                int newVials = 0;

                newVials += compStrat.GetVials(curCollection);

                curVials += newVials;
                totalVials += newVials;

                craftStrat.CraftCards(curCollection, ref curVials);

            } while (!CollectionComplete());

        }

        private bool CollectionComplete()
        {
            return cardSets.Values.All(col => col.IsFull());
            
        }
    }
}
