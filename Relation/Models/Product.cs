﻿namespace Relation.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public int Amount { get; set; }
        public ProductExtend ProductExtend { get; set; }

        public int CategoryId { get; set; } //Auto ชื่อเดียวกัน คีย์นอกที่เชื่อต่อไปยังไฟล์ Category
        //[ForeignKey("TestId")]
        public Category Category { get; set; }

    }
}
