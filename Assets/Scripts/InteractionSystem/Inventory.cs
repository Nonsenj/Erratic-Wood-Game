using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public bool Haskey = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q)) Haskey = !Haskey;
        
    }
}
