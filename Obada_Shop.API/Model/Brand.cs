﻿namespace Obada_Shop.API.Model
{
    public class Brand
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Status { get; set; }
        public ICollection<Product> Products { get; } = new List<Product>();
    }
}
