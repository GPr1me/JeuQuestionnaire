namespace Game.Dal.Models
{
  public sealed record GameContextOptions
  {
    public string ConnectionString { get => $"Host={Host};Database={Database};Username={Username};Password={Password}"; }
    public required string AssemblyName { get; init; }
    public required string Host { get; init; }
    public required string Database { get; init; }
    public required string Username { get; init; }
    public required string Password { get; init; }
  }
}
