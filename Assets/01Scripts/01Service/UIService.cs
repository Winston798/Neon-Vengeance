using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIService : SingletonBase<UIService>
{
    public GameObject canvas;
    private List<Transform> Lays = new List<Transform>();
    private Dictionary<string, BasePanel> Panels = new Dictionary<string, BasePanel>();
    private string UIPanelPath = "UI/UIPanel/";
    public override void Init()
    {
        base.Init();
        //增加层级
        for (int i = 0; i < canvas.transform.childCount; i++)
        {
            if (canvas.transform.GetChild(i).tag == "Lay")
                Lays.Add(canvas.transform.GetChild(i));
        }
    }
    /// <summary>
    /// 获取一个已经加载的面板
    /// </summary>
    /// <typeparam name="T">面板类型</typeparam>
    /// <returns></returns>
    public T GetPanel<T>() where T : BasePanel
    {
        string PanelName = typeof(T).Name;
        if (Panels.ContainsKey(PanelName))
        {
            return Panels[PanelName] as T;
        }
        return null;
    }
    public T ShowPanel<T>(int layCount = 0) where T : BasePanel
    {
        if (layCount > Lays.Count)
        {
            Debug.LogError("层级超限");
            return null;
        }
        string PanelName = typeof(T).Name;
        if (Panels.ContainsKey(PanelName))
        {
            return Panels[PanelName] as T;
        }
        GameObject go = Resources.Load<GameObject>(UIPanelPath + PanelName);
        if (go != null)
        {
            GameObject panel = GameObject.Instantiate(go, Lays[layCount]);
            Panels.Add(PanelName, panel.GetComponent<BasePanel>());
            Panels[PanelName].Show();
            Panels[PanelName].layCount = layCount;
            return Panels[PanelName] as T;
        }
        return null;
    }
    public void HidePanel(string PanelName)
    {
        if (Panels.ContainsKey(PanelName))
        {
            Panels[PanelName].Hide();
            GameObject.Destroy(Panels[PanelName].gameObject);
            Panels.Remove(PanelName);
        }
    }
    /// <summary>
    /// 隐藏面板，泛型
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public void HidePanel<T>()
    {
        string PanelName = typeof(T).Name;
        if (Panels.ContainsKey(PanelName))
        {
            Panels[PanelName].Hide();
            GameObject.Destroy(Panels[PanelName].gameObject);
            Panels.Remove(PanelName);
        }
    }
    /// <summary>
    /// 隐藏所有的面板
    /// </summary>
    public void HideAllPanel()
    {
        foreach (var i in Panels.Values)
        {
            i.Hide();
            Destroy(i.gameObject);
        }
        Panels.Clear();
    }
    /// <summary>
    /// 切换面板
    /// </summary>
    /// <param name="basePanel">原来的面版</param>
    /// <typeparam name="T"></typeparam>
    public void SwitchPanel<T>(BasePanel basePanel) where T : BasePanel
    {
        int layCount = basePanel.layCount;
        HidePanel(basePanel.name.Replace("(Clone)", ""));
        ShowPanel<T>(layCount);
    }
    /// <summary>
    /// 更新面板
    /// </summary>
    /// <typeparam name="T">面板类型</typeparam>
    public void UpdatePanel<T>() where T : BasePanel
    {
        string PanelName = typeof(T).Name;
        if (Panels.ContainsKey(PanelName))
        {
            Panels[PanelName].UpdatePanel();
        }
    }
}
