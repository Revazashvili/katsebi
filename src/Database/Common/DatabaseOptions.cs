namespace Database.Common;

public record DatabaseOptions(bool? UseInMemoryDatabase, string? ConnectionString);