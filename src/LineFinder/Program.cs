using System;
using System.IO;
using Rhyous.LineFinder.Arguments;
using Rhyous.SimpleArgs;

namespace Rhyous.LineFinder
{
    class Program
    {
        static void Main(string[] args)
        {
            ArgsManager.Instance.Start(new ArgsHandler(), args);
            string sourceFile = Args.Value("SourceFile");
            string searchstring = Args.Value("SearchString");
            string destinationFile = Args.Value("DestinationFile");
            bool matchCase = Args.Get("MatchCase").IsEnabled();
            StringComparison comparison = (matchCase)
                ? StringComparison.CurrentCulture
                : StringComparison.CurrentCultureIgnoreCase;
            using (var file = new StreamReader(sourceFile))
            {
                using (var writer = new StreamWriter(destinationFile))
                {
                    string line;
                    while ((line = file.ReadLine()) != null)
                    {
                        if (line.Contains(searchstring, comparison))
                        {
                            writer.WriteLine(line);
                        }
                    }
                }
            }
        }
    }
}
