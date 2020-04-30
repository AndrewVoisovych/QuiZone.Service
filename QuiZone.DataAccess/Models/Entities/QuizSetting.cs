using System;
using System.Collections.Generic;

namespace QuiZone.DataAccess.Models.Entities
{
    public partial class QuizSetting
    {
        public QuizSetting()
        {
            Quiz = new HashSet<Quiz>();
        }

        public int Id { get; set; }
        public bool? RandomPosition { get; set; }
        public string Price { get; set; }
        public int? TimerValue { get; set; }
        public DateTime? DateEnd { get; set; }
        public DateTime CreateDate { get; set; }
        public int CreateUserId { get; set; }
        public DateTime ModDate { get; set; }
        public int? ModUserId { get; set; }

        public virtual ICollection<Quiz> Quiz { get; set; }
    }
}
