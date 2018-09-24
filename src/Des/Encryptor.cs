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
    
//    function splitBlocks(str) {
//    let blocks = [];
//    for (let i = 0; i < str.length / lengthOfBlock; i++) {
//        let block = str.substr(i * lengthOfBlock, lengthOfBlock);
//        blocks[i] = strToBinary(block, sizeOfChar);
//    }
//
//    return blocks;
//}
//
//function splitBlocksBinary(str) {
//    let blocks = [];
//    let size = str.length / sizeOfBlock;
//    for (let i = 0; i < size; i++) {
//        blocks[i] = str.substr(i * sizeOfBlock, sizeOfBlock);
//    }
//
//    return blocks;
//}
//
//function splitInTwo(block) {
//    return [
//        block.slice(0, block.length / 2),
//        block.slice(block.length / 2, block.length)
//    ];
//}
//
//function strToBinary(str, size) {
//    return codesToBinary(str.split('').map(x => x.charCodeAt(0)), size);
//}
//
//function codesToBinary(codes, size) {
//    let output = "";
//
//    for (let i = 0; i < codes.length; i++) {
//        let charBinary = codes[i].toString(2);
//        charBinary = expandLeftZero(charBinary, size);
//
//        output += charBinary;
//    }
//
//    return output;
//}
//
//function fromBinary(str) {
//    let regex = new RegExp(`.{${sizeOfChar}}`, "g");
//    let charCodesBinary = str.match(regex);
//    let charCodes = charCodesBinary.map(x => parseInt(x, 2));
//    // let chars = charCodes.map(String.fromCharCode);
//
//    return charCodes.join(',');
//}
//
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
//function XOR(left, right) {
//    result = [];
//    for (let i = 0; i < left.length; i++) {
//        result[i] = left[i] == right[i] ? "0" : "1";
//    }
//
//    return result;
//}

  }
}