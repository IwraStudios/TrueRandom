using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gravity.TrueRandom
{
    public class RandomUtils 
    {
        ///The buttom layer of the library
        ///Meant to be used in advanced programs
        public byte[] CombineByteArray(Byte[][] bytes)
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
        public int GetPseudoRandomNumber(int input = 0, int min = 0, int max = int.MaxValue)
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
        public int ByteArrayToInt(byte[] bytes)
        {
            if (BitConverter.IsLittleEndian) Array.Reverse(bytes);

            return BitConverter.ToInt32(bytes, 0);
        }

        public string ByteArrayToString(byte[] ba)
        {
            StringBuilder hex = new StringBuilder(ba.Length * 2);
            foreach (byte b in ba)
                hex.AppendFormat("{0:x2}", b);
            return hex.ToString();
        }
    }

    public class RandomGenerator 
    {
        ///Top layer of the library
        ///Meant to be used in general purpose programs

    }

}
