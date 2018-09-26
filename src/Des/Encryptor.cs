namespace Des
{
  public class Encryptor : IEncryptor
  {
    public string Encode(string plainText, string key)
    {
      throw new System.NotImplementedException();
    }

    public string Decode(string cipherText, string key)
    {
      throw new System.NotImplementedException();
    }


//// -------------------------------------
//
//function runDESEncode(str, key) {
//    str = expandString(str);
//    // str = toBinary(str); not needed, is inside of split 
//    let blocks = splitBlocks(str);
//    blocks = blocks.map(IP);
//
//    key = strToBinary(key, sizeOfChar);
//    key = expandKey(key, 56);
//    key = extendKeyUnevenBits(key);
//
//    let textBinary = DESEncode(blocks, key);
//    let resultText = fromBinary(textBinary, sizeOfChar / 2);
//
//    return resultText;
//}
//
//function runDESDecode(strCodes, key) {
//    // str = expandString(str);
//    let strBinary = codesToBinary(strCodes, sizeOfChar);
//    let blocks = splitBlocksBinary(strBinary);
//    blocks = blocks.map(IP);
//
//    key = strToBinary(key, sizeOfChar);
//    key = expandKey(key, 56);
//    key = extendKeyUnevenBits(key);
//
//    let resultTextBinary = DESDecode(blocks, key);
//    // for 64 = 16 x 4 = 4 chars
//    let resultCodes = fromBinary(resultTextBinary).split(',');
//    let resultChars = resultCodes.map(x => String.fromCharCode(x));
//    let resultText = resultChars.join('');
//    return resultText;
//}
//
//// -----------------------------------
//
//function DESEncode(blocks, originalKey) {
//    let [keyLeft, keyRight] = splitInTwo(keyIPTable);
//    let key;
//    // let key = keyLeft.concat(keyRight).map(i => originalKey[i]);
//    for (let i = 0; i < 16; i++) {
//        [keyLeft, keyRight] = shiftKeysLeft(keyLeft, keyRight, i);
//        let ithKeyPerm = keyLeft.concat(keyRight).map(i => originalKey[i]);
//        key = ithKey(ithKeyPerm);
//        for (let j = 0; j < blocks.length; j++) {
//            let block = blocks[j];
//            let [left, right] = splitInTwo(block);
//            let res = encodeRound(left, right, key);
//            blocks[j] = [].concat(...res);
//        }
//    }
//    // get last used key (for i = 15)
//    // [keyLeft, keyRight] = shiftKeysLeft(keyLeft, keyRight, keyShifts.length - 1);
//    // key = keyLeft.concat(keyRight).map(i => originalKey[i]).join('');
//    let text = blocks.map(x => IPReversed(x).join('')).join('');
//
//    return text;
//}
//
//function DESDecode(blocks, originalKey) {
//    let [keyLeft, keyRight] = splitInTwo(keyIPTable);
//    let key;
//    // let key = keyLeft.concat(keyRight).map(i => originalKey[i]);
//    for (let i = 0; i < 16; i++) {
//        [keyLeft, keyRight] = shiftKeysRight(keyLeft, keyRight, i);
//        let ithKeyPerm = keyLeft.concat(keyRight).map(i => originalKey[i]);
//        key = ithKey(ithKeyPerm);
//        for (let j = 0; j < blocks.length; j++) {
//            let block = blocks[j];
//            let [left, right] = splitInTwo(block);
//            let res = decodeRound(left, right, key);
//            blocks[j] = [].concat(...res);
//        }
//    }
//    let text = blocks.map(x => IPReversed(x).join('')).join('');
//
//    return text;
//}
//
//function encodeRound(left, right, key) {
//    // left, right = 32bit each
//    let tmp = right;
//    right = XOR(left, f(right, key));
//    left = tmp;
//
//    return [left, right];
//}
//
//function decodeRound(left, right, key) {
//    let tmp = left;
//    left = XOR(right, f(left, key));
//    right = tmp;
//
//    return [left, right];
//}
//


  }
}