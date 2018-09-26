using System;
using System.Collections.Generic;
using System.Linq;
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

    public static string StringToBinary(string str, int size) =>
      CharCodesToBinary(str.Select(ch => (int) ch).ToArray(), size);
    
    public static string CharCodesToBinary(ICollection<int> codes, int size) =>
      String.Join("", codes.Select(code => ExpandLeftZero(Convert.ToString(code, 2), size)));
    
    public static string[] SplitBlocks(string str)
    {
      var blocks = new string[str.Length / Constants.LengthOfBlock];

      for (var i = 0; i < blocks.Length; i++)
      {
        var block = str.Substring(i * Constants.LengthOfBlock, Constants.LengthOfBlock);
        blocks[i] = Helpers.StringToBinary(block, Constants.SizeOfChar);
      }

      return blocks;
    }
    
    public static string[] SplitBlocksBinary(string str)
    {
      var blocks = new string[str.Length / Constants.LengthOfBlock];

      for (var i = 0; i < blocks.Length; i++)
      {
        blocks[i] = str.Substring(i * Constants.SizeOfBlock, Constants.SizeOfBlock);
      }

      return blocks;
    }

    public static string[] SplitInTwo(string block) =>
      new[]
      {
        block.Substring(0, block.Length / 2),
        block.Substring(block.Length / 2, block.Length / 2)
      };

    public static string FromBinary(string str)
    {
      var blocks = Enumerable.Range(0, str.Length / Constants.SizeOfChar)
        .Select(i => Int32.Parse(str.Substring(i * Constants.SizeOfChar, Constants.SizeOfChar)));

      return String.Join(",", blocks);
    }

    public static string Xor(string left, string right)
    {
      var sb = new StringBuilder();
      
      for (var i = 0; i < left.Length; i++) 
      {
        sb.Append(left[i] == right[i] ? "0" : "1");
      }

      return sb.ToString();
    }

    // public static string F(string right, string key)
    // {
    //   var rightExpanded = E(right);
    // }
    
    //function f(right, key) {
//    let rightExpanded = E(right);
//    let result = XOR(rightExpanded, key).join('');
//
//    // split into 6-bit blocks
//    let bBlocks = result.match(/.{6}/g);
//    result = S(bBlocks).join('');
//
//    result = P(result);
//
//    return result;
//}
//
//function extendKeyUnevenBits(str) {
//    // split each 7 characters
//    let bytes = str.match(/.{7}/g);
//    for (let i = 0; i < bytes.length; i++) {
//        // let ones = bytes[i].split('').reduce((acc, x) => acc + parseInt(x), 0);
//        // [] - in case we have all 0s
//        let ones = (bytes[i].match(/1/g) || []).length;
//        bytes[i] += ones % 2 == 0 ? 1 : 0;
//    }
//
//    return bytes.join('');
//}
//
//function keyIP(str) {
//    return keyIPTable.map(i => str[i - 1]);
//}
//
//function ithKey(str) {
//    return ithKeyTable.map(i => str[i - 1]);
//}
//
//function IP(str) {
//    return IPTable.map(i => str[i - 1]);
//}
//
//function IPReversed(str) {
//    return IPReversedTable.map(i => str[i - 1]);
//}
//
//// tables are 1-based
//function E(str) {
//    return Etable.map(i => str[i - 1]);
//}
//
//function P(str) {
//    return Ptable.map(i => str[i - 1]);
//}
//
//function S(bBlocks) {
//    // b-block = 6bit
//    // b'-block = 4bit 
//    let resultBlocks = [];
//    for (let i = 0; i < bBlocks.length; i++) {
//        let block = bBlocks[i];
//
//        let a = block[0] + block[block.length - 1];
//        let b = block.substring(1, block.length - 1);
//        a = parseInt(a, 2);
//        b = parseInt(b, 2);
//
//        // binary representation
//        let resultBlock = parseInt(Stables[i][a][b]).toString(2);
//        resultBlock = expandLeftZero(resultBlock, 4);
//        resultBlocks.push(resultBlock);
//    }
//
//    return resultBlocks;
//}
//
//function shiftKeysLeft(left, right, i) {
//    left = shiftArray(left, -keyShifts[i]);
//    right = shiftArray(right, -keyShifts[i]);
//
//    return [left, right];
//}
//
//function shiftKeysRight(left, right, i) {
//    left = shiftArray(left, keyShiftsReversed[i]);
//    right = shiftArray(right, keyShiftsReversed[i]);
//
//    return [left, right];
//}
//
//function shiftArray(key, len) {
//    while (len > 0) {
//        key.unshift(key.pop());
//        len--;
//    }
//    while (len < 0) {
//        key.push(key.shift());
//        len++;
//    }
//
//    return key;
//}
//

  }
}