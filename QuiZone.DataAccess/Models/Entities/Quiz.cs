using QuiZone.DataAccess.Models.Abstractions;
using System;
using System.Collections.Generic;

namespace QuiZone.DataAccess.Models.Entities
{
    public partial class Quiz: IEntity
    {
        public Quiz()
        {
            History = new HashSet<History>();
            ListQuiz = new HashSet<ListQuiz>();
            Question = new HashSet<Question>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public int SettingId { get; set; }
        public int TopicId { get; set; }
        public DateTime CreateDate { get; set; }
        public int CreateUserId { get; set; }
        public DateTime ModDate { get; set; }
        public int? ModUserId { get; set; }
        public string ImagePath { get; set; }
        public byte[] Access { get; set; }

        public virtual QuizCategory Category { get; set; }
        public virtual User CreateUser { get; set; }
        public virtual QuizSetting Setting { get; set; }
        public virtual QuizTopic Topic { get; set; }
        public virtual ICollection<History> History { get; set; }
        public virtual ICollection<ListQuiz> ListQuiz { get; set; }
        public virtual ICollection<Question> Question { get; set; }
    }
}
