using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataEncryptionDemo
{
    public record SecurityKeyModel(byte[] password, byte[] key);   
}
