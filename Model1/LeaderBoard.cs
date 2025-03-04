using Model1;

public class Leaderboard : BaseEntity
{
    public int UserId { get; set; }
    public int Points { get; set; }

    public Leaderboard(int userId, int points)
    {
        UserId = userId;
        Points = points;
    }

    public void UpdatePoints(int newPoints) => Points = newPoints;
    public int GetPoints() => Points;

    public static int ComparePoints(Leaderboard l1, Leaderboard l2) => l2.Points.CompareTo(l1.Points);

    public override string ToString() => $"{UserId}: {Points} points";
}