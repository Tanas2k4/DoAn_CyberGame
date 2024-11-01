namespace DAL.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Order
    {
        public int OrderId { get; set; }

        public int SessionId { get; set; }

        public int FoodItemId { get; set; }

        public int Quantity { get; set; }

        public decimal Price { get; set; }

        public virtual FoodItem FoodItem { get; set; }

        public virtual MachineSession MachineSession { get; set; }
    }
}
