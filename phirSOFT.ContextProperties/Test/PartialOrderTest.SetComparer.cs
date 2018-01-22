using System.Collections.Generic;

namespace phirSOFT.ContextProperties.Tests
{
    public partial class PartialOrderTest
    {
        private class SetComparer : Comparer<RelationalSet>
        {
            public override int Compare(RelationalSet x, RelationalSet y)
            {
                return y != null && x != null && x.SetEquals(y) ? 0 : -1;
            }
        }
    }
}
