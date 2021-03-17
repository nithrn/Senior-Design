/**
 * Ardity (Serial Communication for Arduino + Unity)
 * Author: Daniel Wilches <dwilches@gmail.com>
 *
 * This work is released under the Creative Commons Attributions license.
 * https://creativecommons.org/licenses/by/2.0/
 */

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
/**
 * When creating your message listeners you need to implement these two methods:
 *  - OnMessageArrived
 *  - OnConnectionEvent
 */
public class SampleMessageListener : MonoBehaviour
{
    public List<Vector2> EMG_points;
    // Invoked when a line of data is received from the serial device.
    // private float x_val = 0f;
    // public float x_angl = 0f; 

    void OnMessageArrived(string msg)
    {
        // Debug.Log("Message arrived: " + msg);
        string[] data = msg.Split(',');
        // int EMG = Mathf.RoundToInt(float.Parse(data[0])); 
        float x_val = float.Parse(data[0]);
        float EMG = float.Parse(data[1]) / 4.00f; // need to save EMG data points to file that will be read to plot graph
        // Vector2 EMGpoint = (x_val, EMG);
        // float x_val = float.Parse(data[0]);
        
        // float check_x = x_val % 1.0f;
        // if (check_x == 0.05f || check_x == 0.00f) {
        //     EMG_points.Add(new Vector2 (x_val, EMG));
        // }

        EMG_points.Add(new Vector2 (x_val, EMG));


        // EMG_points.Add(new Vector2 (float.Parse(data[0]), float.Parse(data[1])));
        // int x_angl = Mathf.RoundToInt(float.Parse(data[1]));
        // float x_angl = float.Parse(data[2]); //used to rotate limbs around joints
        if (EMG_points.Count > 0) {
            Debug.Log("EMG: " + EMG_points[EMG_points.Count - 1]);
        }
        
        // Debug.Log("Angle: " + x_angl);
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
