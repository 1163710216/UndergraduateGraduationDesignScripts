using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//管理动态的动作
public class GameManager2 : MonoBehaviour
{
   
    //保存所有动作，在Unity中创建了37个空物体，它们挂上BigStep.cs，将它们赋给bigSteps
    public BigStep[] bigSteps;
    //当前执行到第几动作
    private int NowStep = -1;
    //当前执行到第几大动作
    private int NowBigStep = 0;
    //屏幕上展示的文字
    public Text Text_;
    //是否可以开始执行下一步动作
    public bool BolCanNextStep = false;

   private void Start()
   {
       BolCanNextStep = true;
   }

   //在Event_OnWaitTimeOver 发生时被调用
    public void CanNextStep() 
    {
        BolCanNextStep = true;
        if (NowStep == bigSteps[NowBigStep].stepBases.Length - 1)
        {
            bigSteps[NowBigStep].Event_OnStepOver.Invoke();
        }
    }

    //扳动手柄扳机时被调用
    public void Click() {
        if (BolCanNextStep)
        {  // && Input.GetKeyDown(KeyCode.N)
            NextStep();
            BolCanNextStep = false;
        }
    }
    private void Update()
    {
       
    }

    //被Click()调用
    public void NextStep()
    {
        NowStep++;
        //小步执行完后，大步骤++
        if (NowStep == bigSteps[NowBigStep].stepBases.Length)
        {
            NowBigStep++;
            NowStep = 0;
        }
        //将此时对应的text展示在屏幕上
        if (bigSteps[NowBigStep].stepStrs[NowStep] != "")
        {
            Debug.Log(bigSteps[NowBigStep].stepStrs[NowStep]);
            Text_.text = bigSteps[NowBigStep].stepStrs[NowStep];
        }
        //全部执行完毕
        if (NowStep == bigSteps[NowBigStep].stepBases.Length && NowBigStep == bigSteps.Length)
        {
            return;
        }

        if (NowStep == 0) 
        {
            bigSteps[NowBigStep].Event_OnStart.Invoke();
        }
       //执行每一小不走具体的动作
        bigSteps[NowBigStep].stepBases[NowStep].Act();
    }
}
