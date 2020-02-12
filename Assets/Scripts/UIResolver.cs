using UnityEngine;

public class UIResolver : MonoBehaviour
{
    private void Start()
    {
        FindObjectOfType<LangResolver>().ResolveTexts();
    }
}