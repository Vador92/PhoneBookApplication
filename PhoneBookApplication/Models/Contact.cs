using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using SQLite;

namespace PhoneBookApplication.Models
{
    //Constructing Database model for contact
    [Table("Contact")]
    public class Contact
    {
        //AutoIncrement is to avoid unnecessary data usage if not needed by system
        //PrimaryKey allows for uniquely defined records, in this case contacts
        [PrimaryKey, AutoIncrement]
        //generated id for unique contact
        public Guid Id { get; set; }
        public string ProfilePicture { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
    }
}
