using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;

namespace ProtectUI
{
    public class Captcha
    {
        /// <summary>
        /// 生成随机数
        /// </summary>
        /// <param name="codeLen">数据的长度</param>
        /// <returns></returns>
        public string CreateRandomCode(int codeLen)
        {
            var sCode = "A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z,1,2,3,4,5,6,7,8,9,0";
            var aCode = sCode.Split(',');
            string CAPTCHA = "";
            for (var i = 0; i < codeLen; i++)
            {
                Random rnd = new Random(Guid.NewGuid().GetHashCode());
                int index = rnd.Next(aCode.Length);
                CAPTCHA += aCode[index];
            }
            return CAPTCHA;
        }

        /// <summary>
        /// 根据随机数生成图片
        /// </summary>
        /// <param name="validateCode">图片中的文字</param>
        /// <returns></returns>
        public byte[] CreateValidGraphic(string validateCode)
        {
            Bitmap img = new Bitmap((int)Math.Ceiling(validateCode.Length * 26.0), 40);
            Graphics g = Graphics.FromImage(img);
            try
            {
                Random random = new Random();//生成随机数
                g.Clear(Color.White);//清空图片背景色
                for (int i = 0; i < 25; i++)//画图片的干扰线
                {
                    int x1 = random.Next(img.Width);
                    int x2 = random.Next(img.Width);
                    int y1 = random.Next(img.Height);
                    int y2 = random.Next(img.Height);
                    g.DrawLine(new Pen(GetRandomDeepColor(false)), x1, x2, y1, y2);
                }
                Font font = new Font("微软雅黑", 18, (FontStyle.Bold | FontStyle.Italic));
                //渐变性颜色
                //LinearGradientBrush brush = new LinearGradientBrush(new Rectangle(0, 0, img.Width, img.Height), Color.Blue, Color.DarkRed, 1.2f, true);
                var fontBrush = new SolidBrush(Color.Black);
                //深色
                for (var i = 0; i < validateCode.Length; i++)
                {
                    fontBrush.Color = GetRandomDeepColor(true);
                    g.DrawString(validateCode.Substring(i, 1), font, fontBrush, 1 + (i * 22), 2);
                }
                //g.DrawString(validateCode, font, fontBrush, 3, 2);
                for (int i = 0; i < 100; i++)//画图片的前景干扰点
                {
                    int x = random.Next(img.Width);
                    int y = random.Next(img.Height);
                    img.SetPixel(x, y, GetRandomDeepColor(false));
                }
                g.DrawRectangle(new Pen(Color.Silver), 0, 0, img.Width - 1, img.Height - 1);//画图片的边框线
                MemoryStream stream = new MemoryStream();
                img.Save(stream, ImageFormat.Jpeg);
                return stream.ToArray();//输入图片
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Color GetRandomDeepColor(bool islIghtColour)
        {
            Double sumrgb = 193, r = 0, g = 0, b = 0;
            if (islIghtColour)
            {
                //排除浅色
                while (sumrgb > 192)
                {
                    //得到随机的颜色值 
                    r = Math.Truncate(new Random(Guid.NewGuid().GetHashCode()).NextDouble() * 256);
                    g = Math.Truncate(new Random(Guid.NewGuid().GetHashCode()).NextDouble() * 256);
                    b = Math.Truncate(new Random(Guid.NewGuid().GetHashCode()).NextDouble() * 256);
                    sumrgb = r * 0.299 + g * 0.578 + b * 0.114;
                }
            }
            else
            {
                r = Math.Truncate(new Random(Guid.NewGuid().GetHashCode()).NextDouble() * 256);
                g = Math.Truncate(new Random(Guid.NewGuid().GetHashCode()).NextDouble() * 256);
                b = Math.Truncate(new Random(Guid.NewGuid().GetHashCode()).NextDouble() * 256);
            }
            return Color.FromArgb((Int32)r, (Int32)g, (Int32)b);
        }
    }
}