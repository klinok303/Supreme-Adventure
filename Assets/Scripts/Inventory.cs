using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    [SerializeField] KeyCode openKey;

    [SerializeField] Image[] slotsImage;
    [SerializeField] TMP_Text[] slotsText;

    [SerializeField] List<Slot> inventorySlots = new List<Slot>();

    [SerializeField] SimpleAnimationsManager simpleAnimationsManager;

    bool isOpened = true;

    byte pos = 1;

    ReloadState rs;

    void Start()
    {
        rs = GetComponent<ReloadState>();

        ShowItems();
    }

    void Awake()
    {
        foreach (var slot in inventorySlots)
        {
            if (slot.item != null) slot.isFull = true;
        }
    }

    void Update()
    {
        if (Input.GetAxisRaw("Mouse ScrollWheel") != 0 && (rs.GetStateID() != 1 || !Input.GetMouseButton(0)) && isOpened)
        {
            pos -= (byte)(Input.GetAxisRaw("Mouse ScrollWheel") * 10);

            if (pos < 1) pos = 1;
            if (pos > 4) pos = 4;

            IsFullUpdate();
            ShowItems();
        }

        if (Input.GetKeyDown(openKey)) Open();
    }

    void ShowItems()
    {
        for (int i = -1; i < 2; i++)
        {
            if (inventorySlots[pos-i].isFull) 
            {
                slotsImage[1-i].sprite = inventorySlots[pos-i].item.itemSprite;
                slotsImage[1-i].gameObject.SetActive(true);
            }
            else slotsImage[1-i].gameObject.SetActive(false);

            if (inventorySlots[pos-i].amount > 0) 
            {
                slotsText[1-i].text = inventorySlots[pos-i].amount.ToString();
                slotsText[1-i].gameObject.SetActive(true);
            }
            else slotsText[1-i].gameObject.SetActive(false);
        }
    }

    void IsFullUpdate()
    {
        foreach (var slot in inventorySlots)
        {
            if (slot.amount == 0) slot.isFull = false;
        }
    }

    public void AddItem(Item item, byte amount = 1)
    {
        foreach (var slot in inventorySlots)
        {
            if (!slot.isFull)
            {
                slot.item = item;
                slot.amount = amount;
                break;
            }
            else if (slot.item == item && slot.item.maxAmount > slot.amount)
            {
                slot.amount += amount;
                break;
            }
        }
    }

    public void RemoveItem(Item item, byte amount = 1)
    {
        foreach (var slot in inventorySlots)
        {
            if (slot.item == item)
            {
                slot.amount += amount;
                break;
            }
        }
    }

    public void Open()
    {
        isOpened = !isOpened;

        if (isOpened) simpleAnimationsManager.Play(1);
        else simpleAnimationsManager.Play(0);
    }
}
