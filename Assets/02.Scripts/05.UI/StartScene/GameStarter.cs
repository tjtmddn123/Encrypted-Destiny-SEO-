using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStarter : MonoBehaviour
{
    public GameObject startCamera;
    public GameObject startBtn;

    public GameObject playerCamera;
    public GameObject player;

    public GameObject Dummy;
    public GameObject StatueLight;

    public void GameStart()
    {
        startCamera.SetActive(false);        
        startBtn.SetActive(false);
        player.SetActive(true);
        playerCamera.SetActive(true);
        StartCoroutine(WaitSec());
    }

    private IEnumerator WaitSec()
    {
        yield return new WaitForSeconds(2f);
        Dummy.SetActive(false);
        StatueLight.SetActive(false);
    }


       
  
}
