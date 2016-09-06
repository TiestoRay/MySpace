using System;
using Abp.Runtime.Security;

namespace SEPInstance.Func
{
    /// <summary>
    /// DES加密解密
    /// </summary>
    public class DesFunc
    {
        /// <summary>
        /// DES解密
        /// </summary>
        /// <param name="code">要解密的字符串</param>
        /// <param name="pass">密钥</param>
        /// <returns></returns>
        public static string Decrypt(string code, string pass = "gsKnGZ041HLL4IM8")
        {
            string result = SimpleStringCipher.Instance.Decrypt(code, pass);
            return result;
        }

        /// <summary>
        /// DES加密
        /// </summary>
        /// <param name="code">要加密的字符串</param>
        /// <param name="pass">密钥</param>
        /// <returns></returns>
        public static string Encrypt(string code, string pass = "gsKnGZ041HLL4IM8")
        {
            string result = SimpleStringCipher.Instance.Encrypt(code, pass);
            return result;
        }

    }
}
