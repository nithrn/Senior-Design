using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroy : MonoBehaviour
{
    public static DontDestroy Instance;
    // public static 

    public static int good_reps = 0;
    public static int bad_reps = 0;
    public static int total_reps = 0;
    public static List<float> EMG_y;
    
    // public int good_reps = 0;
    // public int bad_reps = 0;
    // public int total_reps = 0;
    // public List<float> EMG_y;

    void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
            // gameObject.tag = "Data";
        }
        else if (Instance != this)
        {
            Destroy (gameObject);
            Debug.Log("Data was destroyed, Previous loaded");
        }

        // GameObject[] objs = GameObject.FindGameObjectsWithTag("music");

        // if (objs.Length > 1)
        // {
        //     Destroy(this.gameObject);
        // }

        // DontDestroyOnLoad(this.gameObject);
    }
    
}
