namespace PTLab_2.Models;

public class CartFinal
{
    public int Id { get; set; }
    public int Quantity { get; set; }
    public string CustomerId { get; set; }
    public string? ProductName { get; set; }
    public int Price { get; set; }
    public int? Discount { get; set; }
}