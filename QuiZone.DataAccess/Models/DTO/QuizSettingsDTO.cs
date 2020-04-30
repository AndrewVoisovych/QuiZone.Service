using System;

namespace QuiZone.DataAccess.Models.DTO
{
    public sealed class QuizSettingsDTO
    {
        public int Id { get; set; }
        public bool? RandomPosition { get; set; }
        public bool? Price { get; set; }
        public int? TimerValue { get; set; }
        public DateTime? DateEnd { get; set; }
        public int CreateUserId { get; set; }
        public int? ModUserId { get; set; }

    }
}
