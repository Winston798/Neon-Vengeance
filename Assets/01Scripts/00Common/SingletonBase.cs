using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 单例基类
/// 这个项目中，所有单例都是被挂载的
/// </summary>
/// <typeparam name="T"></typeparam>
public class SingletonBase<T> : MonoBehaviour
where T : class
{
    public static T Instance;
    public virtual void Init()
    {
        Instance = this as T;
    }
}
