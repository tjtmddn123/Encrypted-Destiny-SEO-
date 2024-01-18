using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterRemover : MonoBehaviour
{
    public void WaterDown(GameObject Water)
    {
        Destroy(Water);
    }
}
