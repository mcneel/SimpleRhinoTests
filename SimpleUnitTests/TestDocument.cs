using System;
using System.IO;

using Rhino;
using Rhino.Testing.Fixtures;

using NUnit.Framework;
using NUnit.Framework.Internal;

namespace SimpleNUnitTests
{
    [TestFixture]
    public sealed class TestDocument : RhinoTestFixture
    {
        [Test]
        public void TestOpen()
        {
            string output = Path.GetDirectoryName(GetType().Assembly.Location);
            string modelPath = Path.Combine(output, @"Files\circle.3dm");

            Assert.DoesNotThrow(() => RhinoDoc.Open(modelPath, out bool _));
        }

        [Test]
        public void TestOpenHeadless()
        {
            string output = Path.GetDirectoryName(GetType().Assembly.Location);
            string modelPath = Path.Combine(output, @"Files\circle.3dm");

            Assert.DoesNotThrow(() => RhinoDoc.OpenHeadless(modelPath));
        }
    }
}
