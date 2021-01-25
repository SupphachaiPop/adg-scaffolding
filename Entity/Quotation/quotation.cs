using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Scaffolding.Models.Tables
{
    [Table("quotation")]
    public class quotation
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(@"quotation_id", Order = 1, TypeName = SQLSERVER_CONST.INT)]
        [Required]
        [Key]
        public int quotation_id { get; set; } // quotation_id (Primary key)

        [Column(@"quotation_no", Order = 2, TypeName = SQLSERVER_CONST.VARCHAR_20)]
        [Required]
        [MaxLength(20)]
        [StringLength(20)]
        public string quotation_no { get; set; } // quotation_no (length: 20)

        [Column(@"quotation_date", Order = 3, TypeName = SQLSERVER_CONST.DATETIME)]
        [Required]
        public System.DateTime quotation_date { get; set; } // quotation_date

        [Column(@"customer_id", Order = 4, TypeName = SQLSERVER_CONST.INT)]
        [Required]
        public int customer_id { get; set; } // customer_id

        [Column(@"sub_total", Order = 5, TypeName = SQLSERVER_CONST.DECIMAL_18_0)]
        [Required]
        public decimal sub_total { get; set; } // sub_total

        [Column(@"discount", Order = 6, TypeName = SQLSERVER_CONST.DECIMAL_18_0)]
        [Required]
        public decimal discount { get; set; } // discount

        [Column(@"vat", Order = 7, TypeName = SQLSERVER_CONST.DECIMAL_18_0)]
        [Required]
        public decimal vat { get; set; } // vat

        [Column(@"security_money", Order = 8, TypeName = SQLSERVER_CONST.DECIMAL_18_0)]
        [Required]
        public decimal security_money { get; set; } // security_money

        [Column(@"total", Order = 9, TypeName = SQLSERVER_CONST.DECIMAL_18_0)]
        [Required]
        public decimal total { get; set; } // total

        [Column(@"comment", Order = 10, TypeName = SQLSERVER_CONST.VARCHAR_4000)]
        [MaxLength(4000)]
        [StringLength(4000)]
        public string comment { get; set; } // comment (length: 4000)

        [Column(@"created_by", Order = 11, TypeName = SQLSERVER_CONST.INT)]
        public int? created_by { get; set; } // created_by

        [Column(@"created_date", Order = 12, TypeName = SQLSERVER_CONST.DATETIME)]
        public System.DateTime? created_date { get; set; } // created_date

        [Column(@"modified_by", Order = 13, TypeName = SQLSERVER_CONST.INT)]
        public int? modified_by { get; set; } // modified_by

        [Column(@"is_referred", Order = 14, TypeName = SQLSERVER_CONST.BIT)]
        public bool? is_referred { get; set; } // is_referred

        [Column(@"modified_date", Order = 15, TypeName = SQLSERVER_CONST.DATETIME)]
        public System.DateTime? modified_date { get; set; } // modified_date

        [Column(@"is_active", Order = 16, TypeName = SQLSERVER_CONST.BIT)]
        public bool? is_active { get; set; } // is_active

        [Column(@"is_deleted", Order = 17, TypeName = SQLSERVER_CONST.BIT)]
        public bool? is_deleted { get; set; } // is_deleted

        public virtual ICollection<quotation_item> quotation_item { get; set; } // quotation_item.FK_quotation_item_quotation

        public virtual customer customer { get; set; } // FK_Table_1_customer_customer_id

        public quotation()
        {
            this.quotation_item = new List<quotation_item>();
        }
    }
}

