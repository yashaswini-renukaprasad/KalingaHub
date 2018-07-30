namespace KalingaHub.DataAccess.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class QuestionTag
    {
        [Key]
        [Column(Order = 0)]
        public Guid QuestionId { get; set; }

        [Key]
        [Column(Order = 1)]
        public Guid TagId { get; set; }
    }
}
