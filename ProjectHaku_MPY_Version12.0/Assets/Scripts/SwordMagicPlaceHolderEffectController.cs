using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordMagicPlaceHolderEffectController : MonoBehaviour {

    public ShieldMagicPlaceHolderEffectController shieldMagicPlaceHolderEffectController;
    public GameObject PlaceHolder;
    public GameObject AnotherSword;
    // Disable LeftModel for now version
    public GameObject LeftModel;
    public GameObject Hand;

    public void Hide()
    {
        PlaceHolder.SetActive(false);
        AnotherSword.SetActive(false);
        // Disable LeftModel for now version
        //LeftModel.SetActive(true);
    }

    public void Show()
    {
        if (!shieldMagicPlaceHolderEffectController.isNotEnd())
        {
            //LeftModel.SetActive(false);
            AnotherSword.SetActive(true);
            PlaceHolder.SetActive(false);
            Hand.SetActive(false);
        }
        else
        {
            PlaceHolder.SetActive(true);
            //LeftModel.SetActive(true);
        }
    }
}
