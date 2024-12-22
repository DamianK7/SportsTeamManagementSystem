using System;
using System.Collections.Generic;
using System.Linq;

public interface IPlayer
{
    string Name { get; set; }
    string Position { get; set; }
    int Score { get; set; }
    void PlayerInfo();
}

public class Player : IPlayer
{
    public string Name { get; set; }
    public string Position { get; set; }
    public int Score { get; set; }

    public Player(string name, string position, int score)
    {
        Name = name;
        Position = position;
        Score = score;
    }

    public void UpdateScore(int points)
    {
        Score = points;
        Console.WriteLine($"{Name} nowy wynik to: {Score}");
    }

    public void PlayerInfo()
    {
        Console.WriteLine($"Zawodnik: {Name}, Pozycja: {Position}, Punkty: {Score}");
    }

    public static List<Player> SearchPlayersByPosition(List<Player> players, string position)
    {
        position = position.ToLower().Trim();
        return players.Where(x => x.Position == position).ToList();
    }
}

public class Team
{
    public List<IPlayer> Players = new List<IPlayer>();

    public void AddPlayer()
    {
        Console.WriteLine("Ilu zawodników chcesz dodać?");
        if (!int.TryParse(Console.ReadLine(), out int amount) || amount <= 0)
        {
            Console.WriteLine("Nieprawidłowa liczba!");
            return;
        }

        for (int i = 0; i < amount; i++)
        {
            Console.WriteLine("Podaj nazwisko zawodnika:");
            string name = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(name))
            {
                Console.WriteLine("Nazwa zawodnika nie może być pusta!");
                i--;
                continue;
            }

            Console.WriteLine("Podaj pozycję zawodnika:");
            string position = Console.ReadLine().ToLower().Trim();
            if (string.IsNullOrWhiteSpace(position))
            {
                Console.WriteLine("Pozycja nie może być pusta!");
                continue;
            }

            Console.WriteLine("Podaj początkowy wynik zawodnika:");
            if (!int.TryParse(Console.ReadLine(), out int score) || score < 0)
            {
                Console.WriteLine("Nieprawidłowy wynik!");
                i--;
                continue;
            }

            Players.Add(new Player(name, position, score));
            Console.WriteLine($"Dodano zawodnika: {name}, Pozycja: {position}, Wynik: {score}");
        }
    }

    public void RemovePlayer()
    {
        Console.WriteLine("Podaj nazwisko zawodnika do usunięcia:");
        string name = Console.ReadLine();
        var playerToRemove = Players.Find(p => p.Name == name);
        if (playerToRemove != null)
        {
            Players.Remove(playerToRemove);
            Console.WriteLine($"Zawodnik {name} został usunięty.");
        }
        else
        {
            Console.WriteLine("Zawodnik nie został znaleziony.");
        }
    }

    public int CalculateTotalPoints()
    {
        return Players.Sum(p => p.Score);
    }

    public void DisplayTeamStats()
    {
        Console.WriteLine($"Suma punktów drużyny: {CalculateTotalPoints()}");
    }

    public static double CalculateAverageScore(List<Player> players)
    {
        return players.Average(x => x.Score);
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        Team team = new Team();
        var playersList = team.Players.Cast<Player>().ToList();

        while (true)
        {
            Console.WriteLine("\nMenu:");
            Console.WriteLine("1. Dodaj zawodnika");
            Console.WriteLine("2. Usuń zawodnika");
            Console.WriteLine("3. Zaktualizuj wynik zawodnika");
            Console.WriteLine("4. Wyszukaj zawodników po pozycji");
            Console.WriteLine("5. Wyświetl statystyki drużyny");
            Console.WriteLine("6. Oblicz średnią punktów drużyny");
            Console.WriteLine("7. Zakończ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    team.AddPlayer();
                    break;
                case "2":
                    team.RemovePlayer();
                    break;
                case "3":
                    Console.WriteLine("Aktualizacja wyniku zawodnika:");
                    Player playerToUpdate = null;
                    while (playerToUpdate == null)
                    {
                        Console.WriteLine("Podaj nazwisko zawodnika, którego wynik chcesz zaktualizować:");
                        string playerName = Console.ReadLine();
                        playerToUpdate = team.Players.Cast<Player>().FirstOrDefault(p => p.Name.Equals(playerName, StringComparison.OrdinalIgnoreCase));
                        if (playerToUpdate == null)
                        {
                            Console.WriteLine("Zawodnik nie został znaleziony. Spróbuj ponownie.");
                        }
                    }
                    Console.WriteLine("Podaj nowy wynik:");
                    if (int.TryParse(Console.ReadLine(), out int newScore))
                    {
                        playerToUpdate.UpdateScore(newScore);
                    }
                    else
                    {
                        Console.WriteLine("Nieprawidłowy wynik!");
                    }
                    break;
                case "4":
                    Console.WriteLine("Podaj pozycję, aby wyszukać zawodników:");
                    string position = Console.ReadLine();
                    var foundPlayers = Player.SearchPlayersByPosition(team.Players.Cast<Player>().ToList(), position);
                    if (foundPlayers.Any())
                    {
                        foreach (var player in foundPlayers)
                        {
                            player.PlayerInfo();
                        }
                    }
                    else
                    {
                        Console.WriteLine($"Nie znaleziono zawodników na pozycji: {position}");
                    }
                    break;
                case "5":
                    Console.WriteLine("Wyświetlanie statystyk drużyny:");
                    team.DisplayTeamStats();
                    break;
                case "6":
                    double averageScore = Team.CalculateAverageScore(team.Players.Cast<Player>().ToList());
                    Console.WriteLine($"Średni wynik drużyny: {averageScore}");
                    break;
                case "7":
                    Console.WriteLine("Kończenie programu...");
                    return;
                default:
                    Console.WriteLine("Nieprawidłowa opcja! Spróbuj ponownie.");
                    break;
            }
        }
    }
}
