using System;

namespace Des  
{  
  public class BitArray  
  {  
    public static int[] ToBits(int decimalnumber, int numberofbits)  
    {  
      var bitarray = new int[numberofbits];  
      var k = numberofbits-1;  
      var bd = Convert.ToString(decimalnumber, 2).ToCharArray();  
  
      for (var i = bd.Length - 1; i >= 0; --i,--k)
      {
        if (bd[i] == '1')
          bitarray[k] = 1;
        else
          bitarray[k] = 0;
      }  
  
      while(k >= 0)  
      {  
        bitarray[k] = 0;  
        --k;  
      }  
  
      return bitarray;  
    }  
  
    public static int ToDecimal(int[] bitsarray) =>
      Convert.ToInt32(string.Join("", bitsarray), 2);  
  }  
}  