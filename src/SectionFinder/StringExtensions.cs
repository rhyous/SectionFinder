﻿using System;

namespace Rhyous.SectionFinder
{
    public static class StringExtensions
    {
        public static bool Contains(this string sourceString, string searchString, StringComparison stringComparison)
        {
            return sourceString.IndexOf(searchString, stringComparison) >= 0;
        }
    }
}
