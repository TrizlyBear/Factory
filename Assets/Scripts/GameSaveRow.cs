using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameSaveRow : MonoBehaviour
{
    public bool selected = false;

    public GameObject selectionIndicator;
    public TextMeshProUGUI saveNameText;
    public TextMeshProUGUI timeDateText;

    private void Update()
    {
        selectionIndicator.SetActive(selected);
    }

    public void SetText(string saveName, string timeDate)
    {
        saveNameText.text = saveName; 
        timeDateText.text = timeDate;
    }

    public void Select()
    {
        MainMenuManager.Instance.SelectSave(this);
    }
}
