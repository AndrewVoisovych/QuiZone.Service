using System;
using System.Collections.Generic;

namespace QuiZone.DataAccess.Models.Entities
{
    public partial class QuizTopic
    {
        public QuizTopic()
        {
            Quiz = new HashSet<Quiz>();
        }

        public int Id { get; set; }
        public string Topic { get; set; }
        public DateTime CreateDate { get; set; }
        public int CreateUserId { get; set; }
        public DateTime ModDate { get; set; }
        public int? ModUserId { get; set; }

        public virtual ICollection<Quiz> Quiz { get; set; }
    }
}
