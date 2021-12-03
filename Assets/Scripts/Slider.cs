using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

[ExecuteAlways]
public class Slider : MonoBehaviour
{
    public UnityEngine.UI.Slider slider;

    public TMP_InputField valueInput;

    public UnityEvent<float> onValueChanged = new UnityEvent<float>();

    private bool isEditing = false;

    private void Update()
    {
        if (!isEditing)
            valueInput.text = (Mathf.Round(slider.value * 100f) / 100f).ToString();
    }

    public void InvokeValueChanged(float val)
    {
        onValueChanged.Invoke(val);
    }

    public void SetValue(string val)
    {
        if (float.TryParse(val, out float value))
        {
            slider.value = value;
        }
    }

    public void SetEditing(bool editing)
    {
        isEditing = editing;
    }
}
