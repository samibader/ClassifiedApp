﻿using ClassifiedApp.DataAccess.Repositories;
using ClassifiedApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ClassifiedApp.DataAccess.Extensions
{
    public static class UserExtensions
    {
        public static User GetSingleByUsername(this EntityBaseRepository<User> userRepository, string username)
        {
            return userRepository.GetAll().FirstOrDefault(x => x.Username == username);
        }

        //public static string CreateSalt(this EntityBaseRepository<User> userRepository)
        //{
        //    var data = new byte[0x10];
        //    using (var cryptoServiceProvider = new RNGCryptoServiceProvider())
        //    {
        //        cryptoServiceProvider.GetBytes(data);
        //        return Convert.ToBase64String(data);
        //    }
        //}

        //public static string EncryptPassword(this EntityBaseRepository<User> userRepository, string password, string salt)
        //{
        //    using (var sha256 = SHA256.Create())
        //    {
        //        var saltedPassword = string.Format("{0}{1}", salt, password);
        //        byte[] saltedPasswordAsBytes = Encoding.UTF8.GetBytes(saltedPassword);
        //        return Convert.ToBase64String(sha256.ComputeHash(saltedPasswordAsBytes));
        //    }
        //}
    }
}
