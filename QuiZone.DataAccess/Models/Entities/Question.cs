using QuiZone.DataAccess.Models.Abstractions;
using System;
using System.Collections.Generic;

namespace QuiZone.DataAccess.Models.Entities
{
    public partial class Question: IEntity
    {
        public Question()
        {
            History = new HashSet<History>();
            QuestionCorrectAnswer = new HashSet<QuestionCorrectAnswer>();
            QuestionOptionsAnswer = new HashSet<QuestionOptionsAnswer>();
        }

        public int Id { get; set; }
        public int QuizId { get; set; }
        public int CategoryId { get; set; }
        public int? Position { get; set; }
        public string Body { get; set; }
        public string ImagePath { get; set; }
        public string Code { get; set; }
        public int? Price { get; set; }
        public bool? RandomOption { get; set; }
        public DateTime CreateDate { get; set; }
        public int CreateUserId { get; set; }
        public DateTime ModDate { get; set; }
        public int? ModUserId { get; set; }

        public virtual QuestionCategory Category { get; set; }
        public virtual Quiz Quiz { get; set; }
        public virtual ICollection<History> History { get; set; }
        public virtual ICollection<QuestionCorrectAnswer> QuestionCorrectAnswer { get; set; }
        public virtual ICollection<QuestionOptionsAnswer> QuestionOptionsAnswer { get; set; }
    }
}
