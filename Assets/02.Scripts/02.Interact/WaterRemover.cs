using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TestTools;

public class WaterRemover : MonoBehaviour
{
    public void MoveWater(GameObject water)
    {
        //float x = water.transform.localPosition.x;
        //float y = water.transform.localPosition.y;
        //float z = water.transform.localPosition.z;

        //water.transform.localPosition = new Vector3(x, -5, z);

        StartCoroutine(MoveRackCase(water, -5));
        
    }
    private IEnumerator MoveRackCase(GameObject water, float offsetY)
    {
        float elapsedTime = 0f;
        float duration = 1f;

        Vector3 initialPosition = water.transform.localPosition;
        Vector3 targetPosition = initialPosition + new Vector3(0f, offsetY, 0f);

        while (elapsedTime < duration)
        {
            water.transform.localPosition = Vector3.Lerp(initialPosition, targetPosition, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        water.transform.localPosition = targetPosition;

        Destroy(water);
    }
}
