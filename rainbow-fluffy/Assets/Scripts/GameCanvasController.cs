using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class GameCanvasController : MonoBehaviour
{
    [SerializeField]
    private Text _text;

    [SerializeField]
    private Button _button;

    public IObservable<Unit> OnClickStartButon => _button.onClick.AsObservable();

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetButton(bool isActive, string text)
    {
        _button.gameObject.SetActive(isActive);
        _button.GetComponentInChildren<Text>().text = text;
    }

    public void SetText(string text)
    {
        _text.text = text;
    }
}
