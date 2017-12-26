using System;
using System.IO;

namespace NPSHawaii
{
    class RAPGenerator
    {
        private static string RapDirectory = $"{AppDomain.CurrentDomain.BaseDirectory}exdata";

        public static void SaveRap(GameItem Item)
        {
            byte[] rap = GenerateRap(Item);

            if(rap != null)
            {
                Directory.CreateDirectory(RapDirectory);
                File.WriteAllBytes($"{RapDirectory}\\{Item.ContentID}.rap", rap);
            }
        }

        private static byte[] GenerateRap(GameItem Item)
        {
            if(Item.RAP.Length == 32)
            {
                byte[] buffer = new byte[Item.RAP.Length / 2];
                Console.WriteLine(Item.RAP);

                for (int i = 0; buffer.Length > i; i++)
                {
                    buffer[i] = Convert.ToByte(Item.RAP.Substring(i * 2, 2), 16);
                }

                return buffer;
            }
            return null;
        }
    }
}
