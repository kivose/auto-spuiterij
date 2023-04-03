using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class TutorialManager : MonoBehaviour
{
    public PageInfo[] pages;

    [System.Serializable]
    public struct PageInfo
    {
        public string title;
        [TextArea(5,9)]
        public string Description;
        public float textSize;
    }

    public TextMeshProUGUI title, desc;

    int index;

    public void Scroll(int toAdd)
    {
        index += toAdd;

        if (index < 0) index = 0;
        else if (index > pages.Length - 1) index = pages.Length - 1;

        title.text = pages[index].title;
        desc.text = pages[index].Description;
        desc.fontSize = pages[index].textSize;
    }
}
