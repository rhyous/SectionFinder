using System.IO;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Rhyous.SectionFinder.Tests
{
    [TestClass]
    public class SectionFinderTests
    {
        public StreamReader GenerateStreamReaderFromString(string s)
        {
            MemoryStream stream = new MemoryStream();
            StreamWriter writer = new StreamWriter(stream);
            writer.Write(s);
            writer.Flush();
            stream.Position = 0;
            StreamReader reader = new StreamReader(stream);
            return reader;
        }

        [TestMethod]
        public void SearchLines()
        {
            const string multiLineString = "A\r\nB\r\nC\r\nA\r\nB\r\nC\r\nA\r\nB\r\nC\r\n";
            var builder = new StringBuilder();
            using (var sectionFinder = new SectionFinder())
            {
                sectionFinder.SearchString = "B";
                sectionFinder.Begin = "A";
                sectionFinder.End = "C";
                sectionFinder.Reader = GenerateStreamReaderFromString(multiLineString);
                sectionFinder.Writer = new StringWriter(builder);
                sectionFinder.SearchLines();
            }
            Assert.AreEqual(multiLineString, builder.ToString());
        }

        [TestMethod]
        public void SearchLinesIgnoresLinesOutOfSection()
        {
            const string multiLineString = "A\r\nB\r\nC\r\nA\r\nB\r\nC\r\nA\r\nB\r\nC\r\n";
            const string expected = "A\r\nB\r\nA\r\nB\r\nA\r\nB\r\n";
            var builder = new StringBuilder();
            using (var sectionFinder = new SectionFinder())
            {
                sectionFinder.SearchString = "B";
                sectionFinder.Begin = "A";
                sectionFinder.End = "B";
                sectionFinder.Reader = GenerateStreamReaderFromString(multiLineString);
                sectionFinder.Writer = new StringWriter(builder);
                sectionFinder.SearchLines();
            }
            Assert.AreEqual(expected, builder.ToString());
        }

        [TestMethod]
        public void XmlTest()
        {
            const string multiLineString = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>\r\n" +
                                           "  <begin>\r\n" +
                                           "    <middle>Trinity</middle>\r\n" +
                                           "  </begin>\r\n" +
                                           "  <begin>\r\n" +
                                           "    <middle>of</middle>\r\n" +
                                           "  </begin>\r\n" +
                                           "  <begin>\r\n" +
                                           "    <middle>Mind</middle>\r\n" +
                                           "  </begin>";
            const string expected = "  <begin>\r\n" +
                                    "    <middle>Trinity</middle>\r\n" +
                                    "  </begin>\r\n";
            var builder = new StringBuilder();
            using (var sectionFinder = new SectionFinder())
            {
                sectionFinder.SearchString = "Trinity";
                sectionFinder.Begin = "<begin>";
                sectionFinder.End = "</begin>";
                sectionFinder.Reader = GenerateStreamReaderFromString(multiLineString);
                sectionFinder.Writer = new StringWriter(builder);
                sectionFinder.SearchLines();
            }
            Assert.AreEqual(expected, builder.ToString());
        }

        [TestMethod]
        public void XmlTestEndNotDefined()
        {
            const string multiLineString = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>\r\n" +
                                           "  <begin>\r\n" +
                                           "    <middle>Trinity</middle>\r\n" +
                                           "  </begin>\r\n" +
                                           "  <begin>\r\n" +
                                           "    <middle>of</middle>\r\n" +
                                           "  </begin>\r\n" +
                                           "  <begin>\r\n" +
                                           "    <middle>Mind</middle>\r\n" +
                                           "  </begin>";
            const string expected = "  <begin>\r\n" +
                                    "    <middle>Trinity</middle>\r\n" +
                                    "  </begin>\r\n";
            var builder = new StringBuilder();
            using (var sectionFinder = new SectionFinder())
            {
                sectionFinder.SearchString = "Trinity";
                sectionFinder.Begin = "<begin>";
                sectionFinder.Reader = GenerateStreamReaderFromString(multiLineString);
                sectionFinder.Writer = new StringWriter(builder);
                sectionFinder.SearchLines();
            }
            Assert.AreEqual(expected, builder.ToString());
        }

        [TestMethod]
        public void XmlTestEndTagMissing()
        {
            const string multiLineString = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>\r\n" +
                                           "  <begin>\r\n" +
                                           "    <middle>Trinity</middle>\r\n" +
                                           "  <begin>\r\n" +
                                           "    <middle>of</middle>\r\n" +
                                           "  </begin>\r\n" +
                                           "  <begin>\r\n" +
                                           "    <middle>Mind</middle>\r\n" +
                                           "  </begin>";
            const string expected = "  <begin>\r\n" +
                                    "    <middle>Trinity</middle>\r\n";
            var builder = new StringBuilder();
            using (var sectionFinder = new SectionFinder())
            {
                sectionFinder.SearchString = "Trinity";
                sectionFinder.Begin = "<begin>";
                sectionFinder.End = "</begin>";
                sectionFinder.Reader = GenerateStreamReaderFromString(multiLineString);
                sectionFinder.Writer = new StringWriter(builder);
                sectionFinder.SearchLines();
            }
            Assert.AreEqual(expected, builder.ToString());
        }

        [TestMethod]
        public void XmlTestStringFoundOutsideSectionNotIncluded()
        {
            const string multiLineString = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>\r\n" +
                                           "  <wrong>\r\n" +
                                           "    <middle>Trinity</middle>\r\n" +
                                           "  </wrong>" +
                                           "  <begin>\r\n" +
                                           "    <middle>of</middle>\r\n" +
                                           "  </begin>\r\n" +
                                           "  <begin>\r\n" +
                                           "    <middle>Mind</middle>\r\n" +
                                           "  </begin>";
            const string expected = "";
            var builder = new StringBuilder();
            using (var sectionFinder = new SectionFinder())
            {
                sectionFinder.SearchString = "Trinity";
                sectionFinder.Begin = "<begin>";
                sectionFinder.End = "</begin>";
                sectionFinder.Reader = GenerateStreamReaderFromString(multiLineString);
                sectionFinder.Writer = new StringWriter(builder);
                sectionFinder.SearchLines();
            }
            Assert.AreEqual(expected, builder.ToString());
        }

        [TestMethod]
        public void XmlTestExcludeBeginExcludeEnd()
        {
            const string multiLineString = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>\r\n" +
                                           "  <begin>\r\n" +
                                           "    <middle>Trinity</middle>\r\n" +
                                           "  </begin>\r\n" +
                                           "  <begin>\r\n" +
                                           "    <middle>of</middle>\r\n" +
                                           "  </begin>\r\n" +
                                           "  <begin>\r\n" +
                                           "    <middle>Mind</middle>\r\n" +
                                           "  </begin>";
            const string expected = "    <middle>Trinity</middle>\r\n";
            var builder = new StringBuilder();
            using (var sectionFinder = new SectionFinder())
            {
                sectionFinder.SearchString = "Trinity";
                sectionFinder.Begin = "<begin>";
                sectionFinder.End = "</begin>";
                sectionFinder.Reader = GenerateStreamReaderFromString(multiLineString);
                sectionFinder.Writer = new StringWriter(builder);
                sectionFinder.ExcludeBegin = true;
                sectionFinder.ExcludeEnd = true;
                sectionFinder.SearchLines();
            }
            Assert.AreEqual(expected, builder.ToString());
        }

        [TestMethod]
        public void XmlTestExcludeEntireSectionOutputed()
        {
            const string multiLineString = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>\r\n" +
                                           "  <begin>\r\n" +
                                           "    <id>1</id>\r\n" +
                                           "    <middle>Trinity</middle>\r\n" +
                                           "  </begin>\r\n" +
                                           "  <begin>\r\n" +
                                           "    <middle>of</middle>\r\n" +
                                           "  </begin>\r\n" +
                                           "  <begin>\r\n" +
                                           "    <middle>Mind</middle>\r\n" +
                                           "  </begin>";
            const string expected = "  <begin>\r\n" +
                                    "    <id>1</id>\r\n" +
                                    "    <middle>Trinity</middle>\r\n" +
                                    "  </begin>\r\n";
            var builder = new StringBuilder();
            using (var sectionFinder = new SectionFinder())
            {
                sectionFinder.SearchString = "Trinity";
                sectionFinder.Begin = "<begin>";
                sectionFinder.End = "</begin>";
                sectionFinder.Reader = GenerateStreamReaderFromString(multiLineString);
                sectionFinder.Writer = new StringWriter(builder);
                sectionFinder.SearchLines();
            }
            Assert.AreEqual(expected, builder.ToString());
        }
    }
}
