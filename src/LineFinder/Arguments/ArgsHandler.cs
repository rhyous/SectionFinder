using System.Collections.Generic;
using System.IO;
using Rhyous.SimpleArgs;

namespace Rhyous.LineFinder.Arguments
{
    // Add this line of code to Main() in Program.cs
    //
    //   ArgsManager.Instance.Start(new ArgsHandler(), args);
    //

    /// <summary>
    /// A class that implements IArgumentHandler where command line
    /// arguments are defined.
    /// </summary>
    public sealed class ArgsHandler : ArgsHandlerBase
    {
        public ArgsHandler()
        {
            Arguments = new List<Argument>
            {
                new Argument
                {
                    Name = "SourceFile",
                    ShortName = "f",
                    Description = "The file to search in. If called without the SourceFile= or f=, it must be first.",
                    Example = @"searchfile.txt",
                    SequenceId = 1,
                    IsRequired = true,
                    CustomValidation = value => File.Exists(value),
                    Action = value =>
                    {
                        if (string.IsNullOrWhiteSpace(ArgumentList.Instance.Args["DestinationFile"].Value))
                        ArgumentList.Instance.Args["DestinationFile"].Value = value + ".filtered";
                    }
                },
                new Argument
                {
                    Name = "SearchString",
                    ShortName = "s",
                    Description = "The string to search for. If called without the SearchString= or s=, it must be second.",
                    Example = @"findme",
                    SequenceId = 2,
                    IsRequired = true
                },
                new Argument
                {
                    Name = "DestinationFile",
                    ShortName = "d",
                    Description = "The output file to dump the found job. If called without the DestinationFile= or d=, it must be third.",
                    Example = @"outputfile.txt",
                    SequenceId = 3,
                    IsRequired = false
                },
                new Argument
                {
                    Name = "MatchCase",
                    ShortName = "mc",
                    Description = "Tells the search to be case sensitive.",
                    Example = @"/mc",
                    DefaultValue = "false",
                    AllowedValues = CommonAllowedValues.TrueFalse,
                    SequenceId = 4,
                    IsRequired = false
                }
            };
        }
    }
}
