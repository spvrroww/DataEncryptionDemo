using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataEncryptionDemo
{
    public record EncryptionResult(byte[] Data, string Passphrase, byte[] IV, byte[] salt);
    
}
