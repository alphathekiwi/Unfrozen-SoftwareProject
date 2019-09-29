using PropertyChanged;
using SQLite;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MobileApp.Models
{
    [AddINotifyPropertyChangedInterface]
    public class User
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool PublicName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool PublicEmail { get; set; }
        public string Email { get; set; }
        public bool PublicPhone { get; set; }
        public string PhoneNumber { get; set; }
        public string Image { get; set; }
        public byte[] Photo { get; set; }
        public User() { }
        public User(string UserName, string Password, string Email, string Phone)
        {
            this.UserName = UserName;
            this.Password = Password;
            this.Email = Email;
            this.PhoneNumber = Phone;
            Image = "";
        }
        public override string ToString()
        {
            return PublicName ? $"{FirstName} {LastName}" : UserName;
        }
    }
}
