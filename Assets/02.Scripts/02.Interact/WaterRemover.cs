using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterRemover : MonoBehaviour
{
    public GameObject water;

    public void WaterRemove()
    {
        Vector3 depth = water.transform.position;
        depth = new Vector3(17f, -5f, -11.427f);
    }
}
