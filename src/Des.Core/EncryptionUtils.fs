namespace Des.Core

open System
module EncryptionUtils =
    open Constants
    open Models 

    let extendText (str : PlainString) : ExtendedString =
        let mutable mutableStr = str
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
    let extractkeyBinaryBlocks (str : BinaryString)     = extractBinaryBlocks str KeyBlockLength

    let extendKeyUnevenBits (str : BinaryString) : BinaryString =
        ""