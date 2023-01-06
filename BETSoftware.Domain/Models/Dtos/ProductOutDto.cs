﻿namespace BETSoftware.Domain.Models.Dtos
{
    public class ProductOutDto
    {
        public int Id { get; set; }
        public int Name { get; set; }
        public string? Description { get; set; }
        public string? ImageUri { get; set; }
        public decimal? Price { get; set; }
    }
}