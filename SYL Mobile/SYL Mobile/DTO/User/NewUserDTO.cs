using System;
using System.Collections.Generic;
using System.Text;

namespace SYL_Mobile.DTO.User
{
    public class NewUserDTO
    {
        public string userName { get; set; }
        public string userLastName { get; set; }
        public string userEmail { get; set; }
        public string userPassword { get; set; }
        public int userType { get; set; }
    }
}
