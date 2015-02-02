using System;

namespace Core.Helpers.Extensions
{
  public static class StringExtensions
  {
    /// <summary>
    /// Truncates a string to be no longer than a certain length
    /// Source from : http://stackoverflow.com/questions/16765328/replace-end-of-string-with-dots-if-word-is-longer-then-x
    /// </summary>
    /// <param name="s"></param>
    /// <param name="length"></param>
    /// <returns></returns>
    public static string TruncateWithEllipsis(this String s, int length)
    {
      if (s == null) return null;
      if (string.IsNullOrWhiteSpace(s)) return String.Empty;

      //there may be a more appropiate unicode character for this
      const string ellipsis = "...";

      if (ellipsis.Length > length)
        throw new ArgumentOutOfRangeException("length", length, "length must be at least as long as ellipsis.");

      if (s.Length > length)
        return s.Substring(0, length - ellipsis.Length) + ellipsis;
      else
        return s;
    }
  }
}
