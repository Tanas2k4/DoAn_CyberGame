namespace DAL.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Maintenance")]
    public partial class Maintenance
    {
        public int MaintenanceId { get; set; }

        public int MachineId { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime? EndTime { get; set; }

        public decimal? Cost { get; set; }

        [StringLength(255)]
        public string Description { get; set; }

        public virtual Machine Machine { get; set; }
    }
}
