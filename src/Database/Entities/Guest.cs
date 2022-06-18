namespace Database.Entities;

#pragma warning disable CS8618
public class Guest
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public ICollection<Episode> Episodes { get; set; }
}