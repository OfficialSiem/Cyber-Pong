using UnityEngine;
using UnityEngine.Tilemaps;

//Only passes in componenets
public class Singleton<T> : MonoBehaviour where T : Component
{
    private static T _instance;

    public static T Instance
    {
        get
        {
            //If an instance does not exists
            if (_instance == null)
            {
                //Lets create a gameObject
                GameObject obj = new GameObject();

                //Name the object the name of the Component (because we're expressing in
                //generaic, whatever our component T is, we can name ourselves whatever that type T
                //was -- pretty straight forward
                obj.name = typeof(T).Name;

                //You can hide the gameobject created to just grab the component from Insepctor/Scene with the script below
                obj.hideFlags = HideFlags.HideAndDontSave;
                
                //Now our instance has that component!
                _instance = obj.AddComponent<T>();
            }
            return _instance;
        }

    }

    private void OnDestroy()
    {
        if (_instance == this)
        {
            _instance = null;
        }
    }
}


public class SingletonPersistent<T> : MonoBehaviour where T : Component
{
    private static T _instance;

    public static T Instance
    {
        get
        {
            //Cltr + K + C to single line comment a bunch of code
            //Cltr + K + U to remove comments single from a bunch of code
            //Block Comments, Cltr + Shift + / :: Repeat to remove

            //If an instance does not exists
            /*if (_instance == null)
            {
                //Lets create a gameObject
                GameObject obj = new GameObject();

                //Name the object the name of the Component (because we're expressing in
                //generaic, whatever our component T is, we can name ourselves whatever that type T
                //was -- pretty straight forward
                obj.name = typeof(T).Name;

                //You can hide the gameobject created to just grab the component from Insepctor/Scene with the script below
                obj.hideFlags = HideFlags.HideAndDontSave;

                //Now our instance has that component!
                _instance = obj.AddComponent<T>();
            }*/
            return _instance;
        }

    }

    private void OnDestroy()
    {
        if (_instance == this)
        {
            _instance = null;
        }
    }

    //Virtual being this can be override elsewhere
    public virtual void Awake()
    {
        //If an instance does not exists
        if (_instance == null)
        {
            //Now our instance gets treated as a component
            _instance = this as T;

            //To be persistant Dont Destroy me on load!
            DontDestroyOnLoad(this);
        }
        else
        {
            //And if there's more than one, then destroy this!
            Destroy(this);
        }

    }
}

