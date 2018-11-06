using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tooltip : MonoBehaviour {
    private Item item;
    private string data;
    private GameObject tooltip;
    private GameObject uitext;


	// Use this for initialization
	void Start () {
        tooltip = GameObject.Find("Tooltip");
        uitext = GameObject.Find("toolTEXT");
        tooltip.SetActive(false);
	}
	
    public void Activate(Item m_item)
    {
        item = m_item;
        ConstructDataString();
        tooltip.SetActive(true);
        
    }

    public void Deactivate()
    {
        tooltip.SetActive(false);
    }

    public void ConstructDataString()
    {
        if (item.Strength == 0)
            data = "<color=#0473f0><b>" + item.Title + "</b></color>\n\nAttack: " + item.MinAttack + "-" + item.MaxAttack + "\n+ " + item.Intellect + " Intellect\n" + item.Description + "";
        if (item.Intellect == 0)
            data = "<color=#0473f0><b>" + item.Title + "</b></color>\n\nAttack: " + item.MinAttack + "-" + item.MaxAttack + "\n+ " + item.Strength + " Strength\n" + item.Description + "";
        else if (item.Strength > 0 && item.Intellect > 0)
            data = "<color=#0473f0><b>" + item.Title + "</b></color>\n\nAttack: " + item.MinAttack + "-" + item.MaxAttack +"\n+ " + item.Strength + " Strength\n" + "+ " + item.Intellect + " Intellect" + item.Description + "";
        uitext.GetComponent<Text>().text = data;
    }
	// Update is called once per frame
	void Update () {
		if (tooltip.activeSelf)
        {
            tooltip.transform.position = Input.mousePosition;
        }
	}
}
