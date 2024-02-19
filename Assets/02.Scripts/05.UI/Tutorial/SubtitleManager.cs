using System.Collections;
using UnityEngine;

public class SubtitleManager : MonoBehaviour, IInteractable_HT
{
    [SerializeField]
    [TextArea]    
    private string Subtitle;
    public TMPro.TextMeshProUGUI SubtitleTMPro;

    private void Start()
    {
        SubtitleTMPro.gameObject.SetActive(false);
    }
    public void OnInteract()
    {
        StartCoroutine(ShowSubtitle());
    }

    private IEnumerator ShowSubtitle()
    {
        SubtitleTMPro.gameObject.SetActive(true);
        SubtitleTMPro.text = string.Format(Subtitle);
        yield return new WaitForSeconds(2f);
        SubtitleTMPro.gameObject.SetActive(false);
    }
}
