using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

/// <summary>
/// PPT + 对话 统一管理器（单例模式）
/// 每一页包含：图片 + 名字 + 对话内容，统一书写，一一对应
/// </summary>
public class PPTSlide : MonoBehaviour
{
    // ---------- 单例 ----------
    
    private static PPTSlide _instance;
    public static PPTSlide Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<PPTSlide>();
                if (_instance == null)
                {
                    GameObject go = new GameObject("PPTSlide");
                    _instance = go.AddComponent<PPTSlide>();
                }
            }
            return _instance;
        }
    }

    // ---------- 数据结构 ----------
    [System.Serializable]
    public class SlideData
    {
        [Tooltip("该页显示的图片")]
        public Sprite slideImage;

        [Tooltip("说话人名字")]
        public string speakerName = "角色名";

        [Tooltip("对话内容")]
        
        public string dialogueText = "这里是对话内容...";
    }

    // ---------- 可配置参数 ----------
    [Header("幻灯片 + 对话列表（按顺序填写）")]
    public List<SlideData> slides = new List<SlideData>();

    [Header("UI 引用")]
    [Tooltip("显示图片的 Image")]
    public Image slideImageDisplay;

    [Tooltip("显示名字的 Text")]
    public Text nameText;

    [Tooltip("显示对话的 Text")]
    public Text contentText;

    // ---------- 内部变量 ----------
    private int _currentIndex = -1;

    // ---------- Unity 生命周期 ----------
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
        if (slides.Count > 0)
            ShowSlide(0);
    }

    // ---------- 公共方法 ----------

    /// <summary>显示下一条</summary>
    public void NextSlide()
    {
        if (slides.Count == 0) return;
        ShowSlide((_currentIndex + 1) % slides.Count);
    }

    /// <summary>显示上一条</summary>
    public void PreviousSlide()
    {
        if (slides.Count == 0) return;
        ShowSlide((_currentIndex - 1 + slides.Count) % slides.Count);
    }

    /// <summary>显示指定索引（图片 + 名字 + 对话同步切换）</summary>
    public void ShowSlide(int index)
    {
        if (index < 0 || index >= slides.Count)
        {
            Debug.LogError($"索引 {index} 超出范围 (0-{slides.Count - 1})");
            return;
        }

        _currentIndex = index;
        SlideData data = slides[index];

        if (slideImageDisplay != null) slideImageDisplay.sprite = data.slideImage;
        if (nameText != null) nameText.text = data.speakerName;
        if (contentText != null) contentText.text = data.dialogueText;
    }

    /// <summary>获取当前索引</summary>
    public int GetCurrentIndex() => _currentIndex;

    /// <summary>获取总条数</summary>
    public int GetSlideCount() => slides.Count;
}