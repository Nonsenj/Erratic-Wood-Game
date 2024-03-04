using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public bool Haskey = false;
    public bool HasSeed = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q)) Haskey = !Haskey;
        if (Input.GetKeyDown(KeyCode.R)) HasSeed = !HasSeed;
        
    }
}
