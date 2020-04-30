using System;
using System.Collections.Generic;

namespace QuiZone.DataAccess.Models.Entities
{
    public partial class QuestionCategory
    {
        public QuestionCategory()
        {
            Question = new HashSet<Question>();
        }

        public int Id { get; set; }
        public string Category { get; set; }
        public DateTime CreateDate { get; set; }
        public int CreateUserId { get; set; }
        public DateTime ModDate { get; set; }
        public int? ModUserId { get; set; }

        public virtual ICollection<Question> Question { get; set; }
    }
}
