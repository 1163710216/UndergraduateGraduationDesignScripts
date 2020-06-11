using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using VRTK;
public class PlayerHeight : MonoBehaviour
{
   // public SteamVR_Action_Boolean teleport;

  //  public SteamVR_Action_Boolean teleport2;
    // Start is called before the first frame update
    void Start()
    {
        //当Grip被按下时执行TrunUpRight函数
        transform.Find("RightController").GetComponent<VRTK_ControllerEvents>().GripTouchStart
            += new VRTK.ControllerInteractionEventHandler(TrunUpRight);
        transform.Find("RightController").GetComponent<VRTK_ControllerEvents>().GripTouchEnd
         += new VRTK.ControllerInteractionEventHandler(TrunDownRight);

        transform.Find("LeftController").GetComponent<VRTK_ControllerEvents>().GripTouchStart
         += new VRTK.ControllerInteractionEventHandler(TrunUpLeft);
        transform.Find("LeftController").GetComponent<VRTK_ControllerEvents>().GripTouchEnd
         += new VRTK.ControllerInteractionEventHandler(TrunDownLeft);



        transform.Find("RightController").GetComponent<VRTK_ControllerEvents>().TriggerTouchStart
       += new VRTK.ControllerInteractionEventHandler(NextStep);
    }

    public GameManager2 GameManager2;
    void NextStep(object sender, VRTK.ControllerInteractionEventArgs e) 
    {
        GameManager2.Click();
    }


    void TrunUpRight(object sender, VRTK.ControllerInteractionEventArgs e)
    {
        Debug.Log(111);
        RightHit = true;
    }

    void TrunDownRight(object sender, VRTK.ControllerInteractionEventArgs e)
    {
        Debug.Log(111);
        RightHit = false;
    }

    void TrunUpLeft(object sender, VRTK.ControllerInteractionEventArgs e)
    {
        Debug.Log(111);
        LeftHit = true;
    }

    void TrunDownLeft(object sender, VRTK.ControllerInteractionEventArgs e)
    {
        Debug.Log(111);
        LeftHit = false;
    }
    private bool LeftHit = false;
    private bool RightHit = false;

    public Transform Playe;
    void Update()
    {
          if (RightHit)
          {
             Playe.transform.position += new Vector3(0, 0.02f, 0);
          }
          if (LeftHit)
          {
             Playe.transform.position -= new Vector3(0, 0.02f, 0);
          }
          
    }
}
