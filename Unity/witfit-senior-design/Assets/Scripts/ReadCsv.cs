using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ReadCsv : MonoBehaviour
{
    public List<float> EMG_y;
    // public int good_reps = 0;
    // public int bad_reps = 0;
    // public int total_reps = 0;
    // Start is called before the first frame update
    void Start()
    {
        // string path = Application.dataPath + "/EMG_y_values.csv";
        // string path2 = Application.dataPath + "/Reps_Data.csv";
        ReadCSVFile1();
        // ReadCSVFile2();
        // Debug.Log("Updated Data");
        
    }

    // Update is called once per frame
    void ReadCSVFile1()
    {
        string path = Application.dataPath + "/EMG_y_values.csv";
        Debug.Log(path);
        // string path2 = Application.dataPath + "/Reps_Data.csv";
        StreamReader strReader = new StreamReader(path);
        bool endOfFile = false;
        while(!endOfFile)
        {
            string data_str = strReader.ReadLine();
            if(data_str == null)
            {
                endOfFile = true;
                break;
            }
            string[] data_log = data_str.Split(',');
            for (int i = 0; i < data_log.Length; i++)
            {
                EMG_y.Add(float.Parse(data_log[i]));
            }

        }
        // StreamReader strReader2 = new StreamReader(path2);
        // endOfFile = false;
        // while(!endOfFile)
        // {
        //     Debug.Log("Updating Reps");
        //     string data_str = strReader2.ReadLine();
        //     if(data_str == null)
        //     {
        //         endOfFile = true;
        //         break;
        //     }
        //     string[] data_log = data_str.Split(',');
        //     // Debug.Log(data_log);
        //     // for (int i = 0; i < data_log.Length; i++)
        //     // {
        //     //     Debug.Log(data_log[i]);
        //     // }
        //     good_reps = int.Parse(data_log[0]);
        //     bad_reps = int.Parse(data_log[1]);
        //     total_reps = int.Parse(data_log[2]);
        //     // for (int i = 0; i < data_log.Length; i++)
        //     // {
        //     //     EMG_y.Add(float.Parse(data_log[i]));
        //     // }
        // }

    }

    // void ReadCSVFile2()
    // {
    //     string path2 = Application.dataPath + "/Reps_Data.csv";
    //     Debug.Log(path2);
    //     StreamReader strReader2 = new StreamReader(path2);
    //     bool endOfFile = false;
    //     while(!endOfFile)
    //     {
    //         Debug.Log("Updating Reps");
    //         string data_str = strReader2.ReadLine();
    //         if(data_str == null)
    //         {
    //             endOfFile = true;
    //             break;
    //         }
    //         string[] data_log = data_str.Split(',');
    //         good_reps = int.Parse(data_log[0]);
    //         bad_reps = int.Parse(data_log[1]);
    //         total_reps = int.Parse(data_log[2]);
    //     }
    // }
}
