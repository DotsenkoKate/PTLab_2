namespace PTLab_2.Models;

public class Cart
{
    public int Id { get; set; }
    public int? Quantity { get; set; }
     public int CustomerId { get; set; }
    public int? ProductId { get; set; }
    
}  
    
//Мусор
    /*  private List<CartLine> lineCollection = new List<CartLine>();
      public IEnumerable<CartLine> Lines { get { return lineCollection; } }
  
      public void AddItem(Product product, int quantity)
      {
          CartLine line = lineCollection
              .Where(b => b.Product.Id == product.Id)
              .FirstOrDefault();
  
          if (line == null)
          {
              lineCollection.Add(new CartLine { Product = product, Quantity = quantity });
          }
          else
          {
              line.Quantity += quantity;
          }
      }
  
      public void RemoveLine(Product product)
      {
          lineCollection.RemoveAll(l => l.Product.Id == product.Id);
      }
  
      public float? ComputeTotalValue()
      {
          return lineCollection.Sum(e => e.Product.Price * e.Quantity);
      }
  
      public void Clear()
      {
          lineCollection.Clear();
      }
  }
      
      public class CartLine
      {
          public Product Product { get; set; }
          public int Quantity { get; set; }
      }
      */

