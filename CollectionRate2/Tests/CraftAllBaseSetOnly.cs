using CollectionRate2.Lib.CollectionCompletionStrategies;
using CollectionRate2.Lib.CraftingStrategies.DupeStrategies;
using CollectionRate2.Lib.CraftingStrategies.RarityStrategies;
using CollectionRate2.Lib.Players;
using CollectionRate2.Lib.SetBreakdowns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollectionRate2.Tests
{
    public class CraftAllBaseSetOnly
    {
        const int MAXRUNS = 1000;
        private Dictionary<string, int[]> runs = new Dictionary<string, int[]>();

        private List<ICollectionCompletionStrategy> colCompStrats = new List<ICollectionCompletionStrategy>()
        {
            new Default(),
            new FirstThreeUsable(),
            new NukeAnimated(),
        };

        private List<IDupeStrategy> craftDupeStrategies = new List<IDupeStrategy>()
        {
            new MatchThree()
        };

        private List<IRarityStrategy> craftRarityStrategies = new List<IRarityStrategy>()
        {
            new HighToLowBySet()
        };


        List<IPlayer> players = new List<IPlayer>();

        public CraftAllBaseSetOnly()
        {
            craftRarityStrategies.ForEach(crs =>
            {
                craftDupeStrategies.ForEach(cds =>
                {
                    colCompStrats.ForEach(ccs =>
                    {
                        CraftAllPlayer cap = new CraftAllPlayer(ccs, crs, cds);
                        players.Add(cap);
                        runs.Add(cap.name, new int[MAXRUNS]);

                    });
                });
            });

            RunTests();
        }

        private void RunTests()
        {

            for (var i = 0; i < MAXRUNS; i++)
            {
                Shop.resetPackHistory();
                players.ForEach(p =>
                {
                    p.InitPlayer();
                    p.AddCollection(new BaseSetBreakdown());
                    p.Collect();

                    runs[p.name][i] = p.numPacks.Values.Aggregate((a, b) => (a + b));
                });
            }

            for (int keyIndex = 0; keyIndex < runs.Keys.Count(); keyIndex++)
            {
                string curTestName = runs.Keys.ElementAt(keyIndex);
                int[] curTestRuns = runs[curTestName];

                int totalPacks = curTestRuns.Aggregate((a, b) => { return a + b; });
                int mean = totalPacks / MAXRUNS;

                Array.Sort(curTestRuns);
                int median = curTestRuns[MAXRUNS / 2];

                Console.WriteLine(string.Format("{0} TEST: {1} runs\nMean: {2}\nMedian: {3}\nMax: {4}\nMin: {5}", curTestName, MAXRUNS, mean, median, curTestRuns[MAXRUNS - 1], curTestRuns[0]));
            }
        }
    }
}
