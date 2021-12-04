using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingHologram : MonoBehaviour
{
    public Vector3 center = Vector3.zero;
    public Vector3 halfSize = Vector3.one;

    private bool _isColliding = false;
    public bool IsColliding { get { return _isColliding; } }

    private void Update()
    {
        _isColliding = Physics.CheckBox(transform.position + center, halfSize);
    }
}
