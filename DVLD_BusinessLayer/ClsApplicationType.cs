using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using DVLD_DataAccessLayer;

namespace DVLD_BusinessLayer
{
    public class ClsApplicationType
    {
        public enum enMode {UpdatMode = 0 ,AddMode = 1}
        public enMode Mode;
        public int ID { get; set; }
        public string Title { get; set; }
        public float Fees { get; set; }

        private ClsApplicationType(int ID,string Title,float Fees)
        {
            this.ID = ID;
            this.Title =Title;
            this.Fees = Fees;
            Mode = enMode.UpdatMode;
        }
        public ClsApplicationType()
        {
            this.ID = 0;
            this.Title = "";
            this.Fees = 0;
            Mode = enMode.AddMode;
        }
        private bool _AddNewApplicationType()
        {
            this.ID = ClsApplicationTypeData.InsertApplicationType(this.Title,this.Fees);

            return (this.ID != 0);
        }
        private bool _UpdateApplicationType()
        {
            return ClsApplicationTypeData.UpdataApplicationType(ID,Title,Fees);
        }
        public static ClsApplicationType Find (int ID)
        {
            string Title = "";
            float Fees = 0;
            if(ClsApplicationTypeData.GetAllicaionByID(ID,ref Title,ref Fees))
            {
                return new ClsApplicationType(ID, Title, Fees);
            }
            else
            {
                return null;
            }
        }
        public bool Save ()
        {
            switch(Mode)
            {
                case enMode.UpdatMode:
                    return _UpdateApplicationType();

                case enMode.AddMode:
                    if (_AddNewApplicationType())
                    {
                        Mode = enMode.UpdatMode;
                        return true;
                    }
                    else
                        return false;
                default:
                    return false;
            }
            
        }

        public static DataTable GetAllApplicationTypes()
        {
            return ClsApplicationTypeData.SelectAllApplicationTypes();
        }
        
    }
}
