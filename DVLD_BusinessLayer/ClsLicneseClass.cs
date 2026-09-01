using DVLD_DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_BusinessLayer
{
    public class ClsLicenseClass
    {
        public enum enMode { AddNew =0,Update =1 }
        public enMode Mode;

        public int LicenseClassID { get; set; }
        public string ClassName { get; set; }
        public string ClassDescription { get; set; }
        public short MinimumAllowedAge { get; set; }
        public short DefaultValidityLength { get; set; }
        public float ClassFees { get; set; }

        public ClsLicenseClass()
        {
            this.LicenseClassID = 0;
            this.ClassName = "";
            this.ClassDescription = "";
            this.MinimumAllowedAge = 0;
            this.DefaultValidityLength = 0;
            this.ClassFees = 0;
            this.Mode = enMode.AddNew;
        }
        private ClsLicenseClass(int LicenseClassID, string LicenseClassName,string Description,short MinimumAllwoedAge,short DefaultValidityLength,float ClassFees)
        {
            this.LicenseClassID = LicenseClassID;
            this.ClassName = LicenseClassName;
            this.ClassDescription= Description;
            this.MinimumAllowedAge = MinimumAllwoedAge;
            this.DefaultValidityLength = DefaultValidityLength;
            this.ClassFees = ClassFees;
            this.Mode = enMode.Update;

        }
        
        public static DataTable GetAllLicensesClasses()
        {
            return ClsLicenseClassData.SelectLicenseClassesOfDB();
        }
        public static ClsLicenseClass Find(string LicenseClassName)
        {
            int licenseClassID = 0;
            string LicenseClassDescription = "";
            short MinimumAllowedAge = 0, DefaultValidityLength = 0;
            float ClassFees = 0;
            if (ClsLicenseClassData.GetLicenseClasseByName(LicenseClassName,ref licenseClassID,ref LicenseClassDescription,ref MinimumAllowedAge,ref DefaultValidityLength,ref ClassFees ))
            {
                return new ClsLicenseClass(licenseClassID, LicenseClassName, LicenseClassDescription,MinimumAllowedAge,DefaultValidityLength,ClassFees);
            }
            else
            {
                return null;
            }
        }
        public static ClsLicenseClass Find(int LicenseClassID)
        {
            
            string LicenseClassDescription = "" , LicenseClassName = "";
            short MinimumAllowedAge = 0, DefaultValidityLength = 0;
            float ClassFees = 0;
            if (ClsLicenseClassData.GetLicenseClassByID(LicenseClassID, ref LicenseClassName, ref LicenseClassDescription, ref MinimumAllowedAge, ref DefaultValidityLength, ref ClassFees))
            {
                return new ClsLicenseClass(LicenseClassID, LicenseClassName, LicenseClassDescription, MinimumAllowedAge, DefaultValidityLength, ClassFees);
            }
            else
            {
                return null;
            }
        }


    }
}
