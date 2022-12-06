namespace PTLab_2.Models;

public class Cart
{
    public int Quantity { get; set; }
    public string? ProductName { get; set; } 
    public double Price { get; set; }
    public int Discount { get; set; }
}