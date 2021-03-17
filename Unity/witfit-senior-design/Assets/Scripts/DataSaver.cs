using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DataSaver : MonoBehaviour
{
    // public BT_MessageListener MessageListener;
    public Check_Reps SaveThis;
    string path;
    string path2;
    string path3;
    // void CreateText()
    void Start()
    {   
        //Path of file
        path = Application.dataPath + "/EMG_y_values.csv";
        path2 = Application.dataPath + "/Reps_Data.csv";
        // path3 = Application.dataPath + "/Log_z2.csv";

        //Create file if it doesnt exist
        if (!File.Exists(path))
        {
            //Title
            File.WriteAllText(path,"");
            //Date
            // string date = "Run Date: " + System.DateTime.Now + "\n";
            // File.AppendAllText(path,date);
        }
        else
        {
            File.WriteAllText(path,"");
        }

        if (!File.Exists(path2))
        {
            //Title
            File.WriteAllText(path2,"");
            //Date
            // string date = "Run Date: " + System.DateTime.Now + "\n";
            // File.AppendAllText(path2,date);
        }
        else
        {
            File.WriteAllText(path2,"");
        }
        // if (!File.Exists(path3))
        // {
        //     //Title
        //     File.WriteAllText(path3,"Data Log \n\n");
        //     //Date
        //     string date = "Run Date: " + System.DateTime.Now + "\n";
        //     File.AppendAllText(path3,date);
        // }
        
        // if (MessageListener != null)
        // {
        //     //Content of File
        //     string content = MessageListener.save_data + ", ";
        //     //Adds x2_angle data to file
        //     File.AppendAllText(path, content);
        // }
    }

    // Update is called once per frame
    public void SaveData()
    {
        if (SaveThis != null)
        {
            if(File.Exists(path)) 
            {
                //Content of File
                List<float> save_EMG = new List<float>(SaveThis.saveEMG_datas);
                for(int i = 0;i < SaveThis.saveEMG_datas.Count; i++)
                {
                    string content = SaveThis.saveEMG_datas[i].ToString() + ",";
                    File.AppendAllText(path, content);
                }
                // string content = save_EMG.ToString() + ",";
                // //Adds x2_angle data to file
                // File.AppendAllText(path, content);
            }

            if(File.Exists(path2)) 
            {
                //Content of File
                string content1 = SaveThis.green_reps + ",";
                string content2 = SaveThis.red_reps + ",";
                string content3 = SaveThis.Rep_cnt + ",";
                //Adds y2_angle data to file
                File.AppendAllText(path2, content1);
                File.AppendAllText(path2, content2);
                File.AppendAllText(path2, content3);
            }

            // if(File.Exists(path3)) 
            // {
            //     //Content of File
            //     string content3 = MessageListener.save_data3 + ",";
            //     //Adds z2_angle data to file
            //     File.AppendAllText(path3, content3);
            // }
            
        }
    }
}
