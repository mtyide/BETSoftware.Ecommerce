﻿namespace BETSoftware.Domain.Models.Dtos
{
    public class ProductInDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageUri { get; set; }
        public decimal? Price { get; set; }
        public bool? Active { get; set; }
    }
}
