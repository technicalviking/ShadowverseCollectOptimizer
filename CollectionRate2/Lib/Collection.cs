using CollectionRate2.Lib.CollectionCompletionStrategies;
using CollectionRate2.Lib.SetBreakdowns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace CollectionRate2
{
    public class Collection
    {
        public Dictionary<Rarities, int[]> Cards;
        public Dictionary<Rarities, bool> FullSets;

        private IBreakdown _curSet;

        public IBreakdown curSet {
            protected set { _curSet = value; }
            get { return _curSet; }
        }

        protected ICollectionCompletionStrategy colCompStrat;

        public Collection(IBreakdown bd, ICollectionCompletionStrategy ccs)
        {
            curSet = bd;
            colCompStrat = ccs;
        }

        public void initCollection()
        {
            Cards = new Dictionary<Rarities, int[]>()
            {
                { Rarities.Bronze, new int[curSet.BRONZE] },
                { Rarities.BronzeAnimated, new int[curSet.BRONZE] },
                { Rarities.Silver, new int[curSet.SILVER] },
                { Rarities.SilverAnimated, new int[curSet.SILVER] },
                { Rarities.Gold, new int[curSet.GOLD]},
                { Rarities.GoldAnimated, new int[curSet.GOLD]},
                { Rarities.Legendary, new int[curSet.LEGENDARY]},
                { Rarities.LegendaryAnimated, new int[curSet.LEGENDARY]}
            };

            FullSets = new Dictionary<Rarities, bool>()
            {
                { Rarities.Bronze, false },
                { Rarities.Silver, false },
                { Rarities.Gold, false },
                { Rarities.Legendary, false }
            };
        }
        #region AddCards
        public void AddBooster(BoosterPack pack)
        {
            for (int cardIndex = 0; cardIndex < pack.cardList.Length; cardIndex++)
            {
                addCard(pack.cardList[cardIndex], pack.indexes[cardIndex]);
            }
        }

        protected void addCard(Rarities rarity, int index)
        {
            ++Cards[rarity][index];
        }
        #endregion AddCards
        #region CheckFull

        public bool IsFull()
        {
            colCompStrat.CheckFull(this);
            return FullSets.Values.All(b=>b);
        }
#endregion
    }
}
