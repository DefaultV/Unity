using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour {

    GameObject inventoryPanel;
    GameObject slotPanel;
    ItemDatabase database;
    public GameObject inventorySlot;
    public GameObject inventoryItem;

    int slotAmount;
    public List<Item> items = new List<Item>();
    public List<GameObject> slots = new List<GameObject>();

    void Start () {
        database = GetComponent<ItemDatabase>();
        slotAmount = 20;
        inventoryPanel = GameObject.Find("Inventory Panel");
        slotPanel = inventoryPanel.transform.FindChild("Slot Panel").gameObject;
        for (int i = 0; i<slotAmount; i++)
        {
            items.Add(new Item());
            slots.Add(Instantiate(inventorySlot));
            slots[i].transform.SetParent(slotPanel.transform,false);
        }
        AddGold(10);
        AddItem(1);
        AddItem(2);
        inventoryPanel.GetComponent<Canvas>().enabled = false;
	}
	
    public void AddItem(int id)
    {
        Item itemToAdd = database.FetchItemByID(id);
        for (int i = 0; i<items.Count; i++)
        {
            if (items[i].ID == -1)
            {
                items[i] = itemToAdd;
                GameObject itemObj = Instantiate(inventoryItem);
                itemObj.GetComponent<ItemData>().item = itemToAdd;
                itemObj.transform.SetParent(slots[i].transform,false);
                itemObj.transform.localPosition = Vector2.zero;
                Debug.Log("With position" + itemObj.transform.position);
                itemObj.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Items/" + itemToAdd.Slug);
                itemObj.name = itemToAdd.Title;
                itemObj.tag = "Item";
                break;
            }
        }
    }

    public void AddGold(int amount)
    {
        Text goldAmount = GameObject.Find("Gold Panel").transform.FindChild("Amount").gameObject.GetComponent<Text>();
        goldAmount.text += amount;
    }
}
