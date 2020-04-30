using System;
using System.Collections.Generic;

namespace QuiZone.DataAccess.Models.Entities
{
    public partial class ListQuiz
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int QuizId { get; set; }
        public int TypeAddedId { get; set; }
        public bool? Status { get; set; }
        public int? Price { get; set; }
        public DateTime CreateDate { get; set; }
        public int CreateUserId { get; set; }
        public DateTime ModDate { get; set; }
        public int? ModUserId { get; set; }

        public virtual Quiz Quiz { get; set; }
        public virtual TypeList TypeAdded { get; set; }
        public virtual User User { get; set; }
    }
}
