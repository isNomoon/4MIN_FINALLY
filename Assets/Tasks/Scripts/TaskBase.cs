using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class TaskBase : MonoBehaviour
{
    // Start is called before the first frame update
    

    [Header("完成后额外动作（可选）")]
    public UnityEvent onTaskCompleted;

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
    protected void CompleteTask()
    {
        onTaskCompleted?.Invoke();
        TaskManager.Instance?.NotifyTaskCompleted(this);
    }

    // ---- 子类按需重写 ----
    protected virtual void OnBegin() { }
    protected virtual void OnEnd() { }
}

