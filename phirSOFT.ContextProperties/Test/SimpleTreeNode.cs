using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    [DebuggerDisplay("Name = {" + nameof(ToString) + "()}, Count = {" + nameof(Count) + "}")]
    class SimpleTreeNode : Collection<SimpleTreeNode>, IComparable<SimpleTreeNode>, IComparable
    {
        public SimpleTreeNode Parent;

        public static readonly SimpleTreeNode root, l, r, lr, ll, rl, rr, lll, llr, lrl, lrr, rll, rlr, rrl, rrr;

        private static readonly Dictionary<SimpleTreeNode, string> translation;

        static SimpleTreeNode()
        {
            lll = new SimpleTreeNode();
            llr = new SimpleTreeNode();
            lrl = new SimpleTreeNode();
            lrr = new SimpleTreeNode();
            rll = new SimpleTreeNode();
            rlr = new SimpleTreeNode();
            rrl = new SimpleTreeNode();
            rrr = new SimpleTreeNode();

            ll = new SimpleTreeNode() { lll, llr };
            lr = new SimpleTreeNode() { lrl, lrr };
            rl = new SimpleTreeNode() { rll, rlr };
            rr = new SimpleTreeNode() { rrl, rrr };

            l = new SimpleTreeNode() { lr, ll };
            r = new SimpleTreeNode() { rl, rr };

            root = new SimpleTreeNode() { l, r };

            translation = new Dictionary<SimpleTreeNode, string>()
                {
                    [root] = "root",
                    [l] = "l",
                    [r] = "r",

                    [ll] = "ll",
                    [rl] = "rl",
                    [lr] = "lr",
                    [rr] = "rr",

                    [lll] = "lll",
                    [rll] = "rll",
                    [lrl] = "lrl",
                    [rrl] = "rrl",

                    [llr] = "llr",
                    [rlr] = "rlr",
                    [lrr] = "lrr",
                    [rrr] = "rrr",
                }
                ;
        }

        public override string ToString()
        {
            return translation.ContainsKey(this) ? translation[this] : base.ToString();
        }

        protected override void ClearItems()
        {
            foreach (var item in this)
            {
                item.Parent = null;
            }
            base.ClearItems();
        }

        protected override void RemoveItem(int index)
        {
            this[index].Parent = null;
            base.RemoveItem(index);
        }

        protected override void SetItem(int index, SimpleTreeNode item)
        {
            item.Parent = this;
            base.SetItem(index, item);
        }

        protected override void InsertItem(int index, SimpleTreeNode item)
        {
            item.Parent = this;
            base.InsertItem(index, item);
        }


        public int CompareTo(SimpleTreeNode other)
        {
            var res = CompareInternal(other);
            return res == 0 ? - other.CompareInternal(this) : res;

        }

        private int CompareInternal(SimpleTreeNode other)
        {
            var current = Parent;
            while (current != null)
            {
                if (current == other)
                    return 1;
                current = current.Parent;
            } 
            return 0;
        }

        public int CompareTo(object obj)
        {
            if (ReferenceEquals(null, obj)) return 1;
            if (!(obj is SimpleTreeNode)) throw new ArgumentException($"Object must be of type {nameof(SimpleTreeNode)}");
            return CompareTo((SimpleTreeNode) obj);
        }

        public static bool operator <(SimpleTreeNode left, SimpleTreeNode right)
        {
            return left.CompareTo(right) < 0;
        }

        public static bool operator >(SimpleTreeNode left, SimpleTreeNode right)
        {
            return left.CompareTo(right) > 0;
        }

        public static bool operator <=(SimpleTreeNode left, SimpleTreeNode right)
        {
            return left.CompareTo(right) <= 0;
        }

        public static bool operator >=(SimpleTreeNode left, SimpleTreeNode right)
        {
            return left.CompareTo(right) >= 0;
        }
    }
}
