using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterRemover : MonoBehaviour
{
    public void MoveWater(GameObject water)
    {
        float x = water.transform.localPosition.x;
        float y = water.transform.localPosition.y;
        float z = water.transform.localPosition.z;

        water.transform.localPosition = new Vector3(x, -5, z);
    }
}
