using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "objectCreator", order = 1)]
public class inventorySystem : ScriptableObject
{
    public string name;
    public string description;
    public int price;
    public int ID;
    public GameObject model;
}
