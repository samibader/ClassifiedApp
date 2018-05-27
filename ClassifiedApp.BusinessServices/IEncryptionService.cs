﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassifiedApp.BusinessServices
{
    public interface IEncryptionService
    {
         string CreateSalt();
         string EncryptPassword(string password, string salt);
    }
}
