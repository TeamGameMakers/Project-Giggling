using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WwiseButton : MonoBehaviour
{
    public void DIA_A()
    {
        AkSoundEngine.PostEvent("DIA_A", gameObject);
    }
    public void DIA_B()
    {
        AkSoundEngine.PostEvent("DIA_B", gameObject);
    }
    public void Interact()
    {
        AkSoundEngine.PostEvent("Interact", gameObject);
    }
    public void Menu_confirm()
    {
        AkSoundEngine.PostEvent("Menu_confirm", gameObject);
    }
    public void Menu_enter()
    {
        AkSoundEngine.PostEvent("Menu_enter", gameObject);
    }
    public void Menu_exit()
    {
        AkSoundEngine.PostEvent("Menu_exit", gameObject);
    }
    public void Menu_negative()
    {
        AkSoundEngine.PostEvent("Menu_negative", gameObject);
    }
    public void Menu_positive()
    {
        AkSoundEngine.PostEvent("Menu_positive", gameObject);
    }
    public void Menu_switch()
    {
        AkSoundEngine.PostEvent("Menu_switch", gameObject);
    }
    public void Pickup()
    {
        AkSoundEngine.PostEvent("Pickup", gameObject);
    }

}
