using System;
using Microsoft.EntityFrameworkCore;
using QuiZone.DataAccess.Models.Entities;

namespace QuiZone.DataAccess
{
    public partial class QuiZoneContext : DbContext
    {
        public QuiZoneContext()
        {
        }

        public QuiZoneContext(DbContextOptions<QuiZoneContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Answer> Answer { get; set; }
        public virtual DbSet<History> History { get; set; }
        public virtual DbSet<ListQuiz> ListQuiz { get; set; }
        public virtual DbSet<Question> Question { get; set; }
        public virtual DbSet<QuestionCategory> QuestionCategory { get; set; }
        public virtual DbSet<QuestionCorrectAnswer> QuestionCorrectAnswer { get; set; }
        public virtual DbSet<QuestionOptionsAnswer> QuestionOptionsAnswer { get; set; }
        public virtual DbSet<Quiz> Quiz { get; set; }
        public virtual DbSet<QuizAccess> QuizAccess { get; set; }
        public virtual DbSet<QuizCategory> QuizCategory { get; set; }
        public virtual DbSet<QuizSetting> QuizSetting { get; set; }
        public virtual DbSet<QuizTopic> QuizTopic { get; set; }
        public virtual DbSet<Token> Token { get; set; }
        public virtual DbSet<TypeList> TypeList { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<UserRole> UserRole { get; set; }
        public virtual DbSet<UserSetting> UserSetting { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Answer>(entity =>
            {
                entity.ToTable("ANSWER");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Body)
                    .IsRequired()
                    .HasColumnName("BODY")
                    .HasColumnType("text");

                entity.Property(e => e.Code)
                    .HasColumnName("CODE")
                    .HasColumnType("text");

                entity.Property(e => e.CreateDate)
                    .HasColumnName("CREATE_DATE")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CreateUserId).HasColumnName("CREATE_USER_ID");

                entity.Property(e => e.ImagePath)
                    .HasColumnName("IMAGE_PATH")
                    .HasColumnType("text");

                entity.Property(e => e.ModDate)
                    .HasColumnName("MOD_DATE")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ModUserId).HasColumnName("MOD_USER_ID");
            });

            modelBuilder.Entity<History>(entity =>
            {
                entity.ToTable("HISTORY");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.AnswerId).HasColumnName("ANSWER_ID");

                entity.Property(e => e.AnswerText)
                    .HasColumnName("ANSWER_TEXT")
                    .HasColumnType("text");

                entity.Property(e => e.CreateDate)
                    .HasColumnName("CREATE_DATE")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CreateUserId).HasColumnName("CREATE_USER_ID");

                entity.Property(e => e.ModDate)
                    .HasColumnName("MOD_DATE")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ModUserId).HasColumnName("MOD_USER_ID");

                entity.Property(e => e.QuestionId).HasColumnName("QUESTION_ID");

                entity.Property(e => e.QuizId).HasColumnName("QUIZ_ID");

                entity.Property(e => e.Status).HasColumnName("STATUS");

                entity.Property(e => e.UserId).HasColumnName("USER_ID");

                entity.HasOne(d => d.Answer)
                    .WithMany(p => p.History)
                    .HasForeignKey(d => d.AnswerId)
                    .HasConstraintName("FK_HISTORY_ANSWER_ID");

