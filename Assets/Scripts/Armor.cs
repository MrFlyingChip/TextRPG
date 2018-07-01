using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public enum ArmorWeight { Light, Medium, Heavy }
public enum ArmorType { Head, Body, Hands, Legs}

[XmlRoot("ArmorList")]
public class ArmorList
{
    [XmlElement("Armor")]
    public List<Armor> Armors { get; set; }
}

public class Armor : Item
{
    public int ArmorRate { get; set; }
	public ArmorWeight ArmorWeight { get; set; }
    public ArmorType ArmorType { get; set; }

    public int Intelligence { get; set; }
    public int Power { get; set; }
    public int Strength { get; set; }
    public int Agility { get; set; }
    public int DodgeChance { get; set; }
    public int CritChance { get; set; }
    public int BlockChance { get; set; }
}
