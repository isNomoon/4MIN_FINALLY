using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class TaskBase : MonoBehaviour
{
    // Start is called before the first frame update
    

    

    /// <summary>管理器进入本任务时调用</summary>
    public void BeginTask()
    {
        OnBegin();
    }

    /// <summary>管理器离开本任务时调用</summary>
    public void EndTask()
    {
        OnEnd();
    }

    /// <summary>子类满足完成条件时调用，通知管理器进入下一个</summary>
    

    // ---- 子类按需重写 ----
    protected virtual void OnBegin() { this.gameObject.SetActive(true); }
    protected virtual void OnEnd() { this.gameObject.SetActive(false); }
}

