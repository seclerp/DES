open System
open Des.Core

[<EntryPoint>]
let main argv =
    let key = "rootttt"
    let inputMessage = "Hello World"
    
    printfn "Plain text:                %s (%i)" inputMessage inputMessage.Length
    
    let extendedMessage = EncryptionUtils.extendText inputMessage
    printfn "Extended text:             %s (%i)" extendedMessage extendedMessage.Length
    
    let binaryMessage = EncryptionUtils.toBinaryString extendedMessage
    printfn "Binary extended text:      %s (%i)" binaryMessage binaryMessage.Length
    
//    let fromBinaryMessage = EncryptionUtils.fromBinaryString binaryMessage
//    printfn "fromBinaryMessage: %A (%A)" extendedMessage extendedMessage.Length
//    
//    let binaryMessageBlocks = EncryptionUtils.extractMessageBinaryBlocks binaryMessage
//    printfn "Binary blocks: %A (%A)" binaryMessageBlocks binaryMessageBlocks.Length
//    
//    let binaryKey = EncryptionUtils.toBinaryMessage key
//    printfn "Binary key: %A (%A)" binaryKey binaryKey.Length
//    
//    let initialKey = EncryptionUtils.extendKeyUnevenBits binaryKey
//    printfn "Initial (extended) key: %A (%A)" initialKey initialKey.Length

    let encryptResult = Encryptor.encrypt inputMessage key
    printfn "Encoded string:            %s (%i)" encryptResult encryptResult.Length
    
    let decryptResult = Encryptor.decrypt encryptResult key
    let decryptResultText = decryptResult |> EncryptionUtils.fromBinaryString
    printfn "Decrypted string (binary): %s (%i)" decryptResult decryptResult.Length
    printfn "Decrypted string (text):   %s (%i)" decryptResultText decryptResultText.Length
    
    0
