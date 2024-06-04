namespace LantanaComfyAPI.Models;

public class Table
{ 
   public int Id { get; set; }
   public DateTime Date { get; set; }
   public DateTime Time { get; set; }
   public string RestaurantType { get; set; }
   public int NumberOfAdults { get; set; }
   public int NumberOfChildren { get; set; }
   public string DiningType { get; set; }
   public string Name { get; set; }
   public string Email { get; set; }
   public string Phone { get; set; }
   public string Message { get; set; }
   
}