# SectionFinder
A C# app to find sections of a file that contain a search string. 

This tool was written to allow searching large text files. It can search files of any size, even files to big for common editors such as Notepad++.

# SectionFinder Usage
```
Usage:
  SectionFinder.exe searchfile.txt findme outputfile.txt [begin] [end] [true] [true] [/mc]

Arguments:
  SourceFile            (Required) The file to search in. If called without the SourceFile= or f=, it must be first.
  SearchString          (Required) The string to search for. If called without the SearchString= or s=, it must be second.
  DestinationFile       (Optional) The output file to dump the found job. If called without the DestinationFile= or d=, it must be third.
  Begin                 (Required) The pattern that identifies the start of a section.
  End                   (Optional) The pattern that identifies the end of a section.
  ExcludeBegin          (Optional) Exclude the begin section line in the output. Default value: false
  ExcludeEnd            (Optional) Exclude the end section line in the output. Default value: false
  MatchCase             (Optional) Tells the search to be case sensitive. Default value: false
```

# LineFinder
This project includes a LineFinder as well, that just finds a line in text file.
```
Usage:
  LineFinder.exe searchfile.txt findme [outputfile.txt] [/mc]

Arguments:
  SourceFile            (Required) The file to search in. If called without the SourceFile= or f=, it must be first.
  SearchString          (Required) The string to search for. If called without the SearchString= or s=, it must be second.
  DestinationFile       (Optional) The output file to dump the found job. If called without the DestinationFile= or d=, it must be third.
```

# Large Files

This tool is especially usefull for finding data in large files. Sometimes a file is too large to open in regular editors like Notepad or Notepad++. For Linefinder, you can find any line in the file regardless of the file size. For Section Finder, you can find sections in large files easily with just a few search strings.

# Finding Sections of an Xml or log

Sometimes an Xml is huge. Even if it can be opened by Notepad or Notepad++ that still doesn't make it easy to read. If you only need on section, you can find it.

Imagine a firewall that logs to Xml and you want to find a snippet for a specific IP address.

```SectionFinder.exe file.xml 192.168.0.1 output.xml tagName```

This will find the snippet and put it into an output.xml file.
