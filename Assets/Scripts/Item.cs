using UnityEngine;

public enum ItemType 
{
    None = 0,
    Buyable = 1,
    Saleable = 2,
    Resalable = 3
};

public enum WeaponType 
{
    None = 0,
    ForClimbing = 1,
    Mealie = 2,
    Firearms = 3
};

[CreateAssetMenu(fileName = "NewItem", menuName = "SupremeAPI/Item")]
public class Item : ScriptableObject
{
    public string name = "NewItem";
    public string description = "";

    public byte maxAmount = 1;

    public float price = 0;

    public Sprite itemSprite;

    public ItemType itemType;
    public WeaponType weaponType;
}
