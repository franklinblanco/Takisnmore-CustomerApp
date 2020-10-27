using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Xamarin.Forms;
using System.Threading.Tasks;

namespace Takisnmore.Scripts
{
    class CacheManager
    {
        #region Singleton pattern
        private static CacheManager instance = new CacheManager();
        private CacheManager() { }
        public static CacheManager Instance
        {
            get { return instance; }
        }
        #endregion

        private string tmp = Environment.GetFolderPath(System.Environment.SpecialFolder.Personal) + "/";

        public ImageSource GetImageSource(string imageid)
        {
            if (ImageExists(imageid))
            {
                return ImageSource.FromFile(tmp + imageid + GetExtension(imageid));
            }
            //Download image and return the imagesource

            if (DownloadImage(imageid)) {
                Console.WriteLine(tmp + imageid + GetExtension(imageid));
                
                return ImageSource.FromFile(tmp + imageid + GetExtension(imageid));
            }
            return null;
        }
        public void DeleteFiles()
        {
            File.Delete(tmp + "IJP000001.jpg");
            File.Delete(tmp + "IJP000002.jpg");
            File.Delete(tmp + "IJE000003.jpeg");
            File.Delete(tmp + "LJE000001.jpeg");

            File.Delete(tmp + "IJP000001");
            File.Delete(tmp + "IJP000002");
            File.Delete(tmp + "IJE000003");
            File.Delete(tmp + "LJE000001");

            File.Delete(tmp + "IJP000001jpg");
            File.Delete(tmp + "IJP000002jpg");
            File.Delete(tmp + "IJE000003jpeg");
            File.Delete(tmp + "LJE000001jpeg");
        }
        public string GetVideoSource(string videoid)
        {
            return null;
        }
        #region File finding
        private bool ImageExists(string imageid)
        {
            return File.Exists(tmp + imageid + GetExtension(imageid));
        }
        private bool VideoExists(string videoid)
        {
            return false;
        }
        #endregion


        #region File creation & download
        private bool DownloadImage(string imageid)
        {
            byte[] image = CustomerClient.Instance.GetMedia(imageid);
            Console.WriteLine("Recieved file with size: " + image.Length.ToString());
            try {
                File.WriteAllBytes(tmp + imageid + GetExtension(imageid), image);
            }
            catch(Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            
            return true;
        }
        private bool DownloadVideo(string videoid)
        {
            return false;
        }
        private bool SetImage(string imageid)
        {

            return false;
        }
        private bool SetVideo(string videoid)
        {
            return false;
        }
        #endregion


        #region Extension handling
        private string GetExtension(string id)
        {

            if (id == "IJP000001")
            {
                return ".jpg";
            }
            else if (id == "IJP000002")
            {
                return ".jpg";
            }
            else if (id == "IJE000003")
            {
                return ".jpeg";
            }
            else if (id == "LJP000001")
            {
                return ".jpg";
            }
            else if (id == "LPN000003")
            {
                return ".png";
            }
            else if (id == "LJE000001")
            {
                return ".jpeg";
            }
            else if (id == "IJP000003")
            {
                return ".jpg";
            }
            else
            {
                Console.WriteLine("No extension found for the id.");
                return "";
            }
        }
        #endregion
    }
}
