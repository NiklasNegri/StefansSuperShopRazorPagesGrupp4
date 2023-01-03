using StefansSuperShop.Data.Entities;
using System.Collections.Generic;

namespace StefansSuperShop.Data.DTOs;

public class ProductDTO
{
    public int ProductId { get; set; }
    public string ProductName { get; set; }
    public string ProductDescription { get; set; }
    public int? SupplierId { get; set; }
    public int? CategoryId { get; set; }
    public string QuantityPerUnit { get; set; }
    public decimal? UnitPrice { get; set; }
    public short? UnitsInStock { get; set; }
    public short? UnitsOnOrder { get; set; }
    public short? ReorderLevel { get; set; }
    public bool Discontinued { get; set; }

    public virtual Category Category { get; set; }
    public virtual Supplier Supplier { get; set; }
    public virtual ICollection<OrderDetail> OrderDetails { get; set; }
}
