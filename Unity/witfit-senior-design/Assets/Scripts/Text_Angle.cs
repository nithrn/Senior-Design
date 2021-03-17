using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Text_Angle : MonoBehaviour
{
    public BT_MessageListener MessageListener;
    public Check_Reps RepChecker;

    public Text ShoulderTextField;
    public Text ForeArmTextField;
    public Text EMG_TextField;
    public Text Rep_TextField;

    // public float emg_threshold = 950.0f;
    // // float low_threshold = 300.0f;
    // public float fore_threshold = 45.0f;
    // // float sho_threshold = -80.0f; 
    // public float err_tolerance = 2.0f; 
    // public int check_rep = 0;
    // public int Rep_cnt = 0;
    

    // Update is called once per frame
    void Update()
    {
        if (MessageListener != null)
        {   
        
            float x1 = Mathf.Round(MessageListener.x_angl * 100.0f)/100.0f; //Shoulder
            float x2 = Mathf.Round(MessageListener.x2_angl * 100.0f)/100.0f; //forearm
            float EMG_data = MessageListener.EMG;
            int num_rep = RepChecker.Rep_cnt;
            // float EMG_data = MessageListener.
            // if  x1 > 180.0f) 
            // {
            //     x1 = x1 - 360.0f;
            // }
            // if  x2 > 180.0f) 
            // {
            //     x2 = x2 - 360.0f;
            // } 
            
            // string angle_x2 = x2.ToString();
            // string angle_x = MessageListener.x2_angl.ToString();
            ShoulderTextField.text = x1.ToString();
            ForeArmTextField.text = x2.ToString();
            EMG_TextField.text = EMG_data.ToString();

            // // if ((EMG_data > emg_threshold) && (x2 > (fore_threshold - err_tolerance)) && (x2 < (fore_threshold + err_tolerance)) && (x1 > (sho_threshold-err_tolerance)) && (x1 < (sho_threshold+err_tolerance))){
            // if ((EMG_data > emg_threshold) && (x2 > (fore_threshold - err_tolerance)) && (x2 < (fore_threshold + err_tolerance))) 
            // // if (EMG_data > emg_threshold)   
            // {   
            //     check_rep++;
            //     if(check_rep > 2){
            //         Rep_cnt++;
            //         check_rep = 0;
            //     }
            //     Rep_cnt++; 
            // }

            Rep_TextField.text = num_rep.ToString();

        }
    }
    // void updateReps(int EMG_data,float roll, float roll2) {
    //     if ((EMG > emg_threshold) && (x2 > (fore_threshold - err_tolerance)) && (x2 < (fore_threshold + err_tolerance)) && (roll >(sho_threshold-err_tolerance)) && (roll<(sho_threshold+err_tolerance))){
    //         Check_rep++;
    //         if(Check_rep > 2){
    //         Rep_cnt++;
    //         Check_rep = 0;
    //         } 
    //     }
    // }
}
