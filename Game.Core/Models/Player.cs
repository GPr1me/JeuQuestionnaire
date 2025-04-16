namespace Game.Core.Models
{
  public class Player
  {
    private static readonly string[] Adjectives = { "Furieux", "Rapide", "Charmant", "Étrange", "Malin", "Joyeux" };
    private static readonly string[] Nouns = { "Canard", "Baguette", "Chapeau", "Chat", "Escargot", "Croissant" };
    private static readonly Random Random = new();

    public string Id { get; set; }
    public string Name { get; set; }
    public string? LastMessage { get; set; }
    public DateTime? LastMessageDate { get; set; }
    public int Score { get; set; } = 0;

    public Player(string id)
    {
      Id = id;
      Name = GenerateRandomFunnyName();
    }

    public void ClearLastMessage()
    {
      LastMessage = null;
      LastMessageDate = null;
    }

    private static string GenerateRandomFunnyName()
    {
      var adjective = Adjectives[Random.Next(Adjectives.Length)];
      var noun = Nouns[Random.Next(Nouns.Length)];
      return $"{adjective} {noun}";
    }
  }
}
