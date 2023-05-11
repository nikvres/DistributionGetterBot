using System.ComponentModel.DataAnnotations;

namespace DistributionGetterBot.Models
{
    public class UserModel
    {
        [Key]
        public string IdUser { get; set; }
        public string Username { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }

        public UserModel(string idUser, string username, string firstname, string lastname) 
        {
            this.IdUser = idUser;
            this.Username = username;
            this.Firstname = firstname;
            this.Lastname = lastname;
        }

    }
}
