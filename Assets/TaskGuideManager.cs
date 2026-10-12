using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;


public class TaskGuideManager : MonoBehaviour
{
    
    private static TaskGuideManager _instance;
    public static TaskGuideManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<TaskGuideManager>();
                if (_instance == null)
                {
                    GameObject go = new GameObject("TaskGuideManager");
                    _instance = go.AddComponent<TaskGuideManager>();
                }
            }
            return _instance;
        }
    }

    
    [System.Serializable]
    public class TaskData
    {
        [Tooltip("任务名字")]
        public string taskName = "任务名";

        [Tooltip("任务内容 / 指引描述")]
        [TextArea(3, 10)]
        public string taskContent = "任务内容...";
    }

    
    [Header("任务列表（按顺序填写）")]
    public List<TaskData> tasks = new List<TaskData>();

    [Header("UI 引用")]
    [Tooltip("显示任务名字的 Text")]
    public Text taskNameText;

    [Tooltip("显示任务内容的 Text")]
    public Text taskContentText;

 

    
    private int _currentIndex = -1;

   
    void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }
        _instance = this;
    }

    void Start()
    {
        if (tasks.Count > 0)
            ShowTask(0);
    }

  
    public void CompleteTask()
    {
        if (tasks.Count == 0) return;

        int next = _currentIndex + 1;

   

        ShowTask(next);
    }

    
    public void ShowTask(int index)
    {
        if (index < 0 || index >= tasks.Count)
        {
            Debug.LogError($"任务索引 {index} 超出范围 (0-{tasks.Count - 1})");
            return;
        }

        _currentIndex = index;
        TaskData data = tasks[index];

        if (taskNameText != null) taskNameText.text = data.taskName;
        if (taskContentText != null) taskContentText.text = data.taskContent;
    }

    public int GetCurrentIndex() => _currentIndex;

    
    public TaskData GetCurrentTask()
    {
        if (_currentIndex >= 0 && _currentIndex < tasks.Count)
            return tasks[_currentIndex];
        return null;
    }

   
}