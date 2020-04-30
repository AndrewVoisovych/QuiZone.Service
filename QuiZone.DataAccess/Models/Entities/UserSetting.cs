using System;
using System.Collections.Generic;

namespace QuiZone.DataAccess.Models.Entities
{
    public partial class UserSetting
    {
        public UserSetting()
        {
            User = new HashSet<User>();
        }

        public int Id { get; set; }
        public string Setting { get; set; }
        public DateTime CreateDate { get; set; }
        public int CreateUserId { get; set; }
        public DateTime ModDate { get; set; }
        public int? ModUserId { get; set; }

        public virtual ICollection<User> User { get; set; }
    }
}
