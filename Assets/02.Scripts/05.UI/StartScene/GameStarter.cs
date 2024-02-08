using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameStarter : MonoBehaviour
{
    public GameObject[] DisactiveObj;

    public GameObject[] ActiveObj;

    public GameObject[] NeedDelayObj;

    public void GameStart()
    {

        Array.ForEach(DisactiveObj, disactiveObj => { disactiveObj.SetActive(false); });

        Array.ForEach(ActiveObj, activeObj => { activeObj.SetActive(true); });

        StartCoroutine(WaitSec());
    }

    private IEnumerator WaitSec()
    {
        yield return new WaitForSeconds(2f);

        Array.ForEach(NeedDelayObj, needDelayObj => { needDelayObj.SetActive(false); });
    }




}
