using System;
using System.Linq;

using Form2.Html.Content.Elements.Containers;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Form2WebApp.Tests
{
    [TestClass]
    public class HtmlContainerTests
    {
        [TestMethod]
        public void Add()
        {
            HtmlDiv g = new HtmlDiv("");
            HtmlLabel l = new HtmlLabel("");
            g.Add(l);

            Assert.AreSame(l.Container, g);
            Assert.IsTrue(g.Contents.Any(c => ReferenceEquals(c, l)));
            Assert.AreEqual(g.Contents.Count, 1);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AddNull()
        {
            HtmlDiv g = new HtmlDiv("");
            g.Add(null);
        }

        [TestMethod]
        public void AddContained()
        {
            HtmlDiv g1 = new HtmlDiv("");
            HtmlDiv g2 = new HtmlDiv("");

            HtmlLabel l = new HtmlLabel("");
            g1.Add(l);
            g2.Add(l);

            Assert.AreSame(l.Container, g2);
            Assert.IsFalse(g1.Contents.Any(c => ReferenceEquals(c, l)));
            Assert.IsTrue(g2.Contents.Any(c => ReferenceEquals(c, l)));
        }

        [TestMethod]
        public void AddSame()
        {
            HtmlDiv g = new HtmlDiv("");

            HtmlLabel l = new HtmlLabel("");
            g.Add(l);
            g.Add(l);

            Assert.AreSame(l.Container, g);
            Assert.IsTrue(g.Contents.Any(c => ReferenceEquals(c, l)));
            Assert.AreEqual(g.Contents.Count, 1);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void RemoveNull()
        {
            HtmlDiv g = new HtmlDiv("");
            g.Remove(null);
        }

        [TestMethod]
        public void Remove()
        {
            HtmlDiv g = new HtmlDiv("");

            HtmlLabel l = new HtmlLabel("");
            g.Add(l);
            g.Remove(l);

            Assert.IsNull(l.Container);
            Assert.IsFalse(g.Contents.Any(c => ReferenceEquals(c, l)));
            Assert.AreEqual(g.Contents.Count, 0);
        }

        [TestMethod]
        public void Insert()
        {
            HtmlDiv g = new HtmlDiv("");
            HtmlLabel l = new HtmlLabel("");
            g.Insert(0, l);

            Assert.AreSame(l.Container, g);
            Assert.IsTrue(g.Contents.Any(c => ReferenceEquals(c, l)));
            Assert.AreEqual(g.Contents.Count, 1);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void InsertNull()
        {
            HtmlDiv g = new HtmlDiv("");
            g.Insert(0, null);
        }

        [TestMethod]
        public void InsertContained()
        {
            HtmlDiv g1 = new HtmlDiv("");
            HtmlDiv g2 = new HtmlDiv("");

            HtmlLabel l = new HtmlLabel("");
            g1.Add(l);
            g2.Insert(0, l);

            Assert.AreSame(l.Container, g2);
            Assert.IsFalse(g1.Contents.Any(c => ReferenceEquals(c, l)));
            Assert.IsTrue(g2.Contents.Any(c => ReferenceEquals(c, l)));
        }

        [TestMethod]
        public void InsertSame()
        {
            HtmlDiv g = new HtmlDiv("");

            HtmlLabel l = new HtmlLabel("");
            g.Insert(0, l);
            g.Insert(0, l);

            Assert.AreSame(l.Container, g);
            Assert.IsTrue(g.Contents.Any(c => ReferenceEquals(c, l)));
            Assert.AreEqual(g.Contents.Count, 1);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void InsertOutOfRange1()
        {
            HtmlDiv g = new HtmlDiv("");

            HtmlLabel l = new HtmlLabel("");
            g.Insert(1, l);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void InsertOutOfRange2()
        {
            HtmlDiv g = new HtmlDiv("");

            g.Add(new HtmlLabel(""));
            g.Add(new HtmlLabel(""));
            g.Add(new HtmlLabel(""));

            g.Insert(4, new HtmlLabel(""));
        }
    }
}
