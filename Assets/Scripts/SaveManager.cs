using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;

public static class SaveManager
{
    public static int CreatePlayer(string name)
    {
        PlayerList players = LoaderManager.LoadPlayers();
        foreach (var player in players.Players)
        {
            if(player.Name == name)
            {
                return 0;
            }
        }
        Player newPlayer = new Player(name);
        SavePlayer(newPlayer);
        PlayerPrefs.SetString("CurrentPlayer", name);
        return 1;
    }

    public static void SavePlayer(Player player)
    {
        PlayerList players = LoaderManager.LoadPlayers();
        players.Players.Add(player);
        XmlSerializer formatter = new XmlSerializer(typeof(PlayerList));
        using (FileStream fs = new FileStream("Player.xml", FileMode.OpenOrCreate))
        {
            formatter.Serialize(fs, players);
        }
    }
    
}
