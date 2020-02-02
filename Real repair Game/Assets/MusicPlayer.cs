using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    public AK.Wwise.Event music;
    //public AK.Wwise.State musicState;

    public static MusicPlayer instance;

    // Start is called before the first frame update
    void Start()
    {
        if (instance == null) //if instance is empty
        {
            instance = this; //the number of instance will be set to equal to only this script. 
        }
        else
        {
            if (instance != this)
            {
                Destroy(gameObject); //there is more than one, this destroy this one.
            }
        }
        
        DontDestroyOnLoad(gameObject);

        //musicState.SetValue();
        music.Post(gameObject);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
