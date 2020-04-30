using System;

namespace QuiZone.DataAccess.Models.DTO
{
    public sealed class QuestionDTO
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public int QuizId { get; set; }
        public int? Position { get; set; }
        public string Body { get; set; }
        public string ImagePath { get; set; }
        public string Code { get; set; }
        public int? Price { get; set; }
        public bool? RandomOption { get; set; }
        public DateTime? CreateDate { get; set; }

    }
}
