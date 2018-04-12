using System;
using System.Collections.Generic;
using System.Text;
using phirSOFT.TopologicalComparison;

namespace phirSOFT.ContextProperties
{
    public partial class ContextProperty<TValue>
    {
        private partial class ContextProviderTree
        {
            internal class ContextProviderComparer : ITopologicalComparer<IContextProvider<IContextProperty<TValue>, TValue>>
            {
                public int Compare(IContextProvider<IContextProperty<TValue>, TValue> x, IContextProvider<IContextProperty<TValue>, TValue> y)
                {
                    {
                        if (x is ITopologicalComparable<IContextProvider<IContextProperty<TValue>, TValue>> xcomp)
                            return xcomp.CompareTo(y);

                        if (y is ITopologicalComparable<IContextProvider<IContextProperty<TValue>, TValue>> ycomp)
                            return ycomp.CompareTo(x);
                    }

                    {
                        if (x is ITopologicalComparable xcomp)
                            return xcomp.CompareTo(y);

                        if (y is ITopologicalComparable ycomp)
                            return ycomp.CompareTo(x);
                    }
                   
                    {
                        if (x is IComparable<IContextProvider<IContextProperty<TValue>, TValue>> xcomp)
                            return xcomp.CompareTo(y);

                        if (y is IComparable<IContextProvider<IContextProperty<TValue>, TValue>> ycomp)
                            return ycomp.CompareTo(x);
                    }

                    {
                        if (x is IComparable xcomp)
                            return xcomp.CompareTo(y);

                        if (y is IComparable ycomp)
                            return ycomp.CompareTo(x);
                    }

                    throw new InvalidOperationException("Could not compare types");
                }

                public bool CanCompare(IContextProvider<IContextProperty<TValue>, TValue> x, IContextProvider<IContextProperty<TValue>, TValue> y)
                {
                    {
                        if (x is ITopologicalComparable<IContextProvider<IContextProperty<TValue>, TValue>> xcomp)
                            return xcomp.CanCompareTo(y);

                        if (y is ITopologicalComparable<IContextProvider<IContextProperty<TValue>, TValue>> ycomp)
                            return ycomp.CanCompareTo(x);
                    }

                    {
                        if (x is ITopologicalComparable xcomp)
                            return xcomp.CanCompareTo(y);

                        if (y is ITopologicalComparable ycomp)
                            return ycomp.CanCompareTo(x);
                    }

                    return x is IComparable<IContextProvider<IContextProperty<TValue>, TValue>> ||
                           y is IComparable<IContextProvider<IContextProperty<TValue>, TValue>> ||
                           x is IComparable ||
                           y is IComparable;
                }
            }
        }
    }
}
