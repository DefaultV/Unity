using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemData : MonoBehaviour,IBeginDragHandler, IDragHandler, IEndDragHandler,IPointerDownHandler,IPointerEnterHandler,IPointerExitHandler {

    public Item item;
    public int amount;
    public int slot;
    public GameObject weaponslot;

    private GameObject weaponToLoad;
    private Inventory inv;
    private Tooltip tooltip;
    private Vector2 offset;

    void Start()
    {
        inv = GameObject.Find("Database").GetComponent<Inventory>();
        weaponslot = GameObject.Find("WeaponSlot");
        tooltip = inv.GetComponent<Tooltip>();
    }

    void Update()
    {

    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (item != null)
        {
            this.transform.SetParent(this.transform.parent.parent);
            this.transform.position = eventData.position - offset;
            GetComponent<CanvasGroup>().blocksRaycasts = false;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (item != null)
        {
            this.transform.position = eventData.position - offset;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        throw new NotImplementedException();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (item != null)
        {
            if (weaponslot.GetComponent<WpSlot>().weaponHeld != null)
            {
                Destroy(weaponslot.GetComponent<WpSlot>().weaponHeld);
            }
            weaponToLoad = Instantiate(Resources.Load<GameObject>("Weapons/" + item.Slug));
            weaponslot.GetComponent<WpSlot>().weaponHeld = weaponToLoad;
            weaponToLoad.transform.SetParent(weaponslot.transform);
            weaponToLoad.transform.localPosition = Vector3.zero;
            weaponToLoad.transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("ENTER");
        tooltip.Activate(item);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        tooltip.Deactivate();
    }
}
