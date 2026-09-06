using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using DVLD_DataAccessLayer;
using System.Diagnostics;
using System.Security.Cryptography;

namespace DVLD_BusinessLayer
{
    public class ClsUser
    {

        public enum enMode  {AddNew = 1,Update =2}
        public enMode Mode;

        public int UserID { get; set; }
        public int PersonID { get; set; }
        public string  UserName { get; set; }
        private string PasswordHashed = "";
        public string Password
        {
            get { return PasswordHashed; }
            set {  PasswordHashed = HashingPassword(value); }
           
        }
        public ClsPresone PersonInfo;
        public short IsActive { get; set; }

        public ClsUser()
        {
            this.UserID = 0;
            this.PersonID = 0;
            this.UserName = "";
            this.Password = "";
            this.PersonInfo = null;
            this.IsActive = 0;
            Mode = enMode.AddNew;
        }
        private ClsUser(int UserID,int PersonID,string UserName,string Password,short IsActive)
        {
            this.UserID = UserID;
            this.PersonID = PersonID;
            this.UserName= UserName;
            this.PasswordHashed = Password;
            this.PersonInfo = ClsPresone.Find(PersonID);
            this.IsActive=IsActive;
            Mode = enMode.Update;

        }

        private bool _AddNewUser()
        {
            this.UserID = ClsUserData.InsertNewUser(this.PersonID,this.UserName,this.PasswordHashed, this.IsActive);

            return (this.UserID != 0);
        }

        private bool _UpdateUser()
        {
            return ClsUserData.UpdateUserToDB(this.UserID,this.PersonID,this.UserName,this.PasswordHashed, this.IsActive);
        }

        public bool Save()
        {
            switch(Mode)
            {
                case enMode.AddNew:
                    if(_AddNewUser())
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case enMode.Update:
                    return _UpdateUser();
            }
            return true;
        }








        public static ClsUser Find(int UserID)
        {
            int PersonID = 0;
            string UserName = "", Password = "";
            short IsActive = 0;

            if(ClsUserData.GetUserByUserID(UserID,ref PersonID,ref UserName,ref Password,ref IsActive))
            {
                return new ClsUser(UserID,PersonID,UserName,Password,IsActive);
            }
            else
            {
                return null;
            }
        }

        public static ClsUser Find(string UserName,string Password)
        {
            string hashedPasswo = HashingPassword(Password);
            int PersonID = 0, UserID = 0;
            
            short IsActive = 0;

            if (ClsUserData.GetUserByUserNameAndPassword(UserName, hashedPasswo, ref UserID, ref PersonID, ref IsActive))
            {
                return new ClsUser(UserID, PersonID, UserName, hashedPasswo, IsActive);
            }
            else
            {
                return null;
            }
        }


        public static bool Delete(int UserID)
        {
            return ClsUserData.DeleteUserToDB(UserID);
        }

        public static DataTable GetAllUsrs()
        {
            return ClsUserData.SelectAllUsers();
        }

        public static bool isUserExistForPersonID(int PersonID)
        {
            return ClsUserData.IsUserExistByPersonID(PersonID);
        }

        public static string HashingPassword(string Password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                // Compute the hash value from the UTF-8 encoded input string
                byte[] hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(Password));

                // Convert the byte array to a lowercase hexadecimal string
                return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
            }
        }
    }
}
