using System.Text;

namespace Des
{
  public class Helpers
  {
    public static string ExpandString(string str)
    {
      var sb = new StringBuilder(str);
      while (((str.Length * Constants.SizeOfChar) % Constants.SizeOfBlock) != 0)
      {
        sb.Append("%");
      }

      return sb.ToString();
    }

    public static string ExpandKey(string str, int keyLength)
    {
      if (str.Length > keyLength) 
      {
        return str.Substring(0, keyLength);
      }
      
      // it's not binary, but it works =)
      str = ExpandLeftZero(str, keyLength);

      return str;
    }

    public static string ExpandLeftZero(string str, int lenNeeded)
    {
      while (str.Length < lenNeeded) 
      {
        str = $"0{str}";
      }

      return str;
    }
  }
}