using System;
using System.Collections.Generic;

namespace QuiZone.DataAccess.Models.Entities
{
    public partial class QuestionCorrectAnswer
    {
        public int Id { get; set; }
        public int QuestionId { get; set; }
        public int AnswerId { get; set; }
        public DateTime CreateDate { get; set; }
        public int CreateUserId { get; set; }
        public DateTime ModDate { get; set; }
        public int? ModUserId { get; set; }

        public virtual Answer Answer { get; set; }
        public virtual Question Question { get; set; }
    }
}
