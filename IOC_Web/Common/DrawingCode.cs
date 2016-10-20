using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
 
using ZXing;
using ZXing.Common;
using ZXing.QrCode;
using ZXing.QrCode.Internal;

namespace IOC_Web.Common
{
    /// <summary>
    /// 二维码
    /// </summary>
    public class QrCode
    {
        /// <summary>
        /// 生成二维码图片
        /// </summary>
        /// <param name="param"></param>
        /// <param name="filePath"></param>
        public static void GetQrCode(QrCodeParam param, string filePath)
        {
            var wr = CreateCode(param);
            Bitmap img = wr.Write(param.Content);

            img = DrawLogo(img, param.imgPath);

            img.Save(filePath, System.Drawing.Imaging.ImageFormat.Jpeg);
        }

        /// <summary>
        /// 生成二维码图片
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public static MemoryStream GetQrCode(QrCodeParam param)
        {
            var wr = CreateCode(param);
            Bitmap img = wr.Write(param.Content);

            img = DrawLogo(img, param.imgPath);

            using (MemoryStream ms = new System.IO.MemoryStream())
            {
                img.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                return ms;
            }
        }

        private static BarcodeWriter CreateCode(QrCodeParam param)
        {
            QrCodeEncodingOptions qrEncodeOption = new QrCodeEncodingOptions();
            qrEncodeOption.CharacterSet = "UTF-8";
            qrEncodeOption.Height = param.Height;
            qrEncodeOption.Width = param.Width;
            qrEncodeOption.Margin = param.Margin;
            qrEncodeOption.ErrorCorrection = ErrorCorrectionLevel.H;
            qrEncodeOption.DisableECI = true;

            ZXing.BarcodeWriter wr = new BarcodeWriter();
            wr.Format = BarcodeFormat.QR_CODE;
            wr.Options = qrEncodeOption;
            return wr;
        }

        private static Bitmap DrawLogo(Bitmap img, string logoPath)
        {
            if (!string.IsNullOrWhiteSpace(logoPath) && File.Exists(logoPath))
            {
                Bitmap logoImg = Bitmap.FromFile(logoPath) as Bitmap;
                Graphics g = Graphics.FromImage(img);
                Rectangle logoRec = new Rectangle(); //设置图片的大小和绘制位置
                logoRec.Width = img.Width / 6;
                logoRec.Height = img.Height / 6;
                logoRec.X = img.Width / 2 - logoRec.Width / 2; //中心点
                logoRec.Y = img.Height / 2 - logoRec.Height / 2;
                g.DrawImage(logoImg, logoRec);
            }
            return img;
        }
    }

    /// <summary>
    /// 条码
    /// </summary>
    public class BarCode
    {
        /// <summary>
        /// 生成条码图片
        /// </summary>
        /// <param name="param"></param>
        /// <param name="filePath"></param>
        public static void GetBarCode(BarCodeParam param, string filePath)
        {
            var wr = CreateCode(param);
            Bitmap img = wr.Write(param.Content);
            img.Save(filePath, System.Drawing.Imaging.ImageFormat.Jpeg);
        }

        /// <summary>
        /// 生成条图片
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public static MemoryStream GetBarCode(BarCodeParam param)
        {
            var wr = CreateCode(param);
            Bitmap img = wr.Write(param.Content);
            using (MemoryStream ms = new System.IO.MemoryStream())
            {
                img.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                return ms;
            }
        }

        /// <summary>
        /// 生成条图片
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public static string GetBarCodeString(BarCodeParam param)
        {
            var wr = CreateCode(param);
            //Bitmap img = new Bitmap(param.Width, param.Height);
            //img = wr.Write(param.Content);
            Bitmap img = wr.Write(param.Content);
            using (MemoryStream ms = new System.IO.MemoryStream())
            {
                img.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                byte[] arr = new byte[ms.Length];
                ms.Position = 0;
                ms.Read(arr, 0, (int)ms.Length);
                ms.Close();
                return Convert.ToBase64String(arr);
            }
        }

        private static BarcodeWriter CreateCode(BarCodeParam param)
        {
            EncodingOptions encodeOption = new EncodingOptions();
            encodeOption.Height = param.Height;
            encodeOption.PureBarcode = param.IsBuildCode;
            //encodeOption.Hints.Add(EncodeHintType.WIDTH, 10);

            BarcodeWriter wr = new BarcodeWriter();
            wr.Options = encodeOption;
            switch (param.CodeFormat)
            {
                case BarCodeFormat.CODE_39:
                    wr.Format = BarcodeFormat.CODE_39;
                    break;
                case BarCodeFormat.CODE_93:
                    wr.Format = BarcodeFormat.CODE_93;
                    break;
                case BarCodeFormat.CODE_128:
                    wr.Format = BarcodeFormat.CODE_128;
                    break;
                case BarCodeFormat.EAN_13:
                    wr.Format = BarcodeFormat.EAN_13;
                    break;
                default:
                    throw new Exception("条码类型不能为空");
            }
            return wr;
        }
    }

    /// <summary>
    /// 二维码参数
    /// </summary>
    public class QrCodeParam
    {
        public QrCodeParam()
        {
            Margin = 1;
            Width = 400;
            Height = 400;
        }

        public int Width { get; set; }
        public int Height { get; set; }
        public string Content { get; set; }
        public int Margin { get; set; }
        public string imgPath { get; set; }
    }

    /// <summary>
    /// 条码参数
    /// </summary>
    public class BarCodeParam
    {
        public BarCodeParam()
        {
            IsBuildCode = false;
            Height = 120;
            CodeFormat = BarCodeFormat.CODE_128;
        }

        public int Height { get; set; }
        public string Content { get; set; }
        public bool IsBuildCode { get; set; }
        public BarCodeFormat CodeFormat { get; set; }
    }

    public enum BarCodeFormat
    {
        CODE_39 = 0,
        CODE_93 = 1,
        CODE_128 = 2,
        EAN_13 = 3
    }
}
