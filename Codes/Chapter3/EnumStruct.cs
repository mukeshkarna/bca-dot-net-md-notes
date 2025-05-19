// Enum for game difficulty
public enum GameDifficulty
{
  Easy = 1,
  Medium = 2,
  Hard = 3,
  Expert = 4
}

// Struct for game score
public struct GameScore
{
  public string PlayerName { get; set; }
  public int Score { get; set; }
  public GameDifficulty Difficulty { get; set; }
  public DateTime DateAchieved { get; set; }

  public GameScore(string playerName, int score, GameDifficulty difficulty)
  {
    PlayerName = playerName;
    Score = score;
    Difficulty = difficulty;
    DateAchieved = DateTime.Now;
  }

  public override string ToString()
  {
    return $"{PlayerName}: {Score} points ({Difficulty}) - {DateAchieved:yyyy-MM-dd}";
  }
}

// Class for game session
public class GameSession
{
  private List<GameScore> scores = new List<GameScore>();

  public void AddScore(GameScore score)
  {
    scores.Add(score);
  }

  public List<GameScore> GetHighScoresForDifficulty(GameDifficulty difficulty)
  {
    return scores
        .Where(s => s.Difficulty == difficulty)
        .OrderByDescending(s => s.Score)
        .ToList();
  }

  public GameScore? GetHighestScore()
  {
    if (scores.Count == 0)
      return null;

    return scores.OrderByDescending(s => s.Score).First();
  }

  public List<GameScore> GetTopNScores(int n)
  {
    return scores
        .OrderByDescending(s => s.Score)
        .Take(n)
        .ToList();
  }

  public Dictionary<GameDifficulty, int> GetAverageScoresByDifficulty()
  {
    return scores
        .GroupBy(s => s.Difficulty)
        .ToDictionary(
            g => g.Key,
            g => (int)g.Average(s => s.Score)
        );
  }
}

// Usage example:
class Program
{
  static void Main()
  {
    GameSession session = new GameSession();

    // Add some scores
    session.AddScore(new GameScore("Alice", 1500, GameDifficulty.Easy));
    session.AddScore(new GameScore("Bob", 2500, GameDifficulty.Medium));
    session.AddScore(new GameScore("Charlie", 3500, GameDifficulty.Hard));
    session.AddScore(new GameScore("Alice", 4000, GameDifficulty.Expert));
    session.AddScore(new GameScore("David", 2000, GameDifficulty.Medium));
    session.AddScore(new GameScore("Eve", 3000, GameDifficulty.Hard));

    // Get high scores for specific difficulty
    Console.WriteLine("Hard difficulty high scores:");
    var hardScores = session.GetHighScoresForDifficulty(GameDifficulty.Hard);
    foreach (var score in hardScores)
    {
      Console.WriteLine(score);
    }

    // Get overall highest score
    Console.WriteLine("\nHighest score overall:");
    var highest = session.GetHighestScore();
    if (highest.HasValue)
      Console.WriteLine(highest.Value);

    // Get top 3 scores
    Console.WriteLine("\nTop 3 scores:");
    var topScores = session.GetTopNScores(3);
    foreach (var score in topScores)
    {
      Console.WriteLine(score);
    }

    // Get average scores by difficulty
    Console.WriteLine("\nAverage scores by difficulty:");
    var averages = session.GetAverageScoresByDifficulty();
    foreach (var kvp in averages)
    {
      Console.WriteLine($"{kvp.Key}: {kvp.Value}");
    }
  }
}