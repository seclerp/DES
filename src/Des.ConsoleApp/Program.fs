open System
open Des.Core

[<EntryPoint>]
let main argv =
    let key = "r00t123"
    let inputMessage = "Hello World"
    
    printfn "Plain text: %A (%A)" inputMessage inputMessage.Length
    
    let extendedMessage = EncryptionUtils.extendText inputMessage
    printfn "Extended text: %A (%A)" extendedMessage extendedMessage.Length
    
    let binaryMessage = EncryptionUtils.toBinaryMessage extendedMessage
    printfn "Binary extended text: %A (%A)" binaryMessage binaryMessage.Length
    
    let binaryMessageBlocks = EncryptionUtils.extractMessageBinaryBlocks binaryMessage
    printfn "Binary blocks: %A (%A)" binaryMessageBlocks binaryMessageBlocks.Length
    
    let binaryKey = EncryptionUtils.toBinaryMessage key
    printfn "Binary key: %A (%A)" binaryKey binaryKey.Length
    
    let initialKey = EncryptionUtils.extendKeyUnevenBits binaryKey
    printfn "Initial (extended) key: %A (%A)" initialKey initialKey.Length
    0
