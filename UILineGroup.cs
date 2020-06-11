using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UILineGroup : StepBase
{
    //路标
    public GameObject[] OneUILineGroups;
    //几个进度条共同形成一条传送路径
    private OneUILine[] UIlineGroups;
    private void Start()
    {
        int i = 0;
        UIlineGroups = new OneUILine[OneUILineGroups.Length];
        foreach (var item in OneUILineGroups)
        {
            UIlineGroups[i] = item.AddComponent<OneUILine>();
            UIlineGroups[i].EnegyHasCome = false;
            UIlineGroups[i].IsOpen = true;
            UIlineGroups[i].IsInGroup = true;
            i++;
        }
    }
    public override void Act()
    {
        base.Act();
        StartCoroutine(UILineGo());
    }
    IEnumerator UILineGo() {

        for (int i = 0; i < UIlineGroups.Length; i++)
        {
            UIlineGroups[i].EnegyHasCome = true;
            while (!UIlineGroups[i].ForGroupHaveDone)
            {
                yield return null;
            }
        }
        foreach (var item in UIlineGroups)
        {
            item.EnegyHasCome = false;
            item.thisimg.fillAmount = 0;
            Destroy(item);
        }
        Event_OnStepOver?.Invoke();
        yield return new WaitForSeconds(WaitTime);
        Event_OnWaitTimeOver?.Invoke();
    }
}
