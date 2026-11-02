using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrontTask : TaskBase
{
    // Start is called before the first frame update
    public List<GameObject> gameObject1;
    public List<GameObject> gameObject2;
    protected override void OnBegin()
    {
        base.OnBegin();

    }
    protected override void OnEnd()
    {

        foreach (var task in gameObject1)
        {
            task.gameObject.SetActive(false);
        }
        foreach (var task in gameObject2)
        {
            task.gameObject.SetActive(true);
        }
        base.OnEnd();
        TaskManager.Instance.EnterNext();
    }
    private void Update()
    {
        if(Input.GetMouseButton(0))
        {
            OnEnd();
        }
    }
}
