using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class Abutment : MonoBehaviour
{
    public RectTransform rectTranform;
    public Button baseButton;
    public Canvas grid;
    
    private bool ifOutBound = true;
    private List<Button> solarButtons;
    private float buttonHeight = 15;
    
    public void Start()
    {
        Tween t = rectTranform.DOLocalMove(new Vector2(320,-50 + buttonHeight), 0.1f);
        Tween scale = rectTranform.DOScaleY(2, 0.1f);
        
        t.SetAutoKill(false).Pause();
        scale.SetAutoKill(false).Pause();
    }

    public void Update()
    {
        
    }

    public void OnClick()
    {
        if (ifOutBound)
        {
            rectTranform.DOPlayForward();
            ifOutBound = false;

            Button t1 = Instantiate(this.baseButton, transform);
            //Button t2 = Instantiate(this.baseButton, transform);
            t1.gameObject.SetActive(true);
            //t2.gameObject.SetActive(true);
            t1.transform.localScale = new Vector3(1,0.5f, 1);
            t1.transform.localPosition = new Vector3(0, 0, 0);
        }
        else
        {
            rectTranform.DOPlayBackwards();
            ifOutBound = true;
        }
    }
}
