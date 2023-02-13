using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class HUDManager : MonoBehaviour
{
    public PageSwitchAnimation switchAnimation;
    public GameObject[] mainMenuPages;

    private void Start()
    {
        OnMainMenuButtonClicked(0);
    }
    public async void OnMainMenuButtonClicked(int index)
    {
        switchAnimation.Enable();

        for (int i = 0; i < mainMenuPages.Length; i++)
        {
            mainMenuPages[i].SetActive(false);  
        }

        await Task.WhenAny(WaitForSwitchOver());

        mainMenuPages[index].SetActive(true);
    }
    public async Task WaitForSwitchOver() 
    {
        bool completed = false;
        while (completed == false)
        {
            completed = !switchAnimation.Active;
            await Task.Yield();
        }
    }
}
