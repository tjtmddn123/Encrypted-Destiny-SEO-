//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class LightAndText : MonoBehaviour
//{
//    public Light detectionLight; 
//    public TextMesh hiddenText; 

//    // Start is called before the first frame update
//    void Start()
//    {
        
//    }

//    // Update is called once per frame
//    public void Update()
//    {
//        if (paper.isHoldingPaper && IsInDetectionLight())
//        {
            
//            hiddenText.gameObject.SetActive(true);
//        }
//        else
//        {
            
//            hiddenText.gameObject.SetActive(false);
//        }
//    }

//    public bool IsInDetectionLight()
//    {
      
//        RaycastHit hit;
//        if (Physics.Raycast(detectionLight.transform.position, detectionLight.transform.forward, out hit))
//        {
//            if (hit.collider.gameObject == PaperInteraction.heldPaper)
//            {
//                return true;
//            }
//        }
//        return false;
//    }
//}
