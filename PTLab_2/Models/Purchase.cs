namespace PTLab_2.Models;

public class Purchase
{
    public int Id { get; set; }
    public int Quantity { get; set; }
    public int CustomerId { get; set; }
    public int ProductId { get; set; }
   /* public string? ProductName { get; set; }
    public double Price { get; set; }
    public int Discount { get; set; }*/
}