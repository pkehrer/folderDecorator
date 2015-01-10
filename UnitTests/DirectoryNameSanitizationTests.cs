using FolderDesigner;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FolderDesigner.UnitTests
{
    [TestFixture]
    public class DirectoryNameSanitizationTests : AssertionHelper
    {
        private readonly DirectoryNameSanitizer _sanitizer = new DirectoryNameSanitizer();

        [TestCase("The.Name.Of.It", "the name of it", MediaType.Movie, MediaType.Tv)]
        [TestCase("ADate2001", "adate", MediaType.Movie, MediaType.Tv)]
        [TestCase("Firefly.2002.720p.BluRay.DTS.x264-ESiR", "firefly", MediaType.Movie, MediaType.Tv)]
        [TestCase("Despicable.Me.720p.Bluray.x264-CBGB", "despicable me", MediaType.Movie, MediaType.Tv)]
        [TestCase("Despicable.Me.1080p.Bluray.x264-CBGB", "despicable me", MediaType.Movie, MediaType.Tv)]
        [TestCase("Angels & Demons", "angels and demons", MediaType.Movie, MediaType.Tv)]
        [TestCase("(2000) The Marshall Mathers LP [v0]", "the marshall mathers lp", MediaType.Music)]
        [TestCase("A - Dash", "a dash", MediaType.Music)]
        [TestCase("Remove this date 2008", "remove this date", MediaType.Music)]
        public void TestSanitization(string input, string expectedOutput, params MediaType[] mediaTypes)
        {
            foreach (var type in mediaTypes)
            {
                Expect(_sanitizer.SanitizeDirectoryName(type, input), EqualTo(expectedOutput));
            }
        }

    }
}
