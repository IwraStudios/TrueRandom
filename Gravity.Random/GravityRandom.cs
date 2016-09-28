using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Gravity.TrueRandom
{
    public class RandomConstants
    {

    }
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
            return new Random(input).Next(min, max);
        }
        public int ByteArrayToInt(byte[] bytes)
        {
            if (BitConverter.IsLittleEndian) Array.Reverse(bytes);
            return BitConverter.ToInt32(bytes, 0);
        }

        public string ByteArrayToHex(byte[] ba)
        {
            StringBuilder hex = new StringBuilder(ba.Length * 2);
            foreach (byte b in ba) hex.AppendFormat("{0:x2}", b);
            return hex.ToString();
        }
        public string ByteArrayToString(byte[] ba){ //UTF8 Only for now TODO: added other encodings
          return System.Text.Encoding.UTF8.GetString(ba, 0, ba.Length);
        }
        public int InefficientRandomInt(int min = int.MinValue, int max = int.MaxValue){
          Thread.Sleep(this.GetPseudoRandomNumber(0, 1, 50));
          int time = Int32.Parse(System.DateTime.Now.ToString("fff")); //Same
          int ran = this.GetPseudoRandomNumber(time, min, max);
        }
        public int EfficientRandomInt(int min = int.MinValue, int max = int.MaxValue){
          //Thread.Sleep(this.GetPseudoRandomNumber(0, 1, 50));
          int time = Int32.Parse(System.DateTime.Now.ToString("fff")); //Same
          int ran = this.GetPseudoRandomNumber(time, min, max);
        }
    }

    public class RandomGenerator
    {
        ///Top layer of the library
        ///Meant to be used in general purpose programs
        public byte[] GetRandomBytes(uint AmountOfBytes = 4, int min = int.MinValue, int max = int.MaxValue)
        {
            if (AmountOfBytes < 4) AmountOfBytes = 4;
            byte[][] tmp = new byte[AmountOfBytes/4][];
            for (uint i = 0; i > AmountOfBytes/4; i++)
            {
                int time = Int32.Parse(System.DateTime.Now.ToString("fff")); //Same
                int d = new RandomUtils().GetPseudoRandomNumber(time, min, max); //TODO: Add more randomness
                //byte[] b = System.BitConverter.GetBytes(d);
                tmp[i] = System.BitConverter.GetBytes(d);
            }
            if((AmountOfBytes % 4) != 0)Array.Resize<byte>(ref tmp[tmp.Length - 1], Convert.ToInt32(AmountOfBytes % 4)); //Fixes only 4 * n issue
            return new RandomUtils().CombineByteArray(tmp);
        }
    }

}
