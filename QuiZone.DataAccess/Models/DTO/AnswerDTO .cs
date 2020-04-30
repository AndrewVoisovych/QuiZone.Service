using System;

namespace QuiZone.DataAccess.Models.DTO
{
    public sealed class AnswerDTO
    {
        public int Id { get; set; }
        public string Body { get; set; }
        public string ImagePath { get; set; }
        public string Code { get; set; }
        public DateTime? CreateDate { get; set; }
    }
}
