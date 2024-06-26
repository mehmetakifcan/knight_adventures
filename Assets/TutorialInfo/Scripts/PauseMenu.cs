using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public void ShowPauseMenu()
    {
        GameController.instance.ShowPauseMenu();
    }
    public void HidePauseMenu()
    {
        GameController.instance.HidePauseMenu();
    }
}
