using System;
using Rhyous.SectionFinder.Arguments;
using Rhyous.SimpleArgs;

namespace Rhyous.SectionFinder
{
    class Program
    {
        private static void Main(string[] args)
        {
            ArgsManager.Instance.Start(new ArgsHandler(), args);
            using (var sectionFinder = new SectionFinder
            {
                SourceFile = Args.Value("SourceFile"),
                SearchString = Args.Value("SearchString"),
                DestinationFile = Args.Value("DestinationFile"),
                Begin = Args.Value("Begin"),
                End = Args.Value("End"),
                ExcludeBegin = Args.Get("ExcludeBegin").IsEnabled(),
                ExcludeEnd = Args.Get("ExcludeEnd").IsEnabled(),
                Comparison = (Args.Get("MatchCase").IsEnabled())
                    ? StringComparison.CurrentCulture
                    : StringComparison.CurrentCultureIgnoreCase
            })
            {
                sectionFinder.SearchLines();
            }
        }
    }
}