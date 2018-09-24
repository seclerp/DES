namespace Des
{
  public interface IEncryptor
  {
    string Encode(string plainText, string key);
    string Decode(string cipherText, string key);
  }
}