using System;
using System.IO;
using System.Linq;

using Rhino.Geometry;
using Rhino.FileIO;

using Rhino.Testing.Fixtures;

using NUnit.Framework;
using NUnit.Framework.Internal;

namespace SimpleNUnitTests
{
    [TestFixture]
    public sealed class PrimitivesFixture : RhinoTestFixture
    {
        [Test]
        public void TestSDKCircle()
        {
            var circle = new Circle(12);
            Assert.AreEqual(12, circle.Radius);
            Assert.AreEqual(12.0 * Math.PI * 2, circle.Circumference);
        }

        [Test]
        public void TestCircleInMemoryFile3dm()
        {
            string output = Path.GetDirectoryName(GetType().Assembly.Location);
            string full_path = Path.Combine(output, @"Files\circle.3dm");

            Circle circle = default;

            using (File3dm doc = File3dm.Read(full_path))
            {
                foreach (File3dmObject obj in doc.Objects)
                {
                    if (obj.Geometry is Curve curve)
                    {
                        curve.TryGetCircle(out circle); ;
                    }
                }
            }

            Assert.AreEqual(12, circle.Radius);
            Assert.AreEqual(12.0 * Math.PI * 2, circle.Circumference);
        }
    }
}
