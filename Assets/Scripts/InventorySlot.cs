using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[ExecuteAlways]
public class InventorySlot : MonoBehaviour
{
    public bool Occupied { get; private set; }

    public Item item;
    public int itemCount = 0;

    [Space]

    public Image itemImage;
    public TextMeshProUGUI itemCountText;

    private void Update()
    {
        Occupied = item != null;

        if (itemCount <= 0)
        {
            item = null;
            itemCount = 0;
        }

        if (Occupied)
        {
            itemCountText.text = itemCount.ToString();

            itemImage.sprite = item.itemImage;
            itemImage.color = Color.white;
        }
        else
        {
            itemImage.sprite = null;
            itemImage.color = new Color(0, 0, 0, 0);
        }

        itemCountText.gameObject.SetActive(Occupied);
    }
}
