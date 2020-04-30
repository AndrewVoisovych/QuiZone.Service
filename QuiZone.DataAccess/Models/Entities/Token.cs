using QuiZone.DataAccess.Models.Abstractions;
using System;
using System.Collections.Generic;

namespace QuiZone.DataAccess.Models.Entities
{
    public partial class Token: IEntity
    {
        public int Id { get; set; }
        public string RefreshToken { get; set; }
        public DateTime CreateDate { get; set; }
        public int? CreateUserId { get; set; }
        public DateTime ModDate { get; set; }
        public int? ModUserId { get; set; }

        public virtual User CreateUser { get; set; }
        public virtual User ModUser { get; set; }
    }
}
