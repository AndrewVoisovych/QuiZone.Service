using System;

namespace QuiZone.DataAccess.Models.DTO
{
    public sealed class QuizDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public string Category { get; set; }
        public int SettingId { get; set; }
        public int TopicId { get; set; }
        public string Topic { get; set; }
        public string ImagePath { get; set; }
        public QuizSettingsDTO Setting { get; set; }
        public string AccessHash { get; set; }
        public int AccessId { get; set; }
        public string Access { get; set; }
        public DateTime? CreateDate { get; set; }
        public int? CreateUserId { get; set; }
    

    }
}
