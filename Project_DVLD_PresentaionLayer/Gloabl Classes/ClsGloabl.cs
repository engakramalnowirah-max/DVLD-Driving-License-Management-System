using DVLD_BusinessLayer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project_DVLD_PresentaionLayer.Gloabl_Classes
{
    internal class ClsGloabl
    {
        public static ClsUser CurrentUser;

        public static bool RememberUserNameAndPassword(string UserName, string Password)
        {
            try
            {
                string CurrenDiretory = System.IO.Directory.GetCurrentDirectory();

                string filePath = CurrenDiretory + "\\data.txt";

                if(UserName == ""&& File.Exists(filePath))
                {
                    File.Delete(filePath);
                    return true;
                }

                string dataToSave = UserName + "/##/" + Password;

                using(StreamWriter Writer = new StreamWriter(filePath))
                {
                    Writer.WriteLine(dataToSave);
                    return true;
                }

               
            }
            catch (Exception ex)
            {

                MessageBox.Show($"An error occurent {ex.Message}");
                return false;
            }
        }
        public static bool GetStoredCreadential(ref string UserName, ref string Password)
        {
            try
            {
                string CurrenDiretory = System.IO.Directory.GetCurrentDirectory();

                string filePath = CurrenDiretory + "\\data.txt";

               
                if (File.Exists(filePath))
                {
                    using(StreamReader Reader = new StreamReader(filePath))
                    {
                        string Line;
                        while ((Line = Reader.ReadLine()) != null)
                        {
                            Console.WriteLine(Line);
                            string[] Reseult = Line.Split(new string[] { "/##/" }, StringSplitOptions.None);
                            UserName = Reseult[0];
                            Password = Reseult[1];
                        }
                        return true;
                    }
                }
                else
                {
                    return false;
                }
               
            }
            catch (Exception ex)
            {
                MessageBox.Show($"an Error occurend {ex.Message}");
                return false;
            }
        }
    }
}
