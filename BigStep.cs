using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class BigStep : MonoBehaviour
{
    public StepBase[] stepBases;

    public string[] stepStrs;

    public StepBase.Events_ Event_OnStart = new StepBase.Events_();
    public StepBase.Events_ Event_OnStepOver = new StepBase.Events_();
}
