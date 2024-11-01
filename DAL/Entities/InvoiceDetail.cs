namespace DAL.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class InvoiceDetail
    {
        public int InvoiceDetailId { get; set; }

        public int InvoiceId { get; set; }

        [StringLength(255)]
        public string Description { get; set; }

        public decimal Amount { get; set; }

        public virtual Invoice Invoice { get; set; }
    }
}
