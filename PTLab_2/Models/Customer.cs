namespace PTLab_2.Models;

public class Customer
{
    public Guid Id { get; set; }
    public string Login { get; set; }
    public string Password { get; set; }
    public string? Name { get; set; }
    public string? Purchase { get; set; }

}