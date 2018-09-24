namespace Des
{
  public class Des
  {
    public new string AlgorithmName = "Data Encryption Standard (DES)";

    //Initial Permutation Table  
    private int[] _ip = new int[]
    {
      58, 50, 42, 34, 26, 18, 10, 2, 60, 52, 44, 36, 28, 20, 12, 4,
      62, 54, 46, 38, 30, 22, 14, 6, 64, 56, 48, 40, 32, 24, 16, 8,
      57, 49, 41, 33, 25, 17, 9, 1, 59, 51, 43, 35, 27, 19, 11, 3,
      61, 53, 45, 37, 29, 21, 13, 5, 63, 55, 47, 39, 31, 23, 15, 7
    };

    //Circular Left shift Table (For Encryption)  
    private int[] _clst = new int[] {1, 1, 2, 2, 2, 2, 2, 2, 1, 2, 2, 2, 2, 2, 2, 1};

    //Circular Right shift Table (For Decryption)  
    private int[] _crst = new int[] {1, 2, 2, 2, 2, 2, 2, 1, 2, 2, 2, 2, 2, 2, 1, 1};

    //Compression Permutation Table  
    private int[] _cpt = new int[]
    {
      14, 17, 11, 24, 1, 5, 3, 28, 15, 6, 21, 10,
      23, 19, 12, 4, 26, 8, 16, 7, 27, 20, 13, 2,
      41, 52, 31, 37, 47, 55, 30, 40, 51, 45, 33, 48,
      44, 49, 39, 56, 34, 53, 46, 42, 50, 36, 29, 32
    };

    //Expansion Permutation Table  
    private int[] _ept = new int[]
    {
      32, 1, 2, 3, 4, 5, 4, 5, 6, 7, 8, 9,
      8, 9, 10, 11, 12, 13, 12, 13, 14, 15, 16, 17,
      16, 17, 18, 19, 20, 21, 20, 21, 22, 23, 24, 25,
      24, 25, 26, 27, 28, 29, 28, 29, 30, 31, 32, 1
    };

    //S Box Tables ( Actual 2D S Box Tables have been converted to 1D S Box Tables for easier computation )  
    private int[,] _sbox = new int[8, 64]
    {
      {
        14, 4, 13, 1, 2, 15, 11, 8, 3, 10, 6, 12, 5, 9, 0, 7,
        0, 15, 7, 4, 14, 2, 13, 1, 10, 6, 12, 11, 9, 5, 3, 8,
        4, 1, 14, 8, 13, 6, 2, 11, 15, 12, 9, 7, 3, 10, 5, 0,
        15, 12, 8, 2, 4, 9, 1, 7, 5, 11, 3, 14, 10, 0, 6, 13
      },
      {
        15, 1, 8, 14, 6, 11, 3, 4, 9, 7, 2, 13, 12, 0, 5, 10,
        3, 13, 4, 7, 15, 2, 8, 14, 12, 0, 1, 10, 6, 9, 11, 5,
        0, 14, 7, 11, 10, 4, 13, 1, 5, 8, 12, 6, 9, 3, 2, 15,
        13, 8, 10, 1, 3, 15, 4, 2, 11, 6, 7, 12, 0, 5, 14, 9
      },
      {
        10, 0, 9, 14, 6, 3, 15, 5, 1, 13, 12, 7, 11, 4, 2, 8,
        13, 7, 0, 9, 3, 4, 6, 10, 2, 8, 5, 14, 12, 11, 15, 1,
        13, 6, 4, 9, 8, 15, 3, 0, 11, 1, 2, 12, 5, 10, 14, 7,
        1, 10, 13, 0, 6, 9, 8, 7, 4, 15, 14, 3, 11, 5, 2, 12
      },
      {
        7, 13, 14, 3, 0, 6, 9, 10, 1, 2, 8, 5, 11, 12, 4, 15,
        13, 8, 11, 5, 6, 15, 0, 3, 4, 7, 2, 12, 1, 10, 14, 9,
        10, 6, 9, 0, 12, 11, 7, 13, 15, 1, 3, 14, 5, 2, 8, 4,
        3, 15, 0, 6, 10, 1, 13, 8, 9, 4, 5, 11, 12, 7, 2, 14
      },
      {
        2, 12, 4, 1, 7, 10, 11, 6, 8, 5, 3, 15, 13, 0, 14, 9,
        14, 11, 2, 12, 4, 7, 13, 1, 5, 0, 15, 10, 3, 9, 8, 6,
        4, 2, 1, 11, 10, 13, 7, 8, 15, 9, 12, 5, 6, 3, 0, 14,
        11, 8, 12, 7, 1, 14, 2, 13, 6, 15, 0, 9, 10, 4, 5, 3
      },
      {
        12, 1, 10, 15, 9, 2, 6, 8, 0, 13, 3, 4, 14, 7, 5, 11,
        10, 15, 4, 2, 7, 12, 9, 5, 6, 1, 13, 14, 0, 11, 3, 8,
        9, 14, 15, 5, 2, 8, 12, 3, 7, 0, 4, 10, 1, 13, 11, 6,
        4, 3, 2, 12, 9, 5, 15, 10, 11, 14, 1, 7, 6, 0, 8, 13
      },
      {
        4, 11, 2, 14, 15, 0, 8, 13, 3, 12, 9, 7, 5, 10, 6, 1,
        13, 0, 11, 7, 4, 9, 1, 10, 14, 3, 5, 12, 2, 15, 8, 6,
        1, 4, 11, 13, 12, 3, 7, 14, 10, 15, 6, 8, 0, 5, 9, 2,
        6, 11, 13, 8, 1, 4, 10, 7, 9, 5, 0, 15, 14, 2, 3, 12
      },
      {
        13, 2, 8, 4, 6, 15, 11, 1, 10, 9, 3, 14, 5, 0, 12, 7,
        1, 15, 13, 8, 10, 3, 7, 4, 12, 5, 6, 11, 0, 14, 9, 2,
        7, 11, 4, 1, 9, 12, 14, 2, 0, 6, 10, 13, 15, 3, 5, 8,
        2, 1, 14, 7, 4, 10, 8, 13, 15, 12, 9, 0, 3, 5, 6, 11
      }
    };

    //P Box Table  
    private int[] _pbox = new int[]
    {
      16, 7, 20, 21, 29, 12, 28, 17, 1, 15, 23, 26, 5, 18, 31, 10,
      2, 8, 24, 14, 32, 27, 3, 9, 19, 13, 30, 6, 22, 11, 4, 25
    };

    //Final Permutation Table  
    private int[] _fp = new int[]
    {
      40, 8, 48, 16, 56, 24, 64, 32, 39, 7, 47, 15, 55, 23, 63, 31,
      38, 6, 46, 14, 54, 22, 62, 30, 37, 5, 45, 13, 53, 21, 61, 29,
      36, 4, 44, 12, 52, 20, 60, 28, 35, 3, 43, 11, 51, 19, 59, 27,
      34, 2, 42, 10, 50, 18, 58, 26, 33, 1, 41, 9, 49, 17, 57, 25
    };

    int[] _plaintextbin = new int[5000];
    char[] _ptca;
    int[] _ciphertextbin = new int[5000];
    char[] _ctca;
    int[] _keybin = new int[64];
    char[] _kca;
    int[] _ptextbitslice = new int[64];
    int[] _ctextbitslice = new int[64];
    int[] _ippt = new int[64];
    int[] _ipct = new int[64];
    int[] _ptLpt = new int[32];
    int[] _ptRpt = new int[32];
    int[] _ctLpt = new int[32];
    int[] _ctRpt = new int[32];
    int[] _changedkey = new int[56];
    int[] _shiftedkey = new int[56];
    int[] _tempRpt = new int[32];
    int[] _tempLpt = new int[32];
    int[] _cKey = new int[28];
    int[] _dKey = new int[28];
    int[] _compressedkey = new int[48];
    int[] _ctExpandedLpt = new int[48];
    int[] _ptExpandedRpt = new int[48];
    int[] _xoredRpt = new int[48];
    int[] _xoredLpt = new int[48];
    int[] _row = new int[2];
    int _rowindex;
    int[] _column = new int[4];
    int _columnindex;
    int _sboxvalue;
    int[] _tempsboxarray = new int[4];
    int[] _ptSBoxRpt = new int[32];
    int[] _ctSBoxLpt = new int[32];
    int[] _ctPBoxLpt = new int[32];
    int[] _ptPBoxRpt = new int[32];
    int[] _attachedpt = new int[64];
    int[] _attachedct = new int[64];
    int[] _fppt = new int[64];
    int[] _fpct = new int[64];

    private int GetAscii(char ch)
    {
      int n = ch;
      return n;
    }

    private int ConvertTextToBits(char[] chararray, int[] savedarray)
    {
      var j = 0;
      for (var i = 0; i < chararray.Length; ++i)
      {
        var ba = BitArray.ToBits(GetAscii(chararray[i]), 8);
        j = i * 8;
        AssignArray1ToArray2B(ba, savedarray, j);
      }

      return (j + 8);
    }

    private void AssignArray1ToArray2B(int[] array1, int[] array2, int fromIndex)
    {
      int x, y;
      for (x = 0, y = fromIndex; x < array1.Length; ++x, ++y)
        array2[y] = array1[x];
    }

    private int AppendZeroes(int[] appendedarray, int len)
    {
      int zeroes;
      if (len % 64 != 0)
      {
        zeroes = (64 - (len % 64));

        for (var i = 0; i < zeroes; ++i)
          appendedarray[len++] = 0;
      }

      return len;
    }

    private void Discard8ThBitsFromKey()
    {
      for (int i = 0, j = 0; i < 64; i++)
      {
        if ((i + 1) % 8 == 0)
          continue;
        _changedkey[j++] = _keybin[i];
      }
    }

    private void AssignChangedKeyToShiftedKey()
    {
      for (var i = 0; i < 56; i++)
      {
        _shiftedkey[i] = _changedkey[i];
      }
    }

    private void InitialPermutation(int[] sentarray, int[] savedarray)
    {
      int tmp;
      for (var i = 0; i < 64; i++)
      {
        tmp = _ip[i];
        savedarray[i] = sentarray[tmp - 1];
      }
    }

    private void DivideIntoLptAndRpt(int[] sentarray, int[] savedLpt, int[] savedRpt)
    {
      for (int i = 0, k = 0; i < 32; i++, ++k)
      {
        savedLpt[k] = sentarray[i];
      }

      for (int i = 32, k = 0; i < 64; i++, ++k)
      {
        savedRpt[k] = sentarray[i];
      }
    }

    private void SaveTemporaryHpt(int[] fromHpt, int[] toHpt)
    {
      for (var i = 0; i < 32; i++)
      {
        toHpt[i] = fromHpt[i];
      }
    }

    private void DivideIntoCKeyAndDKey()
    {
      for (int i = 0, j = 0; i < 28; i++, ++j)
      {
        _cKey[j] = _shiftedkey[i];
      }

      for (int i = 28, j = 0; i < 56; i++, ++j)
      {
        _dKey[j] = _shiftedkey[i];
      }
    }

    private void CircularLeftShift(int[] hKey)
    {
      int i, firstBit = hKey[0];
      for (i = 0; i < 27; i++)
      {
        hKey[i] = hKey[i + 1];
      }

      hKey[i] = firstBit;
    }

    private void AttachCKeyAndDKey()
    {
      var j = 0;
      for (var i = 0; i < 28; i++)
      {
        _shiftedkey[j++] = _cKey[i];
      }

      for (var i = 0; i < 28; i++)
      {
        _shiftedkey[j++] = _dKey[i];
      }
    }

    private void CompressionPermutation()
    {
      int temp;
      for (var i = 0; i < 48; i++)
      {
        temp = _cpt[i];
        _compressedkey[i] = _shiftedkey[temp - 1];
      }
    }

    private void ExpansionPermutation(int[] hpt, int[] expandedHpt)
    {
      int temp;
      for (var i = 0; i < 48; i++)
      {
        temp = _ept[i];
        expandedHpt[i] = hpt[temp - 1];
      }
    }

    private void XorOperation(int[] array1, int[] array2, int[] array3, int sizeOfTheArray)
    {
      for (var i = 0; i < sizeOfTheArray; i++)
      {
        array3[i] = array1[i] ^ array2[i];
      }
    }

    private void AssignSBoxHpt(int[] temparray, int[] sBoxHptArray, int fromIndex)
    {
      var j = fromIndex;
      for (var i = 0; i < 4; i++)
      {
        sBoxHptArray[j++] = _tempsboxarray[i];
      }
    }

    private void SBoxSubstituion(int[] xoredHpt, int[] sBoxHpt)
    {
      int r, t, j = 0, q = 0;
      for (var i = 0; i < 48; i += 6)
      {
        _row[0] = xoredHpt[i];
        _row[1] = xoredHpt[i + 5];
        _rowindex = BitArray.ToDecimal(_row);

        _column[0] = xoredHpt[i + 1];
        _column[1] = xoredHpt[i + 2];
        _column[2] = xoredHpt[i + 3];
        _column[3] = xoredHpt[i + 4];
        _columnindex = BitArray.ToDecimal(_column);

        t = ((16 * (_rowindex)) + (_columnindex));

        _sboxvalue = _sbox[j++, t];

        _tempsboxarray = BitArray.ToBits(_sboxvalue, 4);

        r = q * 4;

        AssignSBoxHpt(_tempsboxarray, sBoxHpt, r);

        ++q;
      }
    }

    private void PBoxPermutation(int[] sBoxHpt, int[] pBoxHpt)
    {
      int temp;
      for (var i = 0; i < 32; i++)
      {
        temp = _pbox[i];
        pBoxHpt[i] = sBoxHpt[temp - 1];
      }
    }

    private void Swap(int[] tempHpt, int[] hpt)
    {
      for (var i = 0; i < 32; i++)
      {
        hpt[i] = tempHpt[i];
      }
    }

    //Heart of DES  
    private void SixteenRounds()
    {
      int n;

      for (var i = 0; i < 16; i++)
      {
        SaveTemporaryHpt(_ptRpt, _tempRpt);

        n = _clst[i];

        DivideIntoCKeyAndDKey();

        for (var j = 0; j < n; j++)
        {
          CircularLeftShift(_cKey);
          CircularLeftShift(_dKey);
        }

        AttachCKeyAndDKey();

        CompressionPermutation();

        ExpansionPermutation(_ptRpt, _ptExpandedRpt);

        XorOperation(_compressedkey, _ptExpandedRpt, _xoredRpt, 48);

        SBoxSubstituion(_xoredRpt, _ptSBoxRpt);

        PBoxPermutation(_ptSBoxRpt, _ptPBoxRpt);

        XorOperation(_ptPBoxRpt, _ptLpt, _ptRpt, 32);

        Swap(_tempRpt, _ptLpt);
      }
    }

    private void AttachLptAndRpt(int[] savedLpt, int[] savedRpt, int[] attachedPt)
    {
      var j = 0;
      for (var i = 0; i < 32; i++)
      {
        attachedPt[j++] = savedLpt[i];
      }

      for (var i = 0; i < 32; i++)
      {
        attachedPt[j++] = savedRpt[i];
      }
    }

    private void FinalPermutation(int[] fromPt, int[] toPt)
    {
      int temp;
      for (var i = 0; i < 64; i++)
      {
        temp = _fp[i];
        toPt[i] = fromPt[temp - 1];
      }
    }

    //DES Components  
    private void StartEncryption()
    {
      InitialPermutation(_ptextbitslice, _ippt);

      DivideIntoLptAndRpt(_ippt, _ptLpt, _ptRpt);

      AssignChangedKeyToShiftedKey();

      SixteenRounds();

      AttachLptAndRpt(_ptLpt, _ptRpt, _attachedpt);

      FinalPermutation(_attachedpt, _fppt);
    }

    private string ConvertBitsToText(int[] sentarray, int len)
    {
      var finaltext = "";
      int j, k, decimalvalue;
      var tempbitarray = new int[8];

      for (var i = 0; i < len; i += 8)
      {
        for (k = 0, j = i; j < (i + 8); ++k, ++j)
        {
          tempbitarray[k] = sentarray[j];
        }

        decimalvalue = BitArray.ToDecimal(tempbitarray);

        if (decimalvalue == 0)
          break;

        finaltext += (char) decimalvalue;
      }

      return finaltext;
    }

    public string Encrypt(string plaintext, string key)
    {
      string ciphertext = null;

      _ptca = plaintext.ToCharArray();
      _kca = key.ToCharArray();
      int j, k;

      //Converting plain text characters into binary digits  
      var st = ConvertTextToBits(_ptca, _plaintextbin);

      var fst = AppendZeroes(_plaintextbin, st);

      //Converting key characters into binary digits  
      var sk = ConvertTextToBits(_kca, _keybin);

      var fsk = AppendZeroes(_keybin, sk);

      Discard8ThBitsFromKey();

      for (var i = 0; i < fst; i += 64)
      {
        for (k = 0, j = i; j < (i + 64); ++j, ++k)
        {
          _ptextbitslice[k] = _plaintextbin[j];
        }

        StartEncryption();

        for (k = 0, j = i; j < (i + 64); ++j, ++k)
        {
          _ciphertextbin[j] = _fppt[k];
        }
      }

      ciphertext = ConvertBitsToText(_ciphertextbin, fst);

      return ciphertext;
    }

    private void CircularRightShift(int[] hKey)
    {
      int i, lastBit = hKey[27];
      for (i = 27; i >= 1; --i)
      {
        hKey[i] = hKey[i - 1];
      }

      hKey[i] = lastBit;
    }

    private void ReversedSixteenRounds()
    {
      int n;

      for (var i = 0; i < 16; i++)
      {
        SaveTemporaryHpt(_ctLpt, _tempLpt);

        CompressionPermutation();

        ExpansionPermutation(_ctLpt, _ctExpandedLpt);

        XorOperation(_compressedkey, _ctExpandedLpt, _xoredLpt, 48);

        SBoxSubstituion(_xoredLpt, _ctSBoxLpt);

        PBoxPermutation(_ctSBoxLpt, _ctPBoxLpt);

        XorOperation(_ctPBoxLpt, _ctRpt, _ctLpt, 32);

        Swap(_tempLpt, _ctRpt);

        n = _crst[i];

        DivideIntoCKeyAndDKey();

        for (var j = 0; j < n; j++)
        {
          CircularRightShift(_cKey);
          CircularRightShift(_dKey);
        }

        AttachCKeyAndDKey();
      }
    }

    private void StartDecryption()
    {
      InitialPermutation(_ctextbitslice, _ipct);

      DivideIntoLptAndRpt(_ipct, _ctLpt, _ctRpt);

      AssignChangedKeyToShiftedKey();

      ReversedSixteenRounds();

      AttachLptAndRpt(_ctLpt, _ctRpt, _attachedct);

      FinalPermutation(_attachedct, _fpct);
    }

    public string Decrypt(string cipherText, string key)
    {
      string plaintext = null;

      _ctca = cipherText.ToCharArray();
      _kca = key.ToCharArray();
      int j, k;

      //Converting plain text characters into binary digits  
      var st = ConvertTextToBits(_ctca, _ciphertextbin);

      var fst = AppendZeroes(_ciphertextbin, st);

      //Converting key characters into binary digits  
      var sk = ConvertTextToBits(_kca, _keybin);

      var fsk = AppendZeroes(_keybin, sk);

      Discard8ThBitsFromKey();

      for (var i = 0; i < fst; i += 64)
      {
        for (k = 0, j = i; j < (i + 64); ++j, ++k)
        {
          _ctextbitslice[k] = _ciphertextbin[j];
        }

        StartDecryption();

        for (k = 0, j = i; j < (i + 64); ++j, ++k)
        {
          _plaintextbin[j] = _fpct[k];
        }
      }

      plaintext = ConvertBitsToText(_plaintextbin, fst);

      return plaintext;
    }
  }
}