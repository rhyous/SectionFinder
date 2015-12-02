# SectionFinder
A C# app to find sections of a file that contain a search string.

# SectionFinder Usage
Usage:
  SectionFinder.exe searchfile.txt findme outputfile.txt [outputfile.txt] [outputfile.txt] [true] [true] [/mc]

Arguments:
  SourceFile            (Required) The file to search in. If called without the SourceFile= or f=, it must be first.
  SearchString          (Required) The string to search for. If called without the SearchString= or s=, it must be second.
  Begin                 (Required) The pattern that identifies the start of a section.
  End                   (Optional) The pattern that identifies the end of a section.
  DestinationFile       (Optional) The output file to dump the found job. If called without the DestinationFile= or d=, it must be third.
  ExcludeBegin          (Optional) Exclude the begin section line in the output. Default value: false
  ExcludeEnd            (Optional) Exclude the end section line in the output. Default value: false
  MatchCase             (Optional) Tells the search to be case sensitive. Default value: false

# LineFinder
This project includes a LineFinder as well, that just finds a line.

Usage:
  LineFinder.exe searchfile.txt findme [outputfile.txt] [/mc]

Arguments:
  SourceFile            (Required) The file to search in. If called without the SourceFile= or f=, it must be first.
  SearchString          (Required) The string to search for. If called without the SearchString= or s=, it must be second.
  DestinationFile       (Optional) The output file to dump the found job. If called without the DestinationFile= or d=, it must be third.
