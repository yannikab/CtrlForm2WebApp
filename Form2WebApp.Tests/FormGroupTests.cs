using System;
using System.Linq;

using Form2.Form.Content;
using Form2.Form.Content.Items;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Form2WebApp.Tests
{
    [TestClass]
    public class FormTests
    {
        [TestMethod]
        public void Add()
        {
            FormGroup g = new FormGroup("");
            FormLabel l = new FormLabel("");
            g.Add(l);

            Assert.AreSame(l.Container, g);
            Assert.IsTrue(g.Contents.Any(c => ReferenceEquals(c, l)));
            Assert.AreEqual(g.Contents.Count, 1);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AddNull()
        {
            FormGroup g = new FormGroup("");
            g.Add(null);
        }

        [TestMethod]
        public void AddContained()
        {
            FormGroup g1 = new FormGroup("");
            FormGroup g2 = new FormGroup("");

            FormLabel l = new FormLabel("");
            g1.Add(l);
            g2.Add(l);

            Assert.AreSame(l.Container, g2);
            Assert.IsFalse(g1.Contents.Any(c => ReferenceEquals(c, l)));
            Assert.IsTrue(g2.Contents.Any(c => ReferenceEquals(c, l)));
        }

        [TestMethod]
        public void AddSame()
        {
            FormGroup g = new FormGroup("");

            FormLabel l = new FormLabel("");
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
            FormGroup g = new FormGroup("");
            g.Remove(null);
        }

        [TestMethod]
        public void Remove()
        {
            FormGroup g = new FormGroup("");

            FormLabel l = new FormLabel("");
            g.Add(l);
            g.Remove(l);

            Assert.IsNull(l.Container);
            Assert.IsFalse(g.Contents.Any(c => ReferenceEquals(c, l)));
            Assert.AreEqual(g.Contents.Count, 0);
        }
    }
}
