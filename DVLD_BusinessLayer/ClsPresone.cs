using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DVLD_DataAccessLayer;
using System.Data;
using System.Runtime.CompilerServices;

namespace DVLD_BusinessLayer
{
    public class ClsPresone
    {
        public enum enMode { UpdateMode =1 , AddMode = 2}

        public int PersonID { get; set; }
        public string NationalNo { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string LastName { get; set; }
        public string ThirdName { get; set; }

        public string FullName
        {
            get
            {
                return FirstName + " " + SecondName + " " + ThirdName + " " + LastName;
            }
        }

        public DateTime DateOfBirth { get; set; }
        public bool Gendor { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public int NationalityCountryID { get; set; }
        public string ImagePath { get; set; }

        public ClsCountry CountryInfo;
        public enMode Mode;

      
        private bool _AddNewPerson()
        {
            this.PersonID = ClsPersonData.InsertNewPersoneInDB(this.NationalNo, this.FirstName, this.SecondName, this.ThirdName, this.LastName, this.DateOfBirth, this.Gendor, this.Address, this.Phone, this.Email, this.NationalityCountryID, this.ImagePath);
            return (this.PersonID != 0);
        }
        private bool _UpdatePerson()
        {
            return (ClsPersonData.UpdutePersonInDB(this.PersonID, this.NationalNo, this.FirstName, this.SecondName, this.ThirdName, this.LastName, this.DateOfBirth, this.Gendor, this.Address, this.Phone, this.Email, this.NationalityCountryID, this.ImagePath));
        }
        public ClsPresone()
        {
            this.PersonID = -1;
            this.NationalNo = "";
            this.FirstName = "";
            this.SecondName = "";
            this.ThirdName = "";
            this.LastName = "";
            this.DateOfBirth = DateTime.Now.AddYears(-18);
            this.Gendor = false;
            this.Email = "";
            this.Address = "";
            this.Phone = "";
            this.NationalityCountryID = -1;
            this.ImagePath = "";


            Mode = enMode.AddMode;
        }
        private ClsPresone(int PersonID,string NationalNo, string FirstName, string SecondName, string ThirdName, string LastName
                               , DateTime DateOfBirth, bool Gendor, string Email, string Address, string Phone, int NationalityCountryID, string ImagePath)
        {
            this.PersonID = PersonID;
            this.NationalNo = NationalNo;
            this.FirstName = FirstName;
            this.SecondName = SecondName;
            this.ThirdName = ThirdName;
            this.LastName = LastName;
            this.DateOfBirth = DateOfBirth;
            this.Gendor = Gendor;
            this.Email = Email;
            this.Address = Address;
            this.Phone = Phone;
            this.NationalityCountryID = NationalityCountryID;
            this.CountryInfo = ClsCountry.GetCountryByID(NationalityCountryID);
            this.ImagePath = ImagePath;
            Mode = enMode.UpdateMode;
        }


        public bool Save()
        {
            switch (Mode)
            {
                case enMode.UpdateMode:
                    return _UpdatePerson();
                case enMode.AddMode:
                    if (_AddNewPerson())
                        return true;
                    else return false;
            }
            return false;

        }

        public static ClsPresone Find(int PersonID)
        {
            string National = "", FirstName = "", SecondName = "", ThirdName = "", LastName = "", Address = "", Phone = "", Email = "" , ImagePath="";
            DateTime DateOfBirth = DateTime.Now;
            bool Gendor = false;
                int NationalityCountryID = -1;
            if(ClsPersonData.GetPersonByPersonID(PersonID,ref National,ref FirstName,ref SecondName,ref ThirdName,ref LastName,ref DateOfBirth,ref Gendor,ref Address,ref Phone,ref Email,ref NationalityCountryID,ref ImagePath))
            {
                return new ClsPresone(PersonID,National, FirstName, SecondName, ThirdName, LastName,  DateOfBirth,  Gendor,Email,Address,Phone, NationalityCountryID,  ImagePath);
            }
            else
                return null;

        }
        public static ClsPresone Find(String National)
        {
            string  FirstName = "", SecondName = "", ThirdName = "", LastName = "", Address = "", Phone = "", Email = "", ImagePath = "";
            DateTime DateOfBirth = DateTime.Now;
            bool Gendor = false;
                int NationalityCountryID = -1, PersonID = -1;
            if (ClsPersonData.GetPersonByNationalID(National,ref PersonID, ref FirstName, ref SecondName, ref ThirdName, ref LastName, ref DateOfBirth, ref Gendor, ref Address, ref Phone, ref Email, ref NationalityCountryID, ref ImagePath))
            {
                return new ClsPresone(PersonID,National, FirstName, SecondName, ThirdName, LastName, DateOfBirth, Gendor, Email, Address, Phone, NationalityCountryID, ImagePath);
            }
            else
                return null;

        }

        static public DataTable GetAllPersons()
        {
            return ClsPersonData.SelectAllPeopleFoDB();
        }
        public static bool isPersonExist(int  PersonID)
        {
            return  ClsPersonData.IsPersonExist(PersonID);
        }
        public static bool isPersonExist(string NationalNo)
        {
            return ClsPersonData.IsPersonExist(NationalNo);
        }
        public static bool Delete(int PersonID)
        {
            return ClsPersonData.DeleteOnePersonOfDB(PersonID);
        }
    }

    
}
