using UnityEngine;


/// <summary>
/// A static instance is similar to a singleton, but instead of destroying any new instances, it overrides the current instance.
/// This is handy for reseting the state and saves you doing it manually.
/// </summary>
public abstract class StaticInstance<T> : MonoBehaviour where T : MonoBehaviour
{
    public static T Instance { get; private set; }
    protected virtual void Awake() => Instance = this as T;

    protected virtual void OnApplicationQUit()
    {
        Instance = null;
        Destroy(gameObject);
    }
}


/// <summary>
/// This transforms the static instance into a basic singleton. THis will destroy any new versions created, leaving the original instance intact
/// </summary>
public abstract class Singleton<T> : StaticInstance<T> where T : MonoBehaviour
{
    protected override void Awake()
    {
        if (Instance != null) Destroy(gameObject);
        base.Awake();
    }
}


/// <summary>
/// Finally we have a persistent version of the singleon. This will survive through scene loads.
/// Perfect for system classes which require stateful, persistent data.
/// Or audio sources where music plays through loading screens, etc.
/// </summary>
public abstract class PersistentSingleton<T> : Singleton<T> where T : MonoBehaviour
{
    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
    }
}
