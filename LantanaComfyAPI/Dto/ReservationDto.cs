namespace LantanaComfyAPI.Dto;

public class ReservationDto
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public DateTime Time { get; set; }
    public string RoomType { get; set; }
    public string RoomSize { get; set; }
    public string RoomServings { get; set; }
    public int Days { get; set; }
    public int NumberOfChildren { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Phone{get; set; }
    public string Message { get; set; } 
}