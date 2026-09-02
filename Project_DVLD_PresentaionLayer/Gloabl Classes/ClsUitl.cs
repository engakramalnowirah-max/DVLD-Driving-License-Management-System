using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace Project_DVLD_PresentaionLayer.Gloabl_Classes
{
    internal class ClsUitl
    {
        public static string _CreateGUID()
        {
            Guid guid = Guid.NewGuid();
            return guid.ToString();
        }
        public static string _ReplacFillNameWithGUID(string FillName)
        {
            string fillName = FillName;
            FileInfo info = new FileInfo(fillName);
            string exten = info.Extension;
            return _CreateGUID() + exten;
        }
        public static bool _CreateFillProjectIsNotExist(string DestinationFolder)
        {
            if (!Directory.Exists(DestinationFolder))
            {
                try
                {
                    Directory.CreateDirectory(DestinationFolder);
                    return true; 
                }
                catch (IOException)
                {
                    MessageBox.Show("Erorr: With Create Folder Dstination","Erorr",MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                    
                }
            }
            return true;
        }
        public static bool CopyImageToProjectFolder(ref string SuoresPath)
        {

            string DestinationFolder = @"C:\DVLD-People-Images\";
            if(!_CreateFillProjectIsNotExist(DestinationFolder))
            {
                return false;
            }

            string DestinationFill = DestinationFolder + _ReplacFillNameWithGUID(SuoresPath);

            try
            {
                File.Copy(SuoresPath, DestinationFill, true);
               
            }
            catch (IOException iox)
            {
                MessageBox.Show(iox.Message,"Erorr",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return false;
            }

            SuoresPath = DestinationFill;
            return true;
        }

    }
}
