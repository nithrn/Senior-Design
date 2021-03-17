using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveAfterCollect : MonoBehaviour
{
    public Check_Reps saveObject;
    // Start is called before the first frame update
    public int good_reps = 0;
    public int bad_reps = 0;
    public int total_reps = 0;
    public List<float> EMG_y;

    void Update()
    {
        if (saveObject != null) 
        {
            good_reps = saveObject.green_reps;
            bad_reps = saveObject.red_reps;
            total_reps = saveObject.Rep_cnt;
            EMG_y = new List<float>(saveObject.saveEMG_datas);
        }
        
    }
}
