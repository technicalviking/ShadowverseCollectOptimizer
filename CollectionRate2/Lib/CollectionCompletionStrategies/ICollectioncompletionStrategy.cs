using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollectionRate2.Lib.CollectionCompletionStrategies
{
    public interface ICollectionCompletionStrategy
    {
        int GetVials(Collection curCol);
        void CheckFull(Collection curCol);
    }
}
