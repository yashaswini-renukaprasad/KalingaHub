namespace KalingaHub.DataAccess.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Answer")]
    public partial class Answer
    {
        public Guid Id { get; set; }

        public Guid QuestionId { get; set; }

        [Required]
        public string Description { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public Guid CreatedBy { get; set; }
    }
}
