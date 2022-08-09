﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Utils
{
    public class HashHelper
    {
        private SHA256 hasher;
        public HashHelper()
        {
            hasher = SHA256.Create();
        }
        public string GetHash(string text)
        {
            byte[] bytes = hasher.ComputeHash(Encoding.UTF8.GetBytes(text));
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++)
            {
                sb.Append(bytes[i].ToString("x2"));
            }
            return sb.ToString();
        }
    }
}
