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
        Console.WriteLine($"{Name}'s nowy wynik to: {Score}");
    }

    public void PlayerInfo()
    {
        Console.WriteLine($"Zawodnik: {Name}, Pozycja: {Position}, Punkty: {Score}");
    }
}