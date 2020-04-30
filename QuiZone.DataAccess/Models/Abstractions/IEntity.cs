using System;

namespace QuiZone.DataAccess.Models.Abstractions
{
    public interface IEntity
    {
        int Id { get; set; }
        public DateTime ModDate { get; set; }
        public int? ModUserId { get; set; }

    }
}
