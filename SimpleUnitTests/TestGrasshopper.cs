using System;
using System.IO;
using System.Linq;

using Rhino.Testing.Fixtures;
using Rhino.Testing.Grasshopper;

using NUnit.Framework;
using NUnit.Framework.Internal;

namespace SimpleNUnitTests
{
    [TestFixture]
    public sealed class TestGrasshopper : RhinoTestFixture
    {
        [Test]
        public void TestCircle()
        {
            string output = Path.GetDirectoryName(GetType().Assembly.Location);
            string ghFilePath = Path.Combine(output, @"Files\circle.gh");

            RhinoTestFixture.TestGrasshopper(ghFilePath, out bool result, out GHReport report);
            Assert.IsTrue(result);
            Assert.IsFalse(report.HasErrors);
        }

        [Test]
        public void TestPointError()
        {
            const string expected = "Data conversion failed from Surface to Point";

            string output = Path.GetDirectoryName(GetType().Assembly.Location);
            string ghFilePath = Path.Combine(output, @"Files\point_error.gh");

            RhinoTestFixture.TestGrasshopper(ghFilePath, out bool _, out GHReport report);

            Assert.IsTrue(report.Entries.Count() == 1);
            GHReportEntry deconstructPointReport = report.Entries.First();

            Assert.IsTrue(deconstructPointReport.Messages.Count() == 1);
            GHMessage message = deconstructPointReport.Messages.First();

            Assert.IsTrue(message.Level == GHMessageLevel.Error);
            Assert.IsTrue(message.Message == expected);
        }
    }
}
