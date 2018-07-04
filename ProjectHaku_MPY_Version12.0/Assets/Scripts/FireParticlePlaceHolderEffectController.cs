using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireParticlePlaceHolderEffectController : MonoBehaviour
{

    public GameObject Effect;
    public GameObject PlaceHolder;
    //public GameObject PlaceHolderSource;
    public Behaviour[] toggleComponents;
    public GameObject[] specialComponents;
    public bool isPreforming = false;

    //Transform PlaceHolderParent;

    void Start()
    {
        //PlaceHolderParent = Effect.transform.parent;
    }

    public void ShowPlaceHolder()
    {
        PlaceHolder.SetActive(true);

    }

    public void Hide()
    {
        Effect.SetActive(false);
        PlaceHolder.SetActive(false);
    }

    public void HideEffect()
    {
        Effect.SetActive(false);
    }

    public void Show()
    {
        Effect.SetActive(true);
    }
}
