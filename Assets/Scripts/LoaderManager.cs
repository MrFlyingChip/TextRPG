using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;

public static class LoaderManager
{

    private static T Load<T>(string path)
    {
        T List = default(T);
        string filePath = path;
        TextAsset targetFile = Resources.Load<TextAsset>(filePath);
        XmlSerializer ser = new XmlSerializer(typeof(T));
        using (StringReader sr = new StringReader(targetFile.text))
        {
            List = (T)ser.Deserialize(sr);
        }
        return List;    
    }

    public static Armor LoadArmor(int id)
    {
        ArmorList armors = Load<ArmorList>("Armor");
        Armor armor = armors.Armors.Find((x) => x.ID == id);
        return armor;
    }

    public static Item LoadItem(int id)
    {
        ItemList items = Load<ItemList>("Item");
        Item item = items.Items.Find((x) => x.ID == id);
        return item;
    }

    public static LanguageList LoadLanguages()
    {
        LanguageList languages = Load<LanguageList>("Language");
        return languages;
    }


    public static PlayerList LoadPlayers()
    {
        PlayerList players = new PlayerList();
        XmlSerializer ser = new XmlSerializer(typeof(PlayerList));
        if (!File.Exists("Player.xml"))
        {
            XmlSerializer formatter = new XmlSerializer(typeof(PlayerList));
            using (FileStream fs = new FileStream("Player.xml", FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, players);
            }
        }
        using (FileStream fs = new FileStream("Player.xml", FileMode.OpenOrCreate))
        {
            players = (PlayerList)ser.Deserialize(fs);
        }
        return players;
    }

}
