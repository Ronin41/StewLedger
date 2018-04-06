using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Windows;

namespace StewLedger.Util
{
    public class Hash
    {
       
        public Hash() { }

        public string GetMd5Hash(string input)
        {
            using (MD5 md5Hash = MD5.Create())
            {
                try
                {
                    byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));
                    StringBuilder stringBuilder = new StringBuilder();

                    for (int i = 0; i < data.Length; i++)
                    {
                        stringBuilder.Append(data[i].ToString("x2"));
                    }

                    return stringBuilder.ToString();
                } catch (Exception e)
                {
                    MessageBox.Show(e.ToString());
                }
            }

            return null;
        }

        public bool VerifyMd5Hash(string input, string hash)
        {
            using (MD5 md5Hash = MD5.Create())
            {
                try
                {
                    string inputHash = GetMd5Hash(input);

                    StringComparer comparer = StringComparer.OrdinalIgnoreCase;

                    if (0 == comparer.Compare(inputHash, hash))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                   
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.ToString());
                }
            }
            
            return false;
        }


    }
}
