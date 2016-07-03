using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public static class StringExtend
{
    public static string Format(this string format, params string[] args)
    {
        return string.Format(format, args);
    }
}
