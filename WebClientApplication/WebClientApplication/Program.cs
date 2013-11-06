using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections;
using System.Drawing;
using System.Net;

namespace WebClientApplication
{
    class Program
    {
        static void Main(string[] args)
        {

            String finishedFile = "jxfive_" + DateTime.Now.ToShortDateString() + "_" + DateTime.Now.ToShortTimeString().Replace(" ", string.Empty) + ".jpg";
            Console.WriteLine(finishedFile);
            Console.WriteLine(finishedFile.Substring(0, finishedFile.IndexOf("_")));
            Console.ReadLine();
            
            String fileName = "1_jxfive_pic#1";
            Console.WriteLine(fileName.Substring(0, fileName.IndexOf("_")));
            Console.ReadLine();
            Console.WriteLine(fileName.Substring(fileName.IndexOf("_") + 1, fileName.LastIndexOf("_") - 2));
            Console.ReadLine();

            DirectoryInfo dir = new DirectoryInfo(@"F:\");
            FileInfo[] sdFiles = dir.GetFiles("*.txt", SearchOption.AllDirectories);
            List<Bitmap> myAL = new List<Bitmap>();



            foreach (FileInfo f in sdFiles)
            {
                Console.WriteLine(f.Name);
                Console.WriteLine(f.FullName);
                if (f.Name.ToString().Substring(0, f.Name.ToString().IndexOf("_")).Equals("1"))
                {
                    Console.WriteLine("SUBSTRING 1");
                    myAL.Add(new Bitmap( new FileStream(f.FullName, FileMode.Open)));
                 //   myAL.Add(new  FileStream(f.FullName, FileMode.Open));
                }
            }
            Bitmap[] bitmapArray = myAL.ToArray();
            Console.WriteLine(bitmapArray.Length);
            CombineImage combinedImage = new CombineImage();
        //    combinedImage.combineByRectangle(myAL.ToArray());
            combinedImage.combineByRectangle(bitmapArray, finishedFile);
            Uri uploadPage = new Uri("http://localhost:49765/About.aspx");
            System.Net.WebClient myWebClient = new WebClient();
            byte[] responseArray = myWebClient.UploadFile(uploadPage, @"F:\jrxfive_something.jpg");

            Console.WriteLine("\nResponse Received.The contents of the file uploaded are:\n{0}", 
            System.Text.Encoding.ASCII.GetString(responseArray));

            Console.ReadLine();

            

        }
    }

    public class CombineImage
    {
        public CombineImage()
        {

        }

        public void combineByRectangle(Bitmap[] imageArray, string fileName)
        {
            int sizeOfArray = imageArray.Length;
            Console.WriteLine(sizeOfArray);
            int widthOfImage = imageArray[0].Width;
            int heightofImage = imageArray[0].Height;
            int currentHeightReference = 0;

            Bitmap combineImage = new Bitmap(widthOfImage, (heightofImage * sizeOfArray));
            Graphics g = Graphics.FromImage(combineImage);
            g.Clear(System.Drawing.Color.White);

            foreach (Bitmap image in imageArray)
            {
                g.DrawImage(image,
                  new System.Drawing.Rectangle(0, currentHeightReference, widthOfImage, heightofImage));
                currentHeightReference += heightofImage;
            }
            combineImage.Save(@"F:\jrxfive_something.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
        }


    }
}
