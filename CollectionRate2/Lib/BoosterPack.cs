using CollectionRate2.Lib.SetBreakdowns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollectionRate2
{
    public class FirstSevenRarity
    {

        public int Bronze
        {
            private set { }
            get { return 6750; }
        }

        public int Silver
        {
            private set { }
            get { return 9250; }
        }

        public int Gold
        {
            private set { }
            get { return 9850; }
        }

        public int Legendary
        {
            private set { }
            get { return 10000; }
        }
    }

    public class EighthCardRarity
    {
        public int Silver
        {
            private set { }
            get { return 9250; }
        }

        public int Gold
        {
            private set { }
            get { return 9850; }
        }

        public int Legendary
        {
            private set { }
            get { return 10000; }
        }
    }

    public class BoosterPack
    {
        public Rarities[] cardList = new Rarities[8];
        public int[] indexes = new int[8];
        public IBreakdown curSet;
        const int MINRAND = 0;
        const int MAXRAND = 10000;

        public FirstSevenRarity firstSeven = new FirstSevenRarity();
        public EighthCardRarity eighthCard = new EighthCardRarity();

        public BoosterPack(IBreakdown set)
        {
            curSet = set;
            FillPack();
            CheckAnimated();
        }

        private void FillPack()
        {
            for (int counter = 0; counter < cardList.Length; counter++)
            {
                int random = Shop.rng.Next(MINRAND, MAXRAND);
                if (counter < cardList.Length - 1)
                {
                    if (random > firstSeven.Bronze)
                    {
                        if (random > firstSeven.Silver)
                        {
                            if (random > firstSeven.Gold)
                            {
                                cardList[counter] = Rarities.Legendary;
                                indexes[counter] = Shop.rng.Next(0, curSet.LEGENDARY);
                            }
                            else
                            {
                                cardList[counter] = Rarities.Gold;
                                indexes[counter] = Shop.rng.Next(0, curSet.GOLD);
                            }
                        }
                        else
                        {
                            cardList[counter] = Rarities.Silver;
                            indexes[counter] = Shop.rng.Next(0, curSet.SILVER);
                        }
                    }
                    else
                    {
                        cardList[counter] = Rarities.Bronze;
                        indexes[counter] = Shop.rng.Next(0, curSet.BRONZE);
                    }
                }
                else
                {
                    if (random > eighthCard.Silver)
                    {
                        if (random > eighthCard.Gold)
                        {
                            cardList[counter] = Rarities.Legendary;
                            indexes[counter] = Shop.rng.Next(0, curSet.LEGENDARY);
                        }
                        else
                        {
                            cardList[counter] = Rarities.Gold;
                            indexes[counter] = Shop.rng.Next(0, curSet.GOLD);
                        }
                    }
                    else
                    {
                        cardList[counter] = Rarities.Silver;
                        indexes[counter] = Shop.rng.Next(0, curSet.SILVER);
                    }
                }
            }
        }

        private void CheckAnimated()
        {
            for (int counter = 0; counter < cardList.Length; counter++)
            {
                var animatedRandom = Shop.rng.Next(0, 100);

                if (animatedRandom >= 92)
                {
                    switch (cardList[counter])
                    {
                        case Rarities.Bronze:
                            cardList[counter] = Rarities.BronzeAnimated;
                            break;
                        case Rarities.Silver:
                            cardList[counter] = Rarities.SilverAnimated;
                            break;
                        case Rarities.Gold:
                            cardList[counter] = Rarities.GoldAnimated;
                            break;
                        case Rarities.Legendary:
                            cardList[counter] = Rarities.LegendaryAnimated;
                            break;
                    }
                }
            }
        }
    }
}
