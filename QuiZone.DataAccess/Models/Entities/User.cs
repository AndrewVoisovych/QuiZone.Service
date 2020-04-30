using QuiZone.DataAccess.Models.Abstractions;
using System;
using System.Collections.Generic;

namespace QuiZone.DataAccess.Models.Entities
{
    public partial class User: IEntity
    {
        public User()
        {
            History = new HashSet<History>();
            ListQuiz = new HashSet<ListQuiz>();
            Quiz = new HashSet<Quiz>();
            TokenCreateUser = new HashSet<Token>();
            TokenModUser = new HashSet<Token>();
        }

        public int Id { get; set; }
        public int RoleId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public byte[] Password { get; set; }
        public bool Verification { get; set; }
        public int? SettingId { get; set; }
        public int? HistoryId { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime ModDate { get; set; }
        public int? ModUserId { get; set; }

        public virtual UserRole Role { get; set; }
        public virtual UserSetting Setting { get; set; }
        public virtual ICollection<History> History { get; set; }
        public virtual ICollection<ListQuiz> ListQuiz { get; set; }
        public virtual ICollection<Quiz> Quiz { get; set; }
        public virtual ICollection<Token> TokenCreateUser { get; set; }
        public virtual ICollection<Token> TokenModUser { get; set; }
    }
}
