using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    [SerializeField]
    private GameObject _wellLeft;

    [SerializeField]
    private GameObject _wellBottom;

    public void SetActiveWallBottom(bool value)
    {
        _wellBottom.SetActive(value);
    }

    public void SetActiveWallLeft(bool value)
    {
        _wellLeft.SetActive(value);
    }
}
