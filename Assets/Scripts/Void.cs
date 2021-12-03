using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Void : MonoBehaviour
{
    public List<string> destroyTags = new List<string>();

    private void OnCollisionEnter(Collision collision)
    {
        GameObject obj = collision.gameObject;

        foreach (string tag in destroyTags)
        {
            if (obj.CompareTag(tag))
            {
                Destroy(obj);
            }
        }
    }
}
