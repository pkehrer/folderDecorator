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
        [TestCase("Despicable.Me.720p.Bluray.x264-CBGB", "despicable me")]
        [TestCase("Despicable.Me.1080p.Bluray.x264-CBGB", "despicable me")]
        [TestCase("Angels & Demons", "angels and demons")]
        public void TestSanitization(string input, string expectedOutput)
        {
            Expect(_sanitizer.SanitizeDirectoryName(input), EqualTo(expectedOutput));
        }

    }
}
