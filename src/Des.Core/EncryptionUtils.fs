namespace Des.Core

open System
module EncryptionUtils =
    open Constants
    open Models 

    let formatBinaryString (str : BinaryString) (size : int) : BinaryString =
        let mutable mutableStr = str
        while mutableStr.Length % size <> 0 do
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
        |> Seq.map (fun char -> formatBinaryString (Convert.ToString(int char, 2)) (SizeOfBlock / SizeOfChar))
        |> Seq.reduce (+)
    
    let fromBinaryString (text : BinaryString) : ExtendedString =
        extractBinaryBlocks text 8
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