using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static TaskManager Instance {  get; private set; }
    [SerializeField] private List<TaskBase> tasks = new List<TaskBase>();
    [SerializeField] private UnityEngine.Events.UnityEvent onAllTasksCompleted;
    public TaskBase CurrentTask { get; private set; }
    public int CurrentIndex { get; private set; } = -1;
    private void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
    }

    /// <summary>开始整条任务链</summary>
    public void StartTutorial()
    {
        foreach (var t in tasks)
            if (t != null) t.gameObject.SetActive(false);

        CurrentIndex = -1;
        EnterNext();
    }

    /// <summary>任务基类完成后调用，进入下一个</summary>
    public void NotifyTaskCompleted(TaskBase task)
    {
        if (task != CurrentTask) return; // 防止旧任务误触发
        EnterNext();
    }

    private void EnterNext()
    {
        if (CurrentTask != null) CurrentTask.EndTask();

        CurrentIndex++;
        if (CurrentIndex >= tasks.Count)
        {
            CurrentTask = null;
            onAllTasksCompleted?.Invoke();
            return;
        }

        CurrentTask = tasks[CurrentIndex];
        CurrentTask.BeginTask();
    }

}
