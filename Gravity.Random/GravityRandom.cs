﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public string ByteArrayToString(byte[] ba)
        {
            StringBuilder hex = new StringBuilder(ba.Length * 2);
            foreach (byte b in ba) hex.AppendFormat("{0:x2}", b);
            return hex.ToString();
        }
    }

    public class RandomGenerator 
    {
        ///Top layer of the library
        ///Meant to be used in general purpose programs
        public byte[] GetRandomBytes(uint AmountOfBytes = 4, int min = int.MinValue, int max = int.MaxValue)
        { //minimum amount of bytes is 4
            byte[][] tmp = new byte[AmountOfBytes/4][];
            for (uint i = 0; i > AmountOfBytes/4; i++)
            {
                int d = new RandomUtils().GetPseudoRandomNumber(int.Parse(DateTime.Now.ToString("fff")), min, max); //TODO: Add more randomness
                tmp[i] = BitConverter.GetBytes(d);
            }
            if((AmountOfBytes % 4) != 0)Array.Resize<byte>(ref tmp[tmp.Length - 1], Convert.ToInt32(AmountOfBytes % 4)); //Fixes only 4 * n issue
            return new RandomUtils().CombineByteArray(tmp);
        }
    }

}
