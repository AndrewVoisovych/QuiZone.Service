using System;
using System.Collections.Generic;

namespace QuiZone.DataAccess.Models.Entities
{
    public partial class TypeList
    {
        public TypeList()
        {
            ListQuiz = new HashSet<ListQuiz>();
        }

        public int Id { get; set; }
        public string Type { get; set; }
        public DateTime CreateDate { get; set; }
        public int CreateUserId { get; set; }
        public DateTime ModDate { get; set; }
        public int? ModUserId { get; set; }

        public virtual ICollection<ListQuiz> ListQuiz { get; set; }
    }
}
