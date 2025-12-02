using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ResService : SingletonBase<ResService>
{
    public Image BlackUI;
    public AllEquipData_SO EquipData;
    public TaskData_SO TaskData;
    private List<EquipData> AllEquip;
    private List<TaskData> AllTask;
    public string CurrentSceneName => SceneManager.GetActiveScene().name;

    #region 预制体
    private GameObject CoinPrefab;
    #endregion

    public override void Init()
    {
        base.Init();
        LoadEquipData();
        LoadTask();
        CoinPrefab = Resources.Load<GameObject>("Prefab/Coin");
    }
    /// <summary>
    /// 加载预制体并将其生成
    /// </summary>
    /// <param name="Path">预制体路径</param>
    /// <returns></returns>
    public GameObject LoadPrefab(string Path)
    {
        GameObject prefab = Resources.Load<GameObject>(Path);
        GameObject go = Instantiate(prefab);
        return go;
    }
    /// <summary>
    /// 加载场景
    /// </summary>
    /// <param name="SceneName">场景名字</param>
    /// <param name="Loaded">加载完成后的回调</param>
    public void LoadScene(string SceneName, Action Loaded)
    {
        StartCoroutine(IELoadScene(SceneName, Loaded));
    }
    //加载场景协程
    private IEnumerator IELoadScene(string SceneName, Action Loaded)
    {
        //加载进度条
        AudioService.Instance.StopBK();
        LoadPanel panel = UIService.Instance.ShowPanel<LoadPanel>(3);
        float progress = 0;
        while (true)
        {
            yield return null;
            progress += Time.deltaTime * 100;
            if (progress >= 100)
            {
                break;
            }
            panel.SetProgress(progress);
        }
        //加载场景
        AsyncOperation ar = SceneManager.LoadSceneAsync(SceneName);
        while (!ar.isDone)
        {
            yield return null;
        }
        Loaded?.Invoke();
        UIService.Instance.HidePanel<LoadPanel>();
    }

    //加载所有装备的数据
    public void LoadEquipData()
    {
        AllEquip = new List<EquipData>();
        int j = 1;
        foreach (var i in EquipData.AllEquip)
        {
            AllEquip.Add(i);
            i.EquipID = j;
            j++;
        }
    }
    /// <summary>
    /// 根据装备ID得到装备数据
    /// </summary>
    /// <param name="ID">装备ID</param>
    /// <returns></returns>
    public EquipData GetEquipDataByID(int ID)
    {
        foreach (var i in AllEquip)
        {
            if (i.EquipID == ID)
            {
                return i;
            }
        }
        return null;
    }
    /// <summary>
    /// 得到所有的装备数据ID
    /// </summary>
    /// <returns></returns>
    public List<int> GetAllEquipID()
    {
        List<int> temp = new List<int>();
        foreach (var i in AllEquip)
        {
            temp.Add(i.EquipID);
        }
        return temp;
    }
    /// <summary>
    /// 加载任务数据
    /// </summary>
    private void LoadTask()
    {
        AllTask = new List<TaskData>();
        foreach (var i in TaskData.AllTask)
        {
            AllTask.Add(i);
        }
    }
    /// <summary>
    /// 得到任务数据
    /// </summary>
    public List<TaskData> GetAllTaskData()
    {
        return AllTask;
    }
    /// <summary>
    /// 生成金币
    /// </summary>
    /// <param name="Count">数量</param>
    /// <param name="Pos">生成位置</param>
    public void CreatCoin(int Count, Vector3 Pos)
    {
        for (int i = 0; i < Count; i++)
        {
            Instantiate(CoinPrefab, Pos, Quaternion.identity).GetComponent<Coin>().Init();
        }
    }
}
