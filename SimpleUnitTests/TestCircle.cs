using System;
using Rhino.Geometry;
using NUnit.Framework;
using NUnit.Framework.Internal;
using Rhino;
using System.IO;
using System.Linq;
using Rhino.DocObjects;
using Rhino.FileIO;
using Rhino.UI;
using Rhino.Test;

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
        public void TestCircleInMemoryFile3dm()
        {
           string output = Path.GetDirectoryName(GetType().Assembly.Location);
            string full_path = Path.Combine(output, "circle.3dm");

            Circle circle;

            using (File3dm doc = File3dm.Read(full_path))
            {
                var circle_file_3dm_geom = doc.Objects.Select(a => a.Geometry).First();

                Curve curve = (Curve)circle_file_3dm_geom;
                curve.TryGetCircle(out circle);
            }

            Assert.AreEqual(12, circle.Radius);
            Assert.AreEqual(12.0 * Math.PI * 2, circle.Circumference);
        }
    }
}
