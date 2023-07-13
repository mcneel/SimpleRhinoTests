using System;
using Rhino.Geometry;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace SimpleeRHTests
{
    [TestFixture]
    public class PrimitivesFixture : Rhino.Test.RhinoTestFixture
    {
        [Test]
        public void TestSDKCircle()
        {
            var circle = new Circle(12);
            Assert.AreEqual(12, circle.Radius);
            Assert.AreEqual(12.0 * Math.PI * 2, circle.Circumference);
        }

        [Test]
        public void TestCircleInMemoryDocument()
        {
            var circle = new Circle(12);
            Assert.AreEqual(12, circle.Radius);
            Assert.AreEqual(12.0 * Math.PI * 2, circle.Circumference);
        }

        [Test]
        public void TestCircleInDocument()
        {
            var circle = new Circle(12);
            Assert.AreEqual(12, circle.Radius);
            Assert.AreEqual(12.0 * Math.PI * 2, circle.Circumference);
        }

        [Test]
        public void TestCircleInGrasshopper()
        {
            var circle = new Circle(12);
            Assert.AreEqual(12, circle.Radius);
            Assert.AreEqual(12.0 * Math.PI * 2, circle.Circumference);
        }
    }
}
