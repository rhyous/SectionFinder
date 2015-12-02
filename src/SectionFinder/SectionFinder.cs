using System;
using System.Collections.Generic;
using System.IO;

namespace SectionFinder
{
    public class SectionFinder : IDisposable
    {
        public string SourceFile { get; set; } // Required
        public string DestinationFile { get; set; }

        public string Begin { get; set; } // Required
        public string SearchString { get; set; } // Required
        public string End
        {
            get { return _End; }
            set { UseEnd = !string.IsNullOrWhiteSpace(_End = value); }
        }  internal string _End;

        public bool ExcludeBegin { get; set; }
        public bool ExcludeEnd { get; set; }

        public bool MatchCase { get; set; }
        public StringComparison Comparison { get; set; }

        internal Line CurrentLine { get; set; }
        internal bool StringFoundInSection { get; set; }
        internal bool InsideSection { get; set; }
        internal bool UseEnd { get; set; }

        internal List<string> SectionLines
        {
            get { return _SectionLines ?? (_SectionLines = new List<string>()); }
        } internal List<string> _SectionLines;

        public TextWriter Writer
        {
            get { return _Writer ?? (_Writer = new StreamWriter(DestinationFile)); }
            set { _Writer = value; }
        } internal TextWriter _Writer;

        public StreamReader Reader
        {
            get { return _Reader ?? (_Reader = new StreamReader(SourceFile)); }
            set { _Reader = value; }
        } internal StreamReader _Reader;

        public void Dispose()
        {
            Writer.Dispose();
            Reader.Dispose();
        }

        public bool IsValid
        {
            get
            {
                return !string.IsNullOrWhiteSpace(Begin)
                         || (string.IsNullOrWhiteSpace(SearchString)
                         || (!string.IsNullOrWhiteSpace(SourceFile) || _Reader != null)
                         );
            }
        }

        public void SearchLines(string sourceFile, string begin, string searchString)
        {
            SourceFile = sourceFile;
            Begin = begin;
            SearchString = searchString;
            SearchLines();
        }

        public void SearchLines()
        {
            if (!IsValid)
                return;
            Line line = new Line();
            while ((line.Text = Reader.ReadLine()) != null)
            {
                ParseLine(line);
                line.Clear();
            }
        }

        internal void ParseLine(Line line)
        {
            StringFoundInSection = StringFoundInSection || line.Text.Contains(SearchString, Comparison);
            if (line.Text.Contains(Begin, StringComparison.CurrentCultureIgnoreCase))
            {
                BeginNewSection(line);
            }
            else if (UseEnd && line.Text.Contains(End, StringComparison.CurrentCultureIgnoreCase))
            {
                EndSection(line);
            }
            else if (InsideSection)
            {
                AddLine(line);
            }
        }

        internal void AddLine(Line line)
        {
            if (!line.AddedToSection)
            {
                SectionLines.Add(line.Text);
            }
        }

        internal void BeginNewSection(Line line)
        {
            if (InsideSection)
            {
                EndSection(line, true);
            }
            SectionLines.Clear();
            if (!ExcludeBegin)
                AddLine(line);
            InsideSection = true;
            StringFoundInSection = false;
        }

        internal void EndSection(Line line, bool exclude = false)
        {
            if (!ExcludeEnd && !exclude)
                AddLine(line);
            if (StringFoundInSection)
            {
                foreach (var jobline in SectionLines)
                {
                    Writer.WriteLine(jobline);
                }
            }
            StringFoundInSection = false;
            InsideSection = false;
        }
    }
}
