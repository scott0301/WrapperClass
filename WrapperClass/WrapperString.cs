using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

using System.Security.Cryptography;
using System.IO;

namespace Wrapper
{
    public class WrapperString
    {
        public static string IsValidNumberString(string s)
        {
            if (s == string.Empty)
            {
                return "0";
            }
            else return s;
        }
        public static bool IsValid(string s)
        {
            if (s == null) return false;
            if (s == "") return false;

            return true;
        }
        public static string GetSafeStringNumber(string s)
        {
            double fnum = 0;

            if (s == string.Empty)
            {
                fnum = 0;
            }
            else
            {
                double.TryParse(s, out fnum);
            }
            return fnum.ToString();
        }
        public static DateTime Conv_StringToDateTime(string s)
        {
            DateTime dt = Convert.ToDateTime(s);
            return dt;
        }

        public static PointF Conv_StringToPointF(string s)
        {
            PointF pt = new PointF(0, 0);

            s = s.Replace("{", "");
            s = s.Replace("}", "");
            string[] data = s.Split(',');
            string[] axisX= data[0].Split('=');
            string[] axisY = data[1].Split('=');

            pt.X = (float)(Convert.ToDouble(axisX.ElementAt(1)));
            pt.Y = (float)(Convert.ToDouble(axisY.ElementAt(1)));
            return pt;
        }

        public static RectangleF Conv_StringToRectangleF(string s)
        {
            RectangleF rc = new RectangleF();

            s = s.Replace("{", "");
            s = s.Replace("}", "");

            string [] data = s.Split(',');

            string [] x = data[0].Split('=');
            string [] y = data[1].Split('=');
            string [] w = data[2].Split('=');
            string [] h = data[3].Split('=');

            rc.X = (float)(Convert.ToDouble(x.ElementAt(1)));
            rc.Y = (float)(Convert.ToDouble(y.ElementAt(1)));
            rc.Width = (float)(Convert.ToDouble(w.ElementAt(1)));
            rc.Height = (float)(Convert.ToDouble(h.ElementAt(1)));

            return rc;
        }
        public static string Conv_MoneyToNumber(string money)
        {
            return money.Replace(",", "").Replace("₩", "");
        }
        public static string Conv_NumberToMoney(string number)
        {
            number = number.Replace("₩", "");
            decimal dc = Convert.ToDecimal(number);
            return dc.ToString("C");
        }
        public static String AESEncrypt256(string keyWord, String InputText)
        {
            string Password = keyWord;

            RijndaelManaged RijndaelCipher = new RijndaelManaged();

            // 입력받은 문자열을 바이트 배열로 변환  
            byte[] PlainText = System.Text.Encoding.Unicode.GetBytes(InputText);

            // 딕셔너리 공격을 대비해서 키를 더 풀기 어렵게 만들기 위해서   
            // Salt를 사용한다.  
            byte[] Salt = Encoding.ASCII.GetBytes(Password.Length.ToString());

            PasswordDeriveBytes SecretKey = new PasswordDeriveBytes(Password, Salt);

            // Create a encryptor from the existing SecretKey bytes.  
            // encryptor 객체를 SecretKey로부터 만든다.  
            // Secret Key에는 32바이트  
            // Initialization Vector로 16바이트를 사용  
            ICryptoTransform Encryptor = RijndaelCipher.CreateEncryptor(SecretKey.GetBytes(32), SecretKey.GetBytes(16));

            MemoryStream memoryStream = new MemoryStream();

            // CryptoStream객체를 암호화된 데이터를 쓰기 위한 용도로 선언  
            CryptoStream cryptoStream = new CryptoStream(memoryStream, Encryptor, CryptoStreamMode.Write);

            cryptoStream.Write(PlainText, 0, PlainText.Length);

            cryptoStream.FlushFinalBlock();

            byte[] CipherBytes = memoryStream.ToArray();

            memoryStream.Close();
            cryptoStream.Close();

            string EncryptedData = Convert.ToBase64String(CipherBytes);

            return EncryptedData;
        }

