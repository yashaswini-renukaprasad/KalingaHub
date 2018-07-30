namespace KalingaHub.DataAccess.Entities
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class KalingaHubDBModel : DbContext
    {
        public KalingaHubDBModel()
            : base("name=KalingaHubDatabase")
        {
        }

        public virtual DbSet<Answer> Answers { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Question> Questions { get; set; }
        public virtual DbSet<Tag> Tags { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<QuestionTag> QuestionTags { get; set; }
        public virtual DbSet<Upvote> Upvotes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
