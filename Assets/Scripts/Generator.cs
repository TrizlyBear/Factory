using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour, IInteractable
{
    public bool generationEnabled = true;

    [Space]

    public List<GameObject> generatedObjects = new List<GameObject>();

    public Transform generationPoint;

    public float generationRate = 1f;

    private float timePassed = 0f;

    private void Update()
    {
        if (generationEnabled)
            timePassed += Time.deltaTime;

        if (timePassed >= 1f / generationRate)
        {
            SpawnObject();
            timePassed = 0f;
        }
    }

    public void SpawnObject()
    {
        GameObject.Instantiate(GetObject(), generationPoint.position, Quaternion.identity);
    }

    private GameObject GetObject()
    {
        int n = Random.Range(0, generatedObjects.Count);
        return generatedObjects[n];
    }

    public void Interact()
    {
        
    }
}
