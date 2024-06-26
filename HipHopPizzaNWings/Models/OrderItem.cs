﻿namespace HipHopPizzaNWings.Models
{
    public class OrderItem
    {
        public int Id { get; set; }

        public int OrderId { get; set; }

        public int ItemId { get; set; }
        public Order? Order { get; set; }
        public Item? Item { get; set; }
        public decimal? Price { get
            {
                return Item?.Price;
            }
        }
    }
}
