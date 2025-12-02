using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameRoot : MonoBehaviour
{
    public static GameRoot Instance;
    public bool IsPause;
    private void Awake()
    {
        if (GameRoot.Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        Init();
        IsPause = false;
        UIService.Instance.ShowPanel<LoginPanel>();
    }
    /// <summary>
    /// 游戏初始化
    /// </summary>
    public void Init()
    {
        //服务层初始化
        GetComponent<ResService>().Init();
        GetComponent<UIService>().Init();
        GetComponent<AudioService>().Init();
        //系统层初始化
        GetComponent<TimerSystem>().Init();
        GetComponent<DataSystem>().Init();
        GetComponent<BagSystem>().Init();
        GetComponent<LevelSystem>().Init();
        GetComponent<DialogueSystem>().Init();
        GetComponent<TaskSystem>().Init();
        GetComponent<ShopSystem>().Init();
        GetComponent<PlayerSystem>().Init();
    }
    //游戏暂停
    public void Pause()
    {
        Time.timeScale = 0;
        GameRoot.Instance.IsPause = true;
        UIService.Instance.ShowPanel<PausePanel>(3);
    }
    //游戏继续
    public void Continue()
    {
        Time.timeScale = 1;
        GameRoot.Instance.IsPause = false;
        UIService.Instance.HidePanel<PausePanel>();
    }
}
