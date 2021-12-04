using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameSaveRow : MonoBehaviour
{
    public TextMeshProUGUI saveNameText;
    public TextMeshProUGUI timeDateText;

    public void SetText(string saveName, string timeDate)
    {
        saveNameText.text = saveName; 
        timeDateText.text = timeDate;
    }
}