                entity.HasOne(d => d.Question)
                    .WithMany(p => p.History)
                    .HasForeignKey(d => d.QuestionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_HISTORY_QUESTION_ID");

                entity.HasOne(d => d.Quiz)
                    .WithMany(p => p.History)
                    .HasForeignKey(d => d.QuizId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_HISTORY_QUIZ_ID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.History)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_HISTORY_USER_ID");
            });

            modelBuilder.Entity<ListQuiz>(entity =>
            {
                entity.ToTable("LIST_QUIZ");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreateDate)
                    .HasColumnName("CREATE_DATE")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CreateUserId).HasColumnName("CREATE_USER_ID");

                entity.Property(e => e.ModDate)
                    .HasColumnName("MOD_DATE")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ModUserId).HasColumnName("MOD_USER_ID");

                entity.Property(e => e.Price).HasColumnName("PRICE");

                entity.Property(e => e.QuizId).HasColumnName("QUIZ_ID");

                entity.Property(e => e.Status).HasColumnName("STATUS");

                entity.Property(e => e.TypeAddedId).HasColumnName("TYPE_ADDED_ID");

                entity.Property(e => e.UserId).HasColumnName("USER_ID");

                entity.HasOne(d => d.Quiz)
                    .WithMany(p => p.ListQuiz)
                    .HasForeignKey(d => d.QuizId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_LIST_QUIZ_ID");

                entity.HasOne(d => d.TypeAdded)
                    .WithMany(p => p.ListQuiz)
                    .HasForeignKey(d => d.TypeAddedId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_LIST_TYPE_ID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.ListQuiz)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_LIST_USER_ID");
            });

            modelBuilder.Entity<Question>(entity =>
            {
                entity.ToTable("QUESTION");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Body)
                    .IsRequired()
                    .HasColumnName("BODY")
                    .HasColumnType("text");

                entity.Property(e => e.CategoryId).HasColumnName("CATEGORY_ID");

                entity.Property(e => e.Code)
                    .HasColumnName("CODE")
                    .HasColumnType("text");

                entity.Property(e => e.CreateDate)
                    .HasColumnName("CREATE_DATE")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CreateUserId).HasColumnName("CREATE_USER_ID");

                entity.Property(e => e.ImagePath)
                    .HasColumnName("IMAGE_PATH")
                    .HasColumnType("text");

                entity.Property(e => e.ModDate)
                    .HasColumnName("MOD_DATE")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ModUserId).HasColumnName("MOD_USER_ID");

                entity.Property(e => e.Position).HasColumnName("POSITION");

                entity.Property(e => e.Price).HasColumnName("PRICE");

                entity.Property(e => e.QuizId).HasColumnName("QUIZ_ID");

                entity.Property(e => e.RandomOption).HasColumnName("RANDOM_OPTION");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Question)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_QUESTION_CATEGORY_ID");

                entity.HasOne(d => d.Quiz)
                    .WithMany(p => p.Question)
                    .HasForeignKey(d => d.QuizId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_QUIZ_ID");
            });

            modelBuilder.Entity<QuestionCategory>(entity =>
            {
                entity.ToTable("QUESTION_CATEGORY");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Category)
                    .IsRequired()
                    .HasColumnName("CATEGORY")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.CreateDate)
                    .HasColumnName("CREATE_DATE")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CreateUserId).HasColumnName("CREATE_USER_ID");

                entity.Property(e => e.ModDate)
                    .HasColumnName("MOD_DATE")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ModUserId).HasColumnName("MOD_USER_ID");
            });

            modelBuilder.Entity<QuestionCorrectAnswer>(entity =>
            {
                entity.ToTable("QUESTION_CORRECT_ANSWER");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.AnswerId).HasColumnName("ANSWER_ID");

                entity.Property(e => e.CreateDate)
                    .HasColumnName("CREATE_DATE")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CreateUserId).HasColumnName("CREATE_USER_ID");

                entity.Property(e => e.ModDate)
                    .HasColumnName("MOD_DATE")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ModUserId).HasColumnName("MOD_USER_ID");

                entity.Property(e => e.QuestionId).HasColumnName("QUESTION_ID");

                entity.HasOne(d => d.Answer)
                    .WithMany(p => p.QuestionCorrectAnswer)
                    .HasForeignKey(d => d.AnswerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ANSWER_CORRECT_ID");

                entity.HasOne(d => d.Question)
                    .WithMany(p => p.QuestionCorrectAnswer)
                    .HasForeignKey(d => d.QuestionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_QUESTION_CORRECT_ID");
            });

            modelBuilder.Entity<QuestionOptionsAnswer>(entity =>
            {
                entity.ToTable("QUESTION_OPTIONS_ANSWER");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.AnswerId).HasColumnName("ANSWER_ID");

                entity.Property(e => e.CreateDate)
                    .HasColumnName("CREATE_DATE")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CreateUserId).HasColumnName("CREATE_USER_ID");

                entity.Property(e => e.ModDate)
                    .HasColumnName("MOD_DATE")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ModUserId).HasColumnName("MOD_USER_ID");

                entity.Property(e => e.QuestionId).HasColumnName("QUESTION_ID");

                entity.HasOne(d => d.Answer)
                    .WithMany(p => p.QuestionOptionsAnswer)
                    .HasForeignKey(d => d.AnswerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ANSWER_ID");

                entity.HasOne(d => d.Question)
                    .WithMany(p => p.QuestionOptionsAnswer)
                    .HasForeignKey(d => d.QuestionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_QUESTION_ID");
            });

            modelBuilder.Entity<Quiz>(entity =>
            {
                entity.ToTable("QUIZ");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Access)
                    .HasColumnName("access")
                    .HasMaxLength(32)
                    .IsFixedLength();

                entity.Property(e => e.AccessId).HasColumnName("ACCESS_ID");

                entity.Property(e => e.CategoryId).HasColumnName("CATEGORY_ID");

                entity.Property(e => e.CreateDate)
                    .HasColumnName("CREATE_DATE")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CreateUserId).HasColumnName("CREATE_USER_ID");

                entity.Property(e => e.Description)
                    .HasColumnName("DESCRIPTION")
                    .HasColumnType("text");

                entity.Property(e => e.ImagePath)
                    .HasColumnName("IMAGE_PATH")
                    .HasColumnType("text");

                entity.Property(e => e.ModDate)
                    .HasColumnName("MOD_DATE")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ModUserId).HasColumnName("MOD_USER_ID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("NAME")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.SettingId).HasColumnName("SETTING_ID");

                entity.Property(e => e.TopicId).HasColumnName("TOPIC_ID");

                entity.HasOne(d => d.AccessNavigation)
                    .WithMany(p => p.Quiz)
                    .HasForeignKey(d => d.AccessId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_QUIZ_ACESS_ID");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Quiz)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CATEGORY_ID");

                entity.HasOne(d => d.CreateUser)
                    .WithMany(p => p.Quiz)
                    .HasForeignKey(d => d.CreateUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CREATE_USER_ID");

                entity.HasOne(d => d.Setting)
                    .WithMany(p => p.Quiz)
                    .HasForeignKey(d => d.SettingId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_QUIZ_SETTING_ID");

                entity.HasOne(d => d.Topic)
                    .WithMany(p => p.Quiz)
                    .HasForeignKey(d => d.TopicId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TOPIC_ID");
            });

            modelBuilder.Entity<QuizAccess>(entity =>
            {
                entity.ToTable("QUIZ_ACCESS");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Access)
                    .IsRequired()
                    .HasColumnName("ACCESS")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.CreateDate)
                    .HasColumnName("CREATE_DATE")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CreateUserId).HasColumnName("CREATE_USER_ID");

                entity.Property(e => e.ModDate)
                    .HasColumnName("MOD_DATE")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ModUserId).HasColumnName("MOD_USER_ID");
            });

            modelBuilder.Entity<QuizCategory>(entity =>
            {
                entity.ToTable("QUIZ_CATEGORY");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Category)
                    .IsRequired()
                    .HasColumnName("CATEGORY")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.CreateDate)
                    .HasColumnName("CREATE_DATE")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CreateUserId).HasColumnName("CREATE_USER_ID");

                entity.Property(e => e.ModDate)
                    .HasColumnName("MOD_DATE")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ModUserId).HasColumnName("MOD_USER_ID");
            });

            modelBuilder.Entity<QuizSetting>(entity =>
            {
                entity.ToTable("QUIZ_SETTING");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.BlockTab).HasColumnName("BLOCK_TAB");

                entity.Property(e => e.CreateDate)
                    .HasColumnName("CREATE_DATE")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CreateUserId).HasColumnName("CREATE_USER_ID");

                entity.Property(e => e.DateEnd)
                    .HasColumnName("DATE_END")
                    .HasColumnType("smalldatetime");

                entity.Property(e => e.DateStart)
                    .HasColumnName("DATE_START")
                    .HasColumnType("smalldatetime");

                entity.Property(e => e.ModDate)
                    .HasColumnName("MOD_DATE")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ModUserId).HasColumnName("MOD_USER_ID");

                entity.Property(e => e.Price)
                    .HasColumnName("PRICE")
                    .HasColumnType("text");

                entity.Property(e => e.RandomPosition).HasColumnName("RANDOM_POSITION");

                entity.Property(e => e.TimerValue).HasColumnName("TIMER_VALUE");
            });

            modelBuilder.Entity<QuizTopic>(entity =>
            {
                entity.ToTable("QUIZ_TOPIC");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreateDate)
                    .HasColumnName("CREATE_DATE")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CreateUserId).HasColumnName("CREATE_USER_ID");

                entity.Property(e => e.ModDate)
                    .HasColumnName("MOD_DATE")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ModUserId).HasColumnName("MOD_USER_ID");

                entity.Property(e => e.Topic)
                    .IsRequired()
                    .HasColumnName("TOPIC")
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Token>(entity =>
            {
                entity.ToTable("TOKEN");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreateDate)
                    .HasColumnName("CREATE_DATE")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CreateUserId).HasColumnName("CREATE_USER_ID");

                entity.Property(e => e.ModDate)
                    .HasColumnName("MOD_DATE")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ModUserId).HasColumnName("MOD_USER_ID");

                entity.Property(e => e.RefreshToken).HasColumnName("REFRESH_TOKEN");

                entity.HasOne(d => d.CreateUser)
                    .WithMany(p => p.TokenCreateUser)
                    .HasForeignKey(d => d.CreateUserId)
                    .HasConstraintName("FK_CREATE_TOKEN_USER");

                entity.HasOne(d => d.ModUser)
                    .WithMany(p => p.TokenModUser)
                    .HasForeignKey(d => d.ModUserId)
                    .HasConstraintName("FK_MOD_TOKEN_USER");
            });

            modelBuilder.Entity<TypeList>(entity =>
            {
                entity.ToTable("TYPE_LIST");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreateDate)
                    .HasColumnName("CREATE_DATE")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CreateUserId).HasColumnName("CREATE_USER_ID");

                entity.Property(e => e.ModDate)
                    .HasColumnName("MOD_DATE")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ModUserId).HasColumnName("MOD_USER_ID");

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasColumnName("TYPE")
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("USER");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreateDate)
                    .HasColumnName("CREATE_DATE")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Email)
                    .HasColumnName("EMAIL")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.HistoryId).HasColumnName("HISTORY_ID");

                entity.Property(e => e.ModDate)
                    .HasColumnName("MOD_DATE")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ModUserId).HasColumnName("MOD_USER_ID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("NAME")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnName("PASSWORD")
                    .HasMaxLength(32)
                    .IsFixedLength();

                entity.Property(e => e.RoleId).HasColumnName("ROLE_ID");

                entity.Property(e => e.SettingId).HasColumnName("SETTING_ID");

                entity.Property(e => e.Surname)
                    .IsRequired()
                    .HasColumnName("SURNAME")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Verification).HasColumnName("VERIFICATION");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.User)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ROLE_ID");

                entity.HasOne(d => d.Setting)
                    .WithMany(p => p.User)
                    .HasForeignKey(d => d.SettingId)
                    .HasConstraintName("FK_SETTING_ID");
            });

            modelBuilder.Entity<UserRole>(entity =>
            {
                entity.ToTable("USER_ROLE");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreateDate)
                    .HasColumnName("CREATE_DATE")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CreateUserId).HasColumnName("CREATE_USER_ID");

                entity.Property(e => e.ModDate)
                    .HasColumnName("MOD_DATE")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ModUserId).HasColumnName("MOD_USER_ID");

                entity.Property(e => e.Role)
                    .IsRequired()
                    .HasColumnName("ROLE")
                    .HasMaxLength(30)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<UserSetting>(entity =>
            {
                entity.ToTable("USER_SETTING");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreateDate)
                    .HasColumnName("CREATE_DATE")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CreateUserId).HasColumnName("CREATE_USER_ID");

                entity.Property(e => e.ModDate)
                    .HasColumnName("MOD_DATE")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ModUserId).HasColumnName("MOD_USER_ID");

                entity.Property(e => e.Setting)
                    .IsRequired()
                    .HasColumnName("SETTING")
                    .HasMaxLength(30)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
