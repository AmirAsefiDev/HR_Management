namespace ERP.Application.Interfaces.Email;

public class EmailDto
{
    public string Title { get; set; }
    public string MessageBody { get; set; }
    public string Destination { get; set; }
}