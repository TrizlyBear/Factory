using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

[ExecuteAlways]
public class OptionCycle : MonoBehaviour
{
    public string Value { get { return options[currentIndex]; } }

    public bool interactable = true;

    [Space]

    public TextMeshProUGUI optionText;

    public List<string> options = new List<string>();

    public UnityEvent<int> onValueChanged = new UnityEvent<int>();

    private int currentIndex = 0;

    private void Update()
    {
        optionText.text = Value;
    }

    public void NextOption()
    {
        if (currentIndex == options.Count - 1)
            currentIndex = 0;

        else
            currentIndex++;

        onValueChanged.Invoke(currentIndex);
    }

    public void PreviousOption()
    {
        if (currentIndex == 0)
            currentIndex = options.Count - 1;

        else
            currentIndex--;

        onValueChanged.Invoke(currentIndex);
    }

    public void SetOption(int index)
    {
        if (index >= 0 && index < options.Count)
        {
            currentIndex = index;
            onValueChanged.Invoke(currentIndex);
        }
    }
}
