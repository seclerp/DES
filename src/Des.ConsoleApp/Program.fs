open System
open Des.Core

[<EntryPoint>]
let main argv =
    let key = "root"
    let inputMessage = "Worl221312331232213213121231231"
    
    printfn "Plain text:                %s (%i)" inputMessage inputMessage.Length
    
    let extendedMessage = EncryptionUtils.extendText inputMessage
    printfn "Extended text:             %s (%i)" extendedMessage extendedMessage.Length
    
    let binaryMessage = EncryptionUtils.toBinaryString extendedMessage
    printfn "Binary extended text:      %s (%i)" binaryMessage binaryMessage.Length

    let encryptResult = Encryptor.encrypt inputMessage key
    printfn "Encoded string:            %s (%i)" encryptResult encryptResult.Length
    
    let decryptResult = Encryptor.decrypt encryptResult key
    let decryptResultText = decryptResult |> EncryptionUtils.fromBinaryString
    printfn "Decrypted string (binary): %s (%i)" decryptResult decryptResult.Length
    printfn "Decrypted string (text):   %s (%i)" decryptResultText decryptResultText.Length
    
    0
