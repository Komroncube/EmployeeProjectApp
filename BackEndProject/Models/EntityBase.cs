namespace BackEndProject.Models;

public class EntityBase
{
    public int Id { get; set; }
    public Status Status { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime ModifyDate { get; set; }
    public DateTime DeletedDate { get; set; }
}
