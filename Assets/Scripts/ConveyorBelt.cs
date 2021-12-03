using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorBelt : MonoBehaviour
{
    public bool transportEnabled = true;

    public Direction transportDirection = Direction.Forward;
    public float transportSpeed = 5f;

    private void OnTriggerStay(Collider other)
    {
        if (!transportEnabled)
            return;

        if (other.GetComponent<Rigidbody>() != null)
        {
            other.transform.Translate(GetDirectionVector() * transportSpeed * Time.deltaTime, Space.World);
        }
    }

    private Vector3 GetDirectionVector()
    {
        switch (transportDirection)
        {
            case Direction.Forward: return transform.forward;
            case Direction.Backward: return -transform.forward;
            case Direction.Right: return transform.right;
            case Direction.Left: return -transform.right;
        }

        return transform.forward;
    }
}
