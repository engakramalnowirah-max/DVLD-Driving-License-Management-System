using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using DVLD_DataAccessLayer;

namespace DVLD_BusinessLayer
{
    public class ClsTestType
    {
        public enum enMode { UpdatMode = 1, AddMode = 2 }
        public enum enTestType { VisionTest =1, WrittenTest = 2, StreetTest = 3}
        public enMode Mode;

        public ClsTestType.enTestType ID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public float Fees { get; set; }

        private ClsTestType(ClsTestType.enTestType ID, string Title,string Description, float Fees)
        {
            this.ID = ID;
            this.Title = Title;
            this.Description = Description;
            this.Fees = Fees;
            Mode = enMode.UpdatMode;
        }
        public ClsTestType()
        {
            this.ID = ClsTestType.enTestType.VisionTest;
            this.Title = "";
            this.Description = "";
            this.Fees = 0;
            Mode = enMode.AddMode;
        }
        private bool _UpdateTestType()
        {
            return ClsTestTypeData.UpdataTestType((int)ID,Title,Description, Fees);
        }
        private bool _AddNewTestType()
        {
            this.ID = (ClsTestType.enTestType)ClsTestTypeData.InsertTestType(this.Title,this.Description,this.Fees);
            return (this.ID != 0);
        }
        public static ClsTestType Find(ClsTestType.enTestType TestTypeID)
        {
            string Title = "", Description = "";
            float Fees = 0;
            if (ClsTestTypeData.GetTestTypeByID((int)TestTypeID, ref Title,ref Description, ref Fees))
            {
                return new ClsTestType(TestTypeID, Title, Description, Fees);
            }
            else
            {
                return null;
            }
        }
        public bool Save()
        {
            switch (Mode)
            {
                case enMode.UpdatMode:
                    return _UpdateTestType();
                case enMode.AddMode:
                    if (_AddNewTestType())
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
        public static DataTable GetAllTestTypes()
        {
            return ClsTestTypeData.GetAllTestTypes();
        }
    }
}
