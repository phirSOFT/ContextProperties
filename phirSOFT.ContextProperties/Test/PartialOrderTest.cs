using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using NUnit.Framework;
using Test;
using static Test.SimpleTreeNode;

namespace phirSOFT.ContextProperties.Tests
{
    [TestFixture]
    public partial class PartialOrderTest
    {
        [Test]
        public void TestMax([Range(1, 10)] int size)
        {
            var exp = size - 1;
            var set = new RelationalSet();

            for (var i = 0; i < size; i++)
            {
                set.Add(i);
            }

            var subsets = new SortedSet<RelationalSet>(new SetComparer());
            var lastGeneration = new SortedSet<RelationalSet>(new SetComparer()) { set };
            while (size > 0)
            {
                var gen = new SortedSet<RelationalSet>(new SetComparer());
                foreach (var last in lastGeneration)
                {
                    foreach (var element in last)
                    {
                        var clone = last.Clone();
                        clone.Remove(element);
                        gen.Add(clone);
                        subsets.Add(clone);
                    }
                }
                lastGeneration = gen;
                size--;
            }

            var max = subsets.Max();

            Assert.AreEqual(exp, max.Count);


        }



        private static IEnumerable<SimpleTreeNode> NodeSet = new[] { root, l, r, ll, lr, rl, rr, lll, llr, lrl, lrr, rll, rlr, rrl, rrr };

        [Test]
        [TestCaseSource(nameof(Nodes))]
        public void TestOrder(object max)
        {
            
            var maxNode = max as SimpleTreeNode;
            var order = new List<SimpleTreeNode>();
            var current = maxNode;

            while (current != null)
            {
                order.Add(current);
                current = current.Parent;
            }

            var resut = PartialOrderedSetHelper.TreeOrder(NodeSet, maxNode).ToList();
            CollectionAssert.AreEqual(order,resut);

        }

        private static IEnumerable Nodes
        {
            get => NodeSet.Select(n => new TestCaseData(n));
        }
    }
}
