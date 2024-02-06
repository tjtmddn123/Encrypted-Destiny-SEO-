using System.Collections;
using UnityEngine;
using Cinemachine;

public class ChangeCinemachine : MonoBehaviour
{
    public CinemachineVirtualCamera playerCameras;
    public CinemachineVirtualCamera changedCamera;
    public InteractManager_HT interact;
    [SerializeField]
    private float ChangingTime = 4f;

    public void CinemachineTest()
    {
        StartCoroutine(ChangeCamera());
        Debug.Log("카메라 바뀜");
    }

    private IEnumerator ChangeCamera()
    {
        playerCameras.gameObject.SetActive(false);
        changedCamera.gameObject.SetActive(true);
        interact.CameraChaging = true;
        interact.promptText.gameObject.SetActive(false);
        yield return new WaitForSeconds(ChangingTime);
        playerCameras.gameObject.SetActive(true);
        changedCamera.gameObject.SetActive(false);
        interact.CameraChaging = false;
        interact.promptText.gameObject.SetActive(true);
    }
}
