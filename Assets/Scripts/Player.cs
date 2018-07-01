using System.Collections;
using System.Collections.Generic;
using System;
using System.Xml.Serialization;
using UnityEngine;

public enum Message { LevelUp, Death, ExperienceGained, Dodged, DamageGained, Blocked}

[XmlRoot("PlayerList")]
public class PlayerList
{
    [XmlElement("Player")]
    public List<Player> Players { get; set; }
}

public class Player
{
    public string Name { get; set; }

    public int MaxHealth { get; set; }
    public int MaxMana { get; set; }

    public int CurrentHealth { get; set; }
    public int CurrentMana { get; set; }

    public int Intelligence { get; set; }
    public int Power { get; set; }
    public int Strength { get; set; }
    public int Agility { get; set; }

    public int Level { get; set; }
    public int CurrentExperience { get; set; }
    public double ExperienceToLevelUp { get; set; }

    public int AttackRate { get; set; }
    public int DodgeChance { get; set; }
    public int CritChance { get; set; }

    public int Armor { get; set; }
    public int DamageAbsorption { get; set; }

    public int BlockChance { get; set; }

    public List<int> Pocket { get; set; }
    public int Gold { get; set; }

    public int HeadID { get; set; }
    public int BodyID { get; set; }
    public int HandsID { get; set; }
    public int LegsID { get; set; }

    public Player()
    {
        Pocket = new List<int>();
    }

    public Player(string name)
    {
        Name = name;
        Pocket = new List<int>();
        Intelligence = 15;
        Power = 15;
        Strength = 15;
        Agility = 15;
        Level = 1;
        ExperienceToLevelUp = 200;
        HeadID = 1;
        BodyID = 2;
        HandsID = 3;
        LegsID = 4;
        ReCountCharacteristics();
        CurrentHealth = MaxHealth;
        CurrentMana = MaxMana;
    }

    public Message GainDamage (int damage)
    {
        float dodge = UnityEngine.Random.Range(0, 100);
        if(dodge < (DodgeChance * 0.04f))
        {
            return Message.Dodged;
        }

        int fullDamage = (int)(damage - damage * (((double)DamageAbsorption) / 100));

        CurrentHealth -= fullDamage;
        if(CurrentHealth <= 0)
        {
            return Message.Death;
        }
        return Message.DamageGained;
    }

    public void WearUp(int itemID, ArmorType type)
    {
        WearDown(type);
        switch (type)
        {
            case ArmorType.Body:               
                Pocket.Remove(BodyID);
                BodyID = itemID;
                ChangeCharacteristicsWithItem(BodyID, 2);                            
                break;
            case ArmorType.Hands:               
                Pocket.Remove(HandsID);
                HandsID = itemID;
                ChangeCharacteristicsWithItem(HandsID, 2);               
                break;
            case ArmorType.Head:
                Pocket.Remove(HeadID);
                HeadID = itemID;
                ChangeCharacteristicsWithItem(HeadID, 2);
                break;
            case ArmorType.Legs:
                Pocket.Remove(LegsID);
                LegsID = itemID;
                ChangeCharacteristicsWithItem(LegsID, 2);            
                break;
        }
    }

    public void WearDown(ArmorType type)
    {
        switch (type)
        {
            case ArmorType.Body:
                if (BodyID != -1)
                {
                    Pocket.Add(BodyID);
                    ChangeCharacteristicsWithItem(BodyID, 1);
                }
                BodyID = -1;
                break;
            case ArmorType.Hands:
                if (HandsID != -1)
                {
                    Pocket.Add(HandsID);
                    ChangeCharacteristicsWithItem(HandsID, 1);
                }
                HandsID = -1;
                break;
            case ArmorType.Head:
                if (HeadID != -1)
                {
                    Pocket.Add(HeadID);
                    ChangeCharacteristicsWithItem(HeadID, 1);
                }
                HeadID = -1;
                break;
            case ArmorType.Legs:
                if (LegsID != -1)
                {
                    Pocket.Add(LegsID);
                    ChangeCharacteristicsWithItem(LegsID, 1);
                }
                LegsID = -1;
                break;
        }
    }

    private void ChangeCharacteristicsWithItem(int itemID, int add)
    {
        Armor armor = LoaderManager.LoadArmor(itemID);
        Armor += armor.ArmorRate * (int)Math.Pow(-1, add);
        Agility += armor.Agility * (int)Math.Pow(-1, add);
        Intelligence += armor.Intelligence * (int)Math.Pow(-1, add);
        Strength += armor.Strength * (int)Math.Pow(-1, add);
        Power += armor.Power * (int)Math.Pow(-1, add);
        DodgeChance += armor.DodgeChance * (int)Math.Pow(-1, add);
        CritChance += armor.CritChance * (int)Math.Pow(-1, add);
        BlockChance += armor.BlockChance * (int)Math.Pow(-1, add);
    }

    private void ReCountCharacteristics()
    {
        MaxHealth = Strength * 17;
        MaxMana = Intelligence * 13;
        DodgeChance = Agility * 3;
        AttackRate = Power;
        CritChance = (Agility + Power) * 2;
    }

    private void LevelUp()
    {
        Intelligence++;
        Power++;
        Strength++;
        Agility++;
        Level++;
        ExperienceToLevelUp *= 2.5;
        ReCountCharacteristics();
        CurrentHealth = MaxHealth;
        CurrentMana = MaxMana;
    }

    public Message GainExperience (int experience)
    {
        CurrentExperience += experience;
        if(CurrentExperience >= ExperienceToLevelUp)
        {
            CurrentExperience -= (int)ExperienceToLevelUp;
            LevelUp();
            return Message.LevelUp;
        }
        return Message.ExperienceGained;
    }
}
