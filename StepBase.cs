using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StepBase : MonoBehaviour
{
    [System.Serializable]
    public class Events_ : UnityEngine.Events.UnityEvent { }
    public GameObject Who;
    public float WaitTime = 1f;
    //每一小步开始执行前，执行的动作
    public Events_ Event_OnStart = new Events_();
    //等待结束后，执行的动作
    public Events_ Event_OnWaitTimeOver = new Events_();
    //每一小步执行完毕后，执行的动作
    public Events_ Event_OnStepOver = new Events_();
    //Unity可重写函数，当脚本实例被加载时会调用Awake函数；
    //Awake函数在所有的游戏对象被初始化完毕之后才会被调用；在脚本实例的整个生命周期中，Awake函数仅执行一次。
    private void Awake()
    {
       Event_OnWaitTimeOver.AddListener(() => { GameObject.Find("GameManager").GetComponent<GameManager2>().CanNextStep(); });
    }
    //在GameManager2的NextStep()中被调用，可以被子类重写和使用
    public virtual void Act()
    {
        Event_OnStart?.Invoke();
    }
}
