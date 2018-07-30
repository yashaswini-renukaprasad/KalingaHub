namespace KalingaHub.DataAccess.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Upvote")]
    public partial class Upvote
    {
        [Key]
        [Column(Order = 0)]
        public Guid ResourceId { get; set; }

        [Key]
        [Column(Order = 1)]
        public Guid UserId { get; set; }
    }
}
