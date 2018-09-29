namespace Des.Core

open System
open Models
module Encryptor =
    let private xor (left : BinaryString) (right : BinaryString) : BinaryString =
        Seq.map2 (fun x y -> if (x = y) then "0" else "1") left right
        |> Seq.reduce (+)

    let private e (right : BinaryString) : BinaryString =
        PermutationUtils.performExpandPermutation right
    
    let private s (input : BinaryString array) : BinaryString array =
        let result = Array.create<BinaryString> input.Length ""
    
        input
        |> Seq.iteri (
            fun i block -> 
                let (first, last) = (Seq.head block |> string, Seq.last block |> string)
                let j = int(Convert.ToInt32(first + last, 2))
                let k = Convert.ToInt32(block.[1..block.Length - 2] |> Seq.map string |> Seq.reduce (+), 2)
                
                let tableValue = PermutationUtils.extractSValue i j k
                let binary = Convert.ToString(tableValue, 2)
                let formattedBinary = EncryptionUtils.formatBinaryString binary 4
                result.[i] <- formattedBinary
        )
        
        result
    
    let private f (right : BinaryString) (key : BinaryString) =
        let expandedRight = e right
        let _xor = xor expandedRight key
        let bBlocks = EncryptionUtils.extractBinaryBlocks (_xor) 6
        
        s bBlocks |> Seq.reduce (+) |> PermutationUtils.performPermutation
        
    let private encodeRound (left : BinaryString) (right : BinaryString) (key : BinaryString) 
        : (BinaryString * BinaryString) =
        let newRight = xor left (f right key)
        (right, newRight)
        
    let private decodeRound (left : BinaryString) (right : BinaryString) (key : BinaryString) 
        : (BinaryString * BinaryString) =
        let newLeft = xor right (f left key)
        (newLeft, left)

    let encrypt (plainText : string) (key : string) : (BinaryString * Entropy) =
        let binaryMessageBlocks =
            plainText 
            |> EncryptionUtils.extendText
            |> EncryptionUtils.toBinaryString
            |> EncryptionUtils.extractMessageBinaryBlocks
            |> Seq.map PermutationUtils.performInitialPermutation
            |> Seq.toArray

        let initialKey =
            key
            |> EncryptionUtils.toBinaryString
            |> Seq.take Constants.KeyLength 
            |> Seq.map string
            |> Seq.reduce (+)
            |> EncryptionUtils.validateKey
            |> EncryptionUtils.extendKeyUnevenBits

        let mutable resultBlocks = binaryMessageBlocks.[*]
        let mutable (keyLeft, keyRight) = PermutationUtils.getInitialKeyPermutationValues()
        
        [0..Constants.QuantityOfRounds - 1] 
        |> Seq.iter (
            fun i ->
                keyLeft <- PermutationUtils.shiftKeyPartsLeft keyLeft i
                keyRight <- PermutationUtils.shiftKeyPartsLeft keyRight i
                
                // 56 bit
                let key56 = 
                    [keyLeft; keyRight]  
                    |> Array.concat 
                    |> Seq.map (fun item -> string initialKey.[item - 1])
                    |> Seq.reduce (+)
                    
                // Get 48 bit key
                let key48 = PermutationUtils.performEncodeKeyPermutation key56
                
                resultBlocks
                |> Seq.iteri (
                    fun i block -> 
                        let (l, r) = (
                            block.[..block.Length / 2 - 1], 
                            block.[block.Length / 2..]
                        )
                        
                        let (left, right) = encodeRound l r key48
                        resultBlocks.[i] <- left + right
                )
        )
        
        let permutedBlocks = 
            resultBlocks 
            |> Seq.map PermutationUtils.performReverseInitialPermutation

        let cipher = permutedBlocks |> Seq.reduce (+)
        let blocksEntropy = permutedBlocks |> Seq.map EncryptionUtils.computeEntropy
            
        (cipher, blocksEntropy)

    let decrypt (cipherText : BinaryString) (key : string) : BinaryString =
        let binaryMessageBlocks =
            cipherText 
            |> EncryptionUtils.extractMessageBinaryBlocks
            |> Seq.map PermutationUtils.performInitialPermutation
            |> Seq.toArray

        let initialKey =
            key
            |> EncryptionUtils.toBinaryString
            |> Seq.take Constants.KeyLength 
            |> Seq.map string
            |> Seq.reduce (+)
            |> EncryptionUtils.extendKeyUnevenBits

        let mutable resultBlocks = binaryMessageBlocks.[*]
        let mutable (keyLeft, keyRight) = PermutationUtils.getInitialKeyPermutationValues()
        
        [0..Constants.QuantityOfRounds - 1] 
        |> Seq.iter (
            fun i ->
                keyLeft <- PermutationUtils.shiftKeyPartsRight keyLeft i
                keyRight <- PermutationUtils.shiftKeyPartsRight keyRight i
                
                // 56 bit
                let key56 = 
                    [keyLeft; keyRight]  
                    |> Array.concat 
                    |> Seq.map (fun item -> string initialKey.[item - 1])
                    |> Seq.reduce (+)
                    
                // Get 48 bit key
                let key48 = PermutationUtils.performEncodeKeyPermutation key56
                
                resultBlocks
                |> Seq.iteri (
                    fun i block -> 
                        let (l, r) = (
                            block.[..block.Length / 2 - 1], 
                            block.[block.Length / 2..]
                        )
                        
                        let (left, right) = decodeRound l r key48
                        resultBlocks.[i] <- left + right
                )
        )
        
        resultBlocks 
        |> Seq.map (fun block -> block |> PermutationUtils.performReverseInitialPermutation)
        |> Seq.reduce (+)