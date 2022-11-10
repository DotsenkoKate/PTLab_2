using Microsoft.EntityFrameworkCore;
using PTLab_2.Models;


namespace PTLab_2.Database;

public class HardwareStoreContext:DbContext
{
    public DbSet<Product> Products { get; set; }

}