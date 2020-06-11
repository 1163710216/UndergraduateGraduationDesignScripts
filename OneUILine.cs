using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting;
//展示一个进度条
public class OneUILine : StepBase
{
    
    public bool IsOpen = true;
    //是否正在增加进度条，用来控制是否展示进度条
    public bool EnegyHasCome = false;
    //是否还有其它进度条
    public bool IsInGroup = false;
    //用来判断一个进度条是否加载满，在存在多个进度条时使用
    public bool ForGroupHaveDone;

    public List<OneUILine> NextLines = new List<OneUILine>();

    [System.Serializable]
    public class Event_ : UnityEngine.Events.UnityEvent { }

    public Event_ Event_1;


    public float ThisLenth = 1;
    [HideInInspector]
    public UnityEngine.UI.Image thisimg;
    private void Start()
    {
        thisimg = GetComponent<UnityEngine.UI.Image>();
        thisimg.fillAmount = 0;
        if (IsInGroup)
        {
            Event_OnWaitTimeOver.RemoveAllListeners();
        }
    }
    public override void Act()
    {
        base.Act();
        EnegyHasCome = true;
    }
    
    private void Update()
    {
        //慢慢加载进度条
        if (IsOpen && EnegyHasCome)
        {
            thisimg.fillAmount += Time.deltaTime / ThisLenth / 2;
        }
        //进度条加载满
        if (thisimg.fillAmount >= 1 && EnegyHasCome)
        {
            Event_1?.Invoke();
            foreach (var item in NextLines)
            {
                if(item)
                item.EnegyHasCome = true;
            }
            if (!IsInGroup) 
            {
                thisimg.fillAmount = 0;
            }
            EnegyHasCome = false;
            ForGroupHaveDone = true;
            StartCoroutine(Alwa());
        }
    }
    
    IEnumerator Alwa() {
        Event_OnStepOver?.Invoke();
        yield return new WaitForSeconds(WaitTime);
        ForGroupHaveDone = false;
        Event_OnWaitTimeOver?.Invoke();
    }
}
