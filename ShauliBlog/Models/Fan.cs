using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShauliBlog.Models {

    public enum Gender {
        Male,
        Female,
        Other
    }

    /// <summary>
    /// Represent Fan of the blog. 
    /// Contains all the data about the fan.
    /// </summary>
    public class Fan {

        public Fan() {

        }

        public Fan(int ID, string FirstName, string LastName, DateTime Birthday, Gender Gender, short Pazam, string username, string password, bool isAdmin) {
            this.ID = ID;
            this.FirstName = FirstName;
            this.LastName = LastName;
            this.Gender = Gender;
            this.Birthday = Birthday;
            this.Pazam = Pazam;
            this.Username = username;
            this.Password = password;
            this.IsAdmin = isAdmin;
        }

        public int ID { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public Gender Gender { get; set; }

        public DateTime Birthday { get; set; }

        public short Pazam { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public bool IsAdmin{ get; set; }
    }
}
