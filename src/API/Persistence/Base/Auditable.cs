namespace Database.Base;

public abstract class Auditable
{
    public DateTime Created { get; set; }
    public DateTime LastModified { get; set; }
}