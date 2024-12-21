using System;
using System.Collections.Generic;
using System.Linq;

interface IPlayer
{
    string Name { get; set; }
    string Position { get; set; }
    int Score { get; set; }
    void PlayerInfo();
}