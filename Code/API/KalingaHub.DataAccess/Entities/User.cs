namespace KalingaHub.DataAccess.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("User")]
    public partial class User
    {
        public Guid Id { get; set; }

        [Required]
        [StringLength(10)]
        public string Mid { get; set; }
    }
}
