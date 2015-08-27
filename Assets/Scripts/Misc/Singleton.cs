using UnityEngine;

public class Singleton<T> : BaseBehaviour where T : BaseBehaviour
{
  private static bool applicationIsQuitting = false;

  private static T _instance;
  
  private static object _lock = new object();
  
  public static T Instance
  {
    get
    {
      if (applicationIsQuitting) {
        return null;
      }
      
      lock(_lock)
      {
        if (_instance == null)
        {
          _instance = (T) FindObjectOfType(typeof(T));
          
          if (_instance == null)
          {
            Debug.Log("Creating singleton on the fly");
            GameObject singleton = new GameObject();
            _instance = singleton.AddComponent<T>();
            singleton.name = typeof(T).ToString();
          }
        }
        DontDestroyOnLoad(_instance.gameObject);
        
        return _instance;
      }
    }
  }

  public void OnDestroy () {
    applicationIsQuitting = true;
  }
}