using System;

namespace QuiZone.DataAccess.Models.DTO
{
    public sealed class OptionsAnswerDTO
    {
        public int Id { get; set; }
        public int QuestionId { get; set; }
        public AnswerDTO Answer { get; set; } 
    }
}