        //AES_256 복호화  
        public static String AESDecrypt256(string keyWord, String InputText)
        {
            try
            {

                string Password = keyWord;

                RijndaelManaged RijndaelCipher = new RijndaelManaged();

                byte[] EncryptedData = Convert.FromBase64String(InputText);
                byte[] Salt = Encoding.ASCII.GetBytes(Password.Length.ToString());

                PasswordDeriveBytes SecretKey = new PasswordDeriveBytes(Password, Salt);

                // Decryptor 객체를 만든다.  
                ICryptoTransform Decryptor = RijndaelCipher.CreateDecryptor(SecretKey.GetBytes(32), SecretKey.GetBytes(16));

                MemoryStream memoryStream = new MemoryStream(EncryptedData);

                // 데이터 읽기 용도의 cryptoStream객체  
                CryptoStream cryptoStream = new CryptoStream(memoryStream, Decryptor, CryptoStreamMode.Read);

                // 복호화된 데이터를 담을 바이트 배열을 선언한다.  
                byte[] PlainText = new byte[EncryptedData.Length];

                int DecryptedCount = cryptoStream.Read(PlainText, 0, PlainText.Length);

                memoryStream.Close();
                cryptoStream.Close();

                string DecryptedData = Encoding.Unicode.GetString(PlainText, 0, DecryptedCount);

                return DecryptedData;
            }
            catch
            {
                return "";
            }
        }  
        public static double StringSimilarity(String src, String target)
        {
            int RowLen = src.Length;  // length of sRow
            int ColLen = target.Length;  // length of sCol
            int RowIdx;                // iterates through sRow
            int ColIdx;                // iterates through sCol
            char Row_i;                // ith character of sRow
            char Col_j;                // jth character of sCol
            int cost;                   // cost

            /// Test string length
            if (Math.Max(src.Length, target.Length) > Math.Pow(2, 31))
                throw (new Exception("\nMaximum string length in Levenshtein.iLD is " + Math.Pow(2, 31) + ".\nYours is " + Math.Max(src.Length, target.Length) + "."));

            // Step 1

            if (RowLen == 0)
            {
                return ColLen;
            }

            if (ColLen == 0)
            {
                return RowLen;
            }

            /// Create the two vectors
            int[] v0 = new int[RowLen + 1];
            int[] v1 = new int[RowLen + 1];
            int[] vTmp;



            /// Step 2
            /// Initialize the first vector
            for (RowIdx = 1; RowIdx <= RowLen; RowIdx++)
            {
                v0[RowIdx] = RowIdx;
            }

            // Step 3

            /// Fore each column
            for (ColIdx = 1; ColIdx <= ColLen; ColIdx++)
            {
                /// Set the 0'th element to the column number
                v1[0] = ColIdx;

                Col_j = target[ColIdx - 1];


                // Step 4

                /// Fore each row
                for (RowIdx = 1; RowIdx <= RowLen; RowIdx++)
                {
                    Row_i = src[RowIdx - 1];


                    // Step 5

                    if (Row_i == Col_j)
                    {
                        cost = 0;
                    }
                    else
                    {
                        cost = 1;
                    }

                    // Step 6

                    /// Find minimum
                    int m_min = v0[RowIdx] + 1;
                    int b = v1[RowIdx - 1] + 1;
                    int c = v0[RowIdx - 1] + cost;

                    if (b < m_min)
                    {
                        m_min = b;
                    }
                    if (c < m_min)
                    {
                        m_min = c;
                    }

                    v1[RowIdx] = m_min;
                }

                /// Swap the vectors
                vTmp = v0;
                v0 = v1;
                v1 = vTmp;

            }


            // Step 7

            /// Value between 0 - 100
            /// 0==perfect match 100==totaly different
            /// 
            /// The vectors where swaped one last time at the end of the last loop,
            /// that is why the result is now in v0 rather than in v1
            int max = System.Math.Max(RowLen, ColLen);
            return 100.0 - ((100.0 * v0[RowLen]) / (double)max);
        }
    }
}
