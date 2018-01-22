using System;
using System.Collections.Generic;

namespace phirSOFT.ContextProperties.Tests
{
    public partial class PartialOrderTest
    {
        private class RelationalSet : SortedSet<int>, IComparable<RelationalSet>
        {


            public RelationalSet Clone()
            {
                var c = new RelationalSet();
                foreach (var element in this)
                {
                    c.Add(element);
                }
                return c;
            }

            public int CompareTo(RelationalSet other)
            {
                return IsSubsetOf(other) ? -1 : other.IsSubsetOf(this) ? 0 : 1;
            }




        }
    }
}
