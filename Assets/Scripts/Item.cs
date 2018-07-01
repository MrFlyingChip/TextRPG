using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

[XmlRoot("ItemList")]
public class ItemList
{
    [XmlElement("Item")]
    public List<Item> Items { get; set; }
}

public class Item
{
    public int ID { get; set; }
    public string Name { get; set; }
    public string Description { set; get; }
    public int ItemLevel { get; set; }
    public int SellCost { get; set; }
    public int BuyCost { get; set; }
}
