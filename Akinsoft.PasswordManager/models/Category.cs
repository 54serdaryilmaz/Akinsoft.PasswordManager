using System;

namespace Akinsoft.PasswordManager.Models
{
    public class Category
    {
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
        public int IsActive { get; set; }
        public DateTime CreatedDate { get; set; }

    }
}