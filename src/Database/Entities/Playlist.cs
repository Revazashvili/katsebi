namespace Database.Entities;

#pragma warning disable CS8618
public class Playlist
{
    public int Id { get; set; }
    public string Name { get; set; }
    public ICollection<Episode> Episodes { get; set; }
}