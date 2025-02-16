using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using HashSaltLogin.Context;
using HashSaltLogin.Models;

namespace HashSaltLogin.Services
{
    public class UserServices
    {
        private readonly DataContext _dataBase;

        public UserServices(DataContext dataBase)
        {
            _dataBase = dataBase;
        }

        public bool CreateUser(UserDTO user)
        {
            bool result = false;

            //If the user or eamil already exists in our database
            if(!DoesUserExist(user.Email))
            {
                UserModel userToAdd = new();
                //We need a method to parse the password into salt and hash
                PasswordDTO hashPassword = HashPassword(user.Password);
                userToAdd.Email = user.Email;
                userToAdd.Hash = hashPassword.Hash;
                userToAdd.Salt = hashPassword.Salt;

                _dataBase.Users.Add(userToAdd);
                result = _dataBase.SaveChanges() == 1;
            }

            return result;
        }

        private bool DoesUserExist(string email)
        {
            return _dataBase.Users.SingleOrDefault(users => users.Email == email) != null;
        }

        private static PasswordDTO HashPassword(string password)
        {
            //bytes have a value between 0 and 255
            byte[] saltBytes = RandomNumberGenerator.GetBytes(64);

            //We converting the array to a string for easy storage.
            string salt = Convert.ToBase64String(saltBytes);

            //We're going to declare the hash

            string hash;

            //Using statment is used to quickly dispose of data for encryptions
            // SHA = Secure Hash Algorithm
            //

            using(var deriveBytes = new Rfc2898DeriveBytes(password, saltBytes, 310000, HashAlgorithmName.SHA256))
            {
                hash = Convert.ToBase64String(deriveBytes.GetBytes(32));
            }
            //Created a new PasswordDTO object to house our salt and hash
            PasswordDTO hashedPassword = new();

            hashedPassword.Salt = salt;
            hashedPassword.Hash = hash;

            return hashedPassword;
        }

        public string Login(UserDTO user)
        {
            //Get our User info if they exist
            var foundUser = GetUserByEmail(user.Email);

            if(foundUser == null)
            {
                return null;
            }

            if(!VerifyPassword(user.Password, foundUser.Salt, foundUser.Hash))
            {
                return null;
            }

            return "Token";

            // Now we know that the user exists in our DataBase we have to Verify the password
        }

        private UserModel GetUserByEmail(string email)
        {
            return _dataBase.Users.SingleOrDefault(user => user.Email == email);
        }

        private static bool VerifyPassword(string password, string salt, string hash)
        {
            //we have to convert our user salt from a string to a byte array for our hash Algorithm
            byte[] saltBytes = Convert.FromBase64String(salt);

            string newHash;
            using(var deriveBytes = new Rfc2898DeriveBytes(password, saltBytes, 310000, HashAlgorithmName.SHA256))
            {
                newHash = Convert.ToBase64String(deriveBytes.GetBytes(32));
            }

            return hash == newHash;

        }
    }
}