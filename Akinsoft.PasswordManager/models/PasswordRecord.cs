using System;

namespace Akinsoft.PasswordManager.Models
{
    public class PasswordRecord
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
        public int CategoryID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedUser { get; set; }

    }
}