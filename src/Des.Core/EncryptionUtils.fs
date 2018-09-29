namespace Des.Core

open System
module EncryptionUtils =
    open Constants
    open Models 

    let isWeakKey (key : BinaryString) =
        [
            "00000000000000000000000000000000000000000000000000000000";
            "11111111111111111111111111111111111111111111111111111111";
            "00000000000000000000000000001111111111111111111111111111";
            "11111111111111111111111111110000000000000000000000000000";
        ] 
        |> Seq.exists (fun weak -> weak = key)

    let validateKey =
        function
        | key when isWeakKey key -> raise (new Exception("Given key is weak"))
        | key -> key

    let formatBinaryString (str : BinaryString) (size : int) : BinaryString =
        let mutable mutableStr = str
        while mutableStr.Length < size do
            mutableStr <- "0" + mutableStr
        mutableStr

    let extendText (str : PlainString) : ExtendedString =
        let mutable mutableStr = string str
        while (mutableStr.Length * SizeOfChar) % SizeOfBlock <> 0 do
            mutableStr <- mutableStr + Extender
        mutableStr

    let extractBinaryBlocks (str : BinaryString) (blockLengthBits : int) : BinaryString array =
        [0 .. blockLengthBits .. str.Length - 1]
        |> Seq.map (fun i -> str.Substring(i, blockLengthBits))
        |> Seq.toArray

    let toBinaryString (text : ExtendedString) : BinaryString =
        text 
        |> Seq.map (fun char -> formatBinaryString (Convert.ToString(int char, 2)) SizeOfChar)
        |> Seq.reduce (+)
    
    let fromBinaryString (text : BinaryString) : ExtendedString =
        extractBinaryBlocks text SizeOfChar
        |> Seq.map (fun block -> Convert.ToInt32(block, 2) |> char |> string)
        |> Seq.reduce (+)

    let extractMessageBinaryBlocks (str : BinaryString) = extractBinaryBlocks str MessageBlockLength
    let extractKeyBinaryBlocks (str : BinaryString)     = extractBinaryBlocks str KeyBlockLength

    let extendKeyUnevenBits (key : BinaryString) : BinaryString =
        let keyBlocks = extractKeyBinaryBlocks key
        keyBlocks 
        |> Seq.map (fun block ->
            let countOf1 = block |> Seq.filter (fun binaryDigit -> binaryDigit = '1') |> Seq.length
            match countOf1 % 2 with
            | 0 -> block + "1"
            | _ -> block + "0"
        )
        |> Seq.reduce (+)

    let computeEntropy (block : BinaryString) : float =
        assert (block.Length = 64)

        let inline log2 a : double = Math.Log(double a, double 2)

        let onesCount = 
            block 
            |> Seq.filter (fun item -> item = '1') 
            |> Seq.length

        let pOne = double onesCount / double block.Length
        let pZero = double 1 - pOne

        let entropy = (double -1 * (pOne * (log2 pOne)) + pZero * (log2 pZero))
        entropy

