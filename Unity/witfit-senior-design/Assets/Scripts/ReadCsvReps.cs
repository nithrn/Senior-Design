using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ReadCsvReps : MonoBehaviour
{
    public int good_reps = 0;
    public int bad_reps = 0;
    public int total_reps = 0;
    // Start is called before the first frame update
    void Awake()
    {
        ReadCSVFile2();
        
    }

    void ReadCSVFile2()
    {
        string path2 = Application.dataPath + "/Reps_Data.csv";
        Debug.Log(path2);
        StreamReader strReader2 = new StreamReader(path2);
        bool endOfFile = false;
        while(!endOfFile)
        {
            Debug.Log("Updating Reps");
            string data_str = strReader2.ReadLine();
            if(data_str == null)
            {
                endOfFile = true;
                break;
            }
            string[] data_log = data_str.Split(',');
            good_reps = int.Parse(data_log[0]);
            bad_reps = int.Parse(data_log[1]);
            total_reps = int.Parse(data_log[2]);
        }
    }
}
