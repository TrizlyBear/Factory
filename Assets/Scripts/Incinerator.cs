using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Incinerator : Building
{
    public List<string> incinerationTags = new List<string>();

    private void OnTriggerEnter(Collider other)
    {
        foreach (string tag in incinerationTags)
        {
            if (other.gameObject.CompareTag(tag))
            {
                Destroy(other.gameObject);
            }
        }
    }
}
