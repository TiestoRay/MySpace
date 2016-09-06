using System;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace SEPInstance.Func
{
    /// <summary>
    /// 验证码的生成
    /// </summary>
    public class ValidateCodeFunc
    {
        /// <summary>
        /// 创建验证码字母
        /// </summary>
        /// <param name="length">验证码的长度</param>
        /// <returns></returns>
        public static string CreateValidateCode(int length = 4)
        {
            string chkCode = string.Empty;

            //验证码的字符集，去掉了一些容易混淆的字符 
            char[] character = { '2', '3', '4', '5', '6', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'J', 'K', 'L', 'M', 'N', 'P', 'R', 'S', 'T', 'W', 'X', 'Y' };
            Random rnd = new Random();
            //生成验证码字符串 
            for (int i = 0; i < length; i++)
            {
                chkCode += character[rnd.Next(character.Length)];
            }
            return chkCode;
        }

        /// <summary>
        /// 根据输入的验证码创建验证码图片
        /// </summary>
        /// <param name="chkCode">验证码字符</param>
        /// <returns></returns>
        public static byte[] CreateValidateCodePic(string chkCode)
        {
            //颜色列表，用于验证码、噪线、噪点 
            Color[] color = { Color.Black, Color.Red, Color.Blue, Color.Green, Color.Orange, Color.Brown, Color.Brown, Color.DarkBlue };
            //字体列表，用于验证码 
            string[] font = { "Times New Roman", "MS Mincho", "Book Antiqua", "Gungsuh", "PMingLiU", "Impact" };
            Bitmap bmp = new Bitmap(100, 40);
            Graphics g = Graphics.FromImage(bmp);
            g.Clear(Color.White);
            Random rnd = new Random();
            //画噪线 
            for (int i = 0; i < 10; i++)
            {
                int x1 = rnd.Next(100);
                int y1 = rnd.Next(40);
                int x2 = rnd.Next(100);
                int y2 = rnd.Next(40);
                Color clr = color[rnd.Next(color.Length)];
                g.DrawLine(new Pen(clr), x1, y1, x2, y2);
            }
            //画验证码字符串 
            for (int i = 0; i < chkCode.Length; i++)
            {
                string fnt = font[rnd.Next(font.Length)];
                Font ft = new Font(fnt, 18);
                Color clr = color[rnd.Next(color.Length)];
                g.DrawString(chkCode[i].ToString(), ft, new SolidBrush(clr), (float)i * 20 + 8, (float)8);
            }
            //画噪点 
            for (int i = 0; i < 100; i++)
            {
                int x = rnd.Next(bmp.Width);
                int y = rnd.Next(bmp.Height);
                Color clr = color[rnd.Next(color.Length)];
                bmp.SetPixel(x, y, clr);
            }

            //将验证码图片写入内存流，并将其以 "image/Png" 格式输出 
            MemoryStream ms = new MemoryStream();
            try
            {
                bmp.Save(ms, ImageFormat.Png);
                return ms.ToArray();

            }
            finally
            {
                //显式释放资源 
                bmp.Dispose();
                g.Dispose();
            }
        }
    }
}
