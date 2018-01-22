using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using static Test.SimpleTreeNode;

namespace Test
{
    [TestFixture]
    public class TreeTest
    {
        [Test]
        [TestCaseSource(nameof(TreeCases))]
        public void TestCompare(object x, object y, int result)
        {
            var left = x as SimpleTreeNode;
            var right = y as SimpleTreeNode;
            Assert.NotNull(left);
            Assert.NotNull(right);

            Assert.AreEqual(result, left.CompareTo(right));
        }

        private static IEnumerable TreeCases
        {
            get
            {
                var translation = new Dictionary<SimpleTreeNode, string>()
                {
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

                foreach (var node in new[] {l, r, ll, lr, rl, rr, lll, llr, lrl, lrr, rll, rlr, rrl, rrr})
                {
                    yield return new TestCaseData(root,node, -1).SetName($"root < {translation[node]}");
                    yield return new TestCaseData(node, root, 1).SetName($"{translation[node]} > root");
                }

                foreach (var node in new[] { ll, lr, lll, llr, lrl, lrr})
                {
                    yield return new TestCaseData(l, node, -1).SetName($" l < {translation[node]}");
                    yield return new TestCaseData(node, l, 1).SetName($"{translation[node]} > l");
                }

                foreach (var node in new[] { l, ll, lr, lll, llr, lrl, lrr })
                {
                    yield return new TestCaseData(r, node, 0).SetName($"r ?? {translation[node]}");
                    yield return new TestCaseData(node, r, 0).SetName($"{translation[node]} ?? r");
                }
            }
        }
    }
}
