using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDManager : MonoBehaviour
{
    public PageSwitchAnimation switchAnimation;
    public GameObject[] mainMenuPages;

    public void OnMainMenuButtonClicked(int index)
    {
        switchAnimation.Enable();

        for (int i = 0; i < mainMenuPages.Length; i++)
        {
            if (i == index)
                mainMenuPages[i].SetActive(true);
            else
                mainMenuPages[i].SetActive(false);
        }
    }
}
