
# System.Security.Cryptography.CryptographicException
System.Security.Cryptography.CryptographicException
Invalid provider type specified.

   at System.Security.Cryptography.Utils.CreateProvHandle(CspParameters parameters, Boolean randomKeyContainer)
   at System.Security.Cryptography.Utils.GetKeyPairHelper(CspAlgorithmType keyType, CspParameters parameters, Boolean randomKeyContainer, Int32 dwKeySize, SafeProvHandle& safeProvHandle, SafeKeyHandle& safeKeyHandle)
   at System.Security.Cryptography.RSACryptoServiceProvider.GetKeyPair()
   at System.Security.Cryptography.RSACryptoServiceProvider..ctor(Int32 dwKeySize, CspParameters parameters, Boolean useDefaultKeySize)
   at System.Security.Cryptography.X509Certificates.X509Certificate2.get_PrivateKey()
   
   Solution : Provide Read/ Write permission to C:\ProgramData\Microsoft\Crypto\RSA\MachineKeys folder
