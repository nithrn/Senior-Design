using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class BT_MessageListener : MonoBehaviour
{
    // public List<Vector2> EMG_points;
    // Invoked when a line of data is received from the serial device.
    // private float x_val = 0f;
    // public char msg_flag = '0';

    // public Vector2 EMG_point;
    public float x_angl = 0f;
    public float y_angl = 0f;
    public float z_angl = 0f;
    public float x2_angl = 0f;
    public float y2_angl = 0f;
    public float z2_angl = 0f;
    public float EMG = 0.0f;
    // public string save_data1;
    // public string save_data2;
    // public string save_data3;


    public float emg_threshold = 950.0f;
    // float low_threshold = 300.0f;
    public float fore_threshold = 45.0f;
    // float sho_threshold = -80.0f; 
    public float err_tolerance = 2.0f; 
    public int check_rep = 0;
    // public int Rep_cnt = 0;

    void OnMessageArrived(string msg)
    {
        // Debug.Log("Message arrived: " + msg);
        string[] data = msg.Split(',');
        // data = msg.Split(',');
        // save_data1 = data[4]; // saves x-angle data of forearm IMU
        // save_data2 = data[5]; // saves x-angle data of forearm IMU
        // save_data3 = data[6]; // saves x-angle data of forearm IMU


        // int EMG = Mathf.RoundToInt(float.Parse(data[0])); 
        // float x_val = float.Parse(data[0]);
        // float EMG = float.Parse(data[1]); // need to save EMG data points to file that will be read to plot graph
        // Vector2 EMGpoint = (x_val, EMG);
        // float x_val = float.Parse(data[0]);
        // float check_x = x_val % 1.0f;
        // if (check_x == 0.05f || check_x == 0.00f) {
        //     EMG_points.Add(new Vector2 (float.Parse(data[0]), float.Parse(data[1])));
        // }
        // EMG_points.Add(new Vector2 (float.Parse(data[0]), float.Parse(data[1])));
        // int x_angl = Mathf.RoundToInt(float.Parse(data[1]));
        // msg_flag = char.Parse(data[0]);

        // float x_val = float.Parse(data[0]);
        // EMG = float.Parse(data[1]);
        EMG = float.Parse(data[0]);
        // EMG_float.x = x_val;
        // EMG_float.y = EMG; 
        // float EMG = float.Parse(data[1]) / 4.00f; // need to save EMG data points to file that will be read to plot graph
        // float check_x = x_val % 1.0f;
        // if (check_x == 0.05f || check_x == 0.00f) {
        //     EMG_points.Add(new Vector2 (x_val, EMG));
        // }

        // if ((EMG_data > emg_threshold) && (x2 > (fore_threshold - err_tolerance)) && (x2 < (fore_threshold + err_tolerance)) && (x1 > (sho_threshold-err_tolerance)) && (x1 < (sho_threshold+err_tolerance))){
        // if ((EMG > emg_threshold) && (x2_angl > (fore_threshold - err_tolerance)) && (x2_angl < (fore_threshold + err_tolerance))) 
        // // if (EMG_data > emg_threshold)   
        // {   
        //     check_rep++;
        //     if(check_rep > 2){
        //         Rep_cnt++;
        //         check_rep = 0;
        //     }
        //     Rep_cnt++; 
        // }


        x_angl = 180.0f - float.Parse(data[1]); //used to rotate shoulder   
        y_angl = float.Parse(data[2]);
        z_angl = float.Parse(data[3]);

        // x2_angl = float.Parse(data[0]); //used to rotate shoulder
        // y2_angl = float.Parse(data[1]);
        // z2_angl = float.Parse(data[2]);

        x2_angl = 180.0f - float.Parse(data[4]); //used to rotate elbow
        y2_angl = float.Parse(data[5]);
        z2_angl = float.Parse(data[6]);

        if (x_angl > 180.0f) 
        {
            x_angl = x_angl - 360.0f;
        }
        if (x2_angl > 180.0f) 
        {
            x2_angl = x2_angl - 360.0f;
        } 
        
        // w_angl = float.Parse(data[3]);
        // if (EMG_points.Count > 0) {
        //     Debug.Log("EMG: " + EMG_points[EMG_points.Count - 1]);
        // }
        
        // Debug.Log("Angle: " + x2_angl);

    }

    // Invoked when a connect/disconnect event occurs. The parameter 'success'
    // will be 'true' upon connection, and 'false' upon disconnection or
    // failure to connect.
    void OnConnectionEvent(bool success)
    {
        if (success)
            Debug.Log("Connection established");
        else
            Debug.Log("Connection attempt failed or disconnection detected");
    }
}





