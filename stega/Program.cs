using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;

namespace stega
{
   


    class Program
    {
        const string imgpath = @"C:\Users\Dan\source\repos\stega\stega\image.png";
        static void Main(string[] args)
        {
            Console.WriteLine(@"1 - Encode
2 - Decode
How should I procced?");
            string choice = Console.ReadLine();
            if (choice =="1")
            {
                Console.WriteLine("Your message?");
                string mess = Console.ReadLine();
                Console.WriteLine("Your image?");
                string filepath = Console.ReadLine();
                Console.WriteLine(Encode(mess, filepath));
            }
            if (choice =="2")
            {
                Console.WriteLine("Your image?");
                string filepath = Console.ReadLine();
                Console.WriteLine(Decode(filepath));
            }
            Console.ReadLine();
        }
        private static string Encode(string text, string imagepath) 
        {
            Bitmap encodedbm = new Bitmap(Image.FromFile(imagepath));
            string new_imagepath = imagepath.Split('.')[0] + "Encode" + ".png";
            for (int i = 0; i < encodedbm.Width; i++)
            {
                for (int j = 0; j < encodedbm.Height; j++)
                {
                    Color pixel = encodedbm.GetPixel(i, j);
                    if (i < 1 && j < text.Length)
                    {
                        char letter = Convert.ToChar(text.Substring(j, 1));
                        int value = Convert.ToInt32(letter);
                        encodedbm.SetPixel(i, j, Color.FromArgb(pixel.R, pixel.G, pixel.B, value));
                        encodedbm.Save(new_imagepath);

                    }
                    if (i == encodedbm.Width - 1 && j == encodedbm.Height - 1)
                    {
                        encodedbm.SetPixel(i, j, Color.FromArgb(pixel.R, pixel.G, text.Length));
                    }
                }

            }
            return "Message encoded;";
        }
        private static string Decode(string imagepath) 
        {
            string text = "";
            Bitmap encodedimage = new Bitmap(imagepath);
            Color lastpixel = encodedimage.GetPixel(encodedimage.Width - 1, encodedimage.Height - 1);
            int msgLength = lastpixel.B;
            for (int i = 0; i < encodedimage.Width; i++)
            {
                for (int j = 0; j < encodedimage.Height; j++)
                {
                    if (i<1 &&j<msgLength)
                    {
                        Color pixel = encodedimage.GetPixel(i, j);                       
                        int value = pixel.B;
                        char c = Convert.ToChar(value);
                        string letter = Encoding.ASCII.GetString(new byte[] { Convert.ToByte(c) });
                        text += letter;
                        
                    }
                    
                }

            }
            return text;
        }
    }
}
