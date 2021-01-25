using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Scaffolding.Models.Tables
{
    [Table("quotation_item")]
    public class quotation_item
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(@"quotation_item_id", Order = 1, TypeName = SQLSERVER_CONST.INT)]
        [Required]
        [Key]
        public int quotation_item_id { get; set; } // quotation_item_id (Primary key)

        [Column(@"quotation_id", Order = 2, TypeName = SQLSERVER_CONST.INT)]
        [Required]
        public int quotation_id { get; set; } // quotation_id

        [Column(@"template_id", Order = 3, TypeName = SQLSERVER_CONST.INT)]
        public int? template_id { get; set; } // template_id

        [Column(@"description", Order = 4, TypeName = SQLSERVER_CONST.VARCHAR_300)]
        [Required]
        [MaxLength(300)]
        [StringLength(300)]
        public string description { get; set; } // description (length: 300)

        [Column(@"qty", Order = 5, TypeName = SQLSERVER_CONST.DECIMAL_18_0)]
        [Required]
        public decimal qty { get; set; } // qty

        [Column(@"uom_id", Order = 6, TypeName = SQLSERVER_CONST.INT)]
        [Required]
        public int uom_id { get; set; } // uom_id

        [Column(@"qty_day", Order = 7, TypeName = SQLSERVER_CONST.INT)]
        [Required]
        public int qty_day { get; set; } // qty_day

        [Column(@"unit_price", Order = 8, TypeName = SQLSERVER_CONST.DECIMAL_18_0)]
        [Required]
        public decimal unit_price { get; set; } // unit_price

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

        public virtual quotation quotation { get; set; } // FK_quotation_item_quotation
        public virtual uom uom { get; set; } // FK_quotation_item_uom

    }
}

