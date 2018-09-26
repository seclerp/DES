namespace Des.Core

open System
module EncryptionUtils =
    open Constants
    open Models 

    let extendText (str : PlainString) : ExtendedString =
        let mutable mutableStr = string str
        while (mutableStr.Length * SizeOfChar) % SizeOfBlock <> 0 do
            mutableStr <- mutableStr + Extender
        mutableStr

    let toBinaryMessage (text : ExtendedString) : BinaryString =
        let expandBinaryString (binaryStr : BinaryString) = 
            let mutable mutableStr = binaryStr
            while (mutableStr.Length * SizeOfChar) % SizeOfBlock <> 0 do
                mutableStr <- "0" + mutableStr
            mutableStr
        text 
        |> Seq.map (fun char -> expandBinaryString (Convert.ToString(int char, 2)))
        |> Seq.fold (+) ""
    
    let extractBinaryBlocks (str : BinaryString) (blockLengthBits : int) : BinaryString array =
        [0 .. blockLengthBits .. str.Length - 1]
        |> Seq.map (fun i -> str.Substring(i, blockLengthBits))
        |> Seq.toArray

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
