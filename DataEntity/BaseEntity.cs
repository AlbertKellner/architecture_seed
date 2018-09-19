namespace DataEntity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public abstract class BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 0)]
        public int Id { get; set; }
        
        //public string IdentityId { get; set; }

        [Required]
        [Column(Order = 1)]
        public DateTime AddedDate { get; set; }

        [Required]
        [Column(Order = 2)]
        public DateTime ModifiedDate { get; set; }

        [NotMapped]
        public List<string> ValidationErrors { get; set; }

        [NotMapped]
        public bool IsItNew => Id <= 0;

        public override string ToString() => $"Id={Id} | Type={GetType()}";
    }
}