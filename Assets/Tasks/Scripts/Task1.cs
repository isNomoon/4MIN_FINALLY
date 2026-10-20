using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Task1 : TaskBase
{
    // Start is called before the first frame update
    [Header("点击规则")]
    [SerializeField] private int requiredClicks = 30;
    [SerializeField] private float minInterval = 1f;   // 最小点击间隔（秒）

    [Header("视角固定")]
    [SerializeField] private Transform cameraTransform; // 留空则用 Camera.main
    [SerializeField] private Transform lookTarget;      // 固定看向的目标

    [Header("相机拉近")]
    [SerializeField] private float zoomFov = 30f;       // 按下时的 FOV
    [SerializeField] private float normalFov = 60f;     // 松开时的 FOV
    [SerializeField] private float zoomSpeed = 10f;

    private Camera cam;
    private Quaternion fixedRotation;   // 开始时记录，固定用
    private Vector3 fixedPosition;
    private float lastClickTime = -999f;
    private int clickCount;
    private bool isPressing;

    protected override void OnBegin()
    {
        clickCount = 0;
        lastClickTime = -999f;
        isPressing = false;

        cam =  cameraTransform.GetComponent<Camera>()
            ;

        if (cam == null)
        {
            Debug.LogError("[Task_Click30] 找不到相机");
            return;
        }

        // 记录并固定视角
        fixedPosition = cam.transform.position;
        fixedRotation = lookTarget != null
            ? Quaternion.LookRotation(lookTarget.position - fixedPosition)
            : cam.transform.rotation;
        cam.transform.SetPositionAndRotation(fixedPosition, fixedRotation);
        cam.fieldOfView = normalFov;

       
    }

    protected override void OnEnd()
    {
        // 复原相机
        if (cam != null) cam.fieldOfView = normalFov;
        isPressing = false;
    }

    private void Update()
    {
        if (cam == null) return;

        // 每帧强制固定视角（防止其他脚本动相机）
        cam.transform.SetPositionAndRotation(fixedPosition, fixedRotation);

        // 按下 / 松开
        if (Input.GetMouseButtonDown(0))
        {
            isPressing = true;
            TryClick();
        }
        if (Input.GetMouseButtonUp(0))
        {
            isPressing = false;
        }

        // 平滑 FOV 过渡
        float targetFov = isPressing ? zoomFov : normalFov;
        cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, targetFov, Time.deltaTime * zoomSpeed);
    }

    private void TryClick()
    {
        float now = Time.time;
        if (now - lastClickTime < minInterval)
        {
            Debug.Log($"[提示] 点击太快，请间隔 {minInterval} 秒");
            return;
        }

        lastClickTime = now;
        clickCount++;
        Debug.Log($"[进度] {clickCount}/{requiredClicks}");

        if (clickCount >= requiredClicks)
            CompleteTask();
    }
}
