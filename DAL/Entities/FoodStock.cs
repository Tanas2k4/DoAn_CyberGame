namespace DAL.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("FoodStock")]
    public partial class FoodStock
    {
        public int FoodStockId { get; set; }

        public int FoodItemId { get; set; }

        public int Quantity { get; set; }

        public DateTime LastUpdated { get; set; }

        public virtual FoodItem FoodItem { get; set; }
    }
}
