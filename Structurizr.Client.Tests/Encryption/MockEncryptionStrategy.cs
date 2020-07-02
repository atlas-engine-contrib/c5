using System;
using AtlasEngine.Modelling.C5.Encryption;

namespace AtlasEngine.Modelling.C5.Api.Encryption.Tests
{
    internal class MockEncryptionStrategy : EncryptionStrategy
    {
        public override string Type { get; }

        public override string Encrypt(string plaintext)
        {
            var array = plaintext.ToCharArray();
            Array.Reverse(array);
            return new string(array);
        }

        public override string Decrypt(string ciphertext)
        {
            var array = ciphertext.ToCharArray();
            Array.Reverse(array);
            return new string(array);
        }

    }
}