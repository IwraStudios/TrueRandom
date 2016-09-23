using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Diagnostics;
using Gravity.Base32;
using System.IO;

namespace Gravity
{
    class Random_Gravity
    {
        static int length = 20;
        const string Alpha1 = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
        const string Alpha2 = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890!@#$%^&*()_-+=";
        const string Alpha3 = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890!#$%&'()*+,-./:;<=>?@[]^_`{|}~";
        const string path = @"C:\Users\NullFunction\Desktop\has.txt";
        static void Main(string[] args)
        {
            string a = GenPass(Alpha1, length);
            string b = GenPass(Alpha2, length);
            string c = GenPass(Alpha3, length);
            //byte[] b = CombineByteArray(a);
            //string e = Convert.ToBase64String(b);
            //string c = Utilities.Base32.ToBase32String(b);
            //Console.WriteLine("Base 64 = " + e);
            Console.WriteLine("String [A-Z][0-9] = " + a);
            Console.WriteLine("String [A-Z][0-9][SpChCasual] = " + b);
            Console.WriteLine("String [A-Z][0-9][SpChAll] = " + c);
            using (StreamWriter writetext = new StreamWriter(path))
            {
                writetext.WriteLine(a);
                writetext.WriteLine(b);
                writetext.WriteLine(c);
            }
            Thread.Sleep(int.MaxValue);
        }

        public static string GenPass(string valid, int length1)
        {

            //const string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            StringBuilder res = new StringBuilder();
            Random rnd = new Random();
            while (0 < length1--)
            {
                //Thread.Sleep(GetPseudoRandomNumber(0, 100, 1000));
                char c = valid[rnd.Next(0, valid.Length)];
                if ("+-&|!(){}[]^\"~*?:\\".Contains(c))
                {
                    //res.Append("\\\\");
                }
                res.Append(c); /*GetPseudoRandomNumber(int.Parse(System.DateTime.Now.ToString("fff"))0 ,valid.Length)*/
            }
            return res.ToString();
        }

        public static byte[] CombineByteArray(Byte[][] bytes)
        {
            byte[] start = new byte[0];
            foreach (byte[] a in bytes)
            {
                int b = start.Length;
                Array.Resize<byte>(ref start, (start.Length + a.Length));
                Array.Copy(a, 0, start, b, a.Length);
            }
            return start;
        }

        public static byte[] GetRandomNumber(int min = 0, int max = 100000)
        {
            int i = Int32.Parse(System.DateTime.Now.ToString("fff"));
            int d = GetPseudoRandomNumber(i, min, max);
            byte[] b = System.BitConverter.GetBytes(d);
            return b;
        }

        public static int GetPseudoRandomNumber(int input = 0, int min = 0, int max = int.MaxValue)
        {
            Random random;
            if (input == 0)
            {
                random = new Random();
            }
            else
            {
                random = new Random(input);
            }
            return random.Next(min, max);
        }

        static int ConvertToInt(byte[] bytes)
        {
            if (BitConverter.IsLittleEndian)
                Array.Reverse(bytes);

            return BitConverter.ToInt32(bytes, 0);
        }

        public static string ByteArrayToString(byte[] ba)
        {
            StringBuilder hex = new StringBuilder(ba.Length * 2);
            foreach (byte b in ba)
                hex.AppendFormat("{0:x2}", b);
            return hex.ToString();
        }

        public static string RandomizeString(string a)
        {

            return null;
        }


    }
}
