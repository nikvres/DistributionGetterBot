using System.ComponentModel.DataAnnotations;

namespace DistributionGetterBot.Models
{
    public class UserModel
    {
        [Key]
        public long IdUser { get; set; }
        public string Username { get; set; }
        public string? Firstname { get; set; }
        public string? Lastname { get; set; }

        public UserModel(long idUser, string username, string firstname, string lastname) 
        {
            this.IdUser = idUser;
            this.Username = username;
            this.Firstname = firstname;
            this.Lastname = lastname;
        }

    }
}
