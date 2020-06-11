using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Step_Move : StepBase
{
    //指定空物体作为路标
    public Transform[] Roads;
    //需要移动的物体的模板
    public GameObject WillInstantiate;
    //起始位置处放置的空物体
    public Transform StartPos;
    //移动的物体
    GameObject inster;
    Transform[] gameObjects;
    public override void Act()
    {
        base.Act();
        //在起始位置处产生移动的物体
        inster = Instantiate(WillInstantiate, StartPos.position, StartPos.rotation);
        //获得inster所有孩子的坐标
        gameObjects = inster.GetComponentsInChildren<Transform>();
        foreach (var item in gameObjects)
        {
            if (item == inster.transform) continue;
            StartCoroutine(Step_MoveIenu(item));
        }
    }//向路标靠近，实现移动
    private IEnumerator Step_MoveIenu(Transform Who) {
     
        foreach (var item in Roads)
        {
            while (Vector3.Distance(Who.transform.position,item.position) > 0.2f)
            {
                Who.transform.position += (item.position - Who.transform.position).normalized * Time.deltaTime * 2;
                yield return null;
            }
        }
        StopAllCoroutines();
        StartCoroutine(GoAway());
    }
    
    //物体到达目的地后四散消失的特效
    private IEnumerator GoAway() {
        List<Vector3> itm_Yie = new List<Vector3>();
        for (int j = 0; j < gameObjects.Length; j++)
        {
            yield return null;
            itm_Yie.Add(gameObjects[j].transform.position + new Vector3(Random.Range(-0.2f, 0.2f), 0, Random.Range(-0.3f, 0.2f)));
        }
        for (float i = 0; i < 0.5f; i+= 0.005f)
        {
            for (int j = 0; j < gameObjects.Length; j++)
            {
                if (gameObjects[j] == inster.transform) continue;
                gameObjects[j].transform.position = Vector3.Lerp(gameObjects[j].transform.position, itm_Yie[j],i);
                gameObjects[j].localScale = (0.1f - 0.2f * i) * Vector3.one;
            } 
            yield return null;
        }
        Destroy(inster);
        Event_OnStepOver?.Invoke();
        yield return new WaitForSeconds(WaitTime);
        Event_OnWaitTimeOver?.Invoke();
    }
}
