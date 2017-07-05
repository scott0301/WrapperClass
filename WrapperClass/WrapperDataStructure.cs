using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WrapperUnion
{
    public static class WrapperDataStructure
    {
        /// <summary>
        /// Double List Sorting by value which has indexes
        /// </summary>
        /// <param name="list"></param>
        /// <param name="bAscending"></param>
        /// <returns></returns>
        public static List<int> GetSortedIndex(List<double> list, bool bAscending = true)
        {
            int nResCount = list.Count;

            List<KeyValuePair<int, double>> kvp = new List<KeyValuePair<int, double>>();

            for (int i = 0; i < nResCount; i++)
            {
                KeyValuePair<int, double> single = new KeyValuePair<int, double>(i, list.ElementAt(i));

                kvp.Add(single);
            }

            if (bAscending == true)
            {
                kvp.Sort(delegate(KeyValuePair<int, double> x, KeyValuePair<int, double> y) { return y.Value.CompareTo(x.Value); });
            }
            else if (bAscending == false)
            {
                kvp.Sort(delegate(KeyValuePair<int, double> x, KeyValuePair<int, double> y) { return x.Value.CompareTo(y.Value); });
            }
            

            List<int> listSorted = new List<int>();

            for (int i = 0; i < kvp.Count; i++)
            {
                listSorted.Add(kvp.ElementAt(i).Key);
            }
            return listSorted;

        }
    }
}
