using FolderDesigner;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests
{
    [TestFixture]
    public class DirectoryNameSanitizationTests : AssertionHelper
    {
        private readonly DirectoryNameSanitizer _sanitizer = new DirectoryNameSanitizer();

        [TestCase("The.Name.Of.It", "the name of it")]
        [TestCase("ADate2001", "adate")]
        [TestCase("Firefly.2002.720p.BluRay.DTS.x264-ESiR", "firefly")]
        public void TestSanitization(string input, string expectedOutput)
        {
            Expect(_sanitizer.SanitizeDirectoryName(input), EqualTo(expectedOutput));
        }

    }
}
