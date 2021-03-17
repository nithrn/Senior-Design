using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Check_Reps : MonoBehaviour
{
    public BT_MessageListener MessageListener;
    public GameObject RepSphere;
    // public DontDestroy Saver;
    
    // public List<Vector2> saveEMG_points;
    public List<float> saveEMG_datas;
    public int Rep_cnt = 0;
    public int green_reps = 0;
    public int red_reps = 0;
    

    // private float x2 = MessageListener.x2_angl;
    private float relax_angl = -65.0f;
    private float peak_angl = 20.0f;
    private float start_angl = -100.0f;
    // private float peak_angl = 50.0f;
    // private float lower_angl = 
    private ArmState _currentState;
    private float EMG_threshold = 900.0f;
    private int activate_flag = 0;
    private bool flag_set = false; 
    private int save_cnt = 0;
    private bool flag_save1 = false;
    private bool flag_save2 = false;
    private bool flag_save3 = false;
    private bool flag_save4 = false;
    private bool flag_save5 = false;

    // 0 = neutral/start
    // 1 = green
    // 2 = red



    // Update is called once per frame
    void Update()
    {
        float x2 = MessageListener.x2_angl;
        float x1 = MessageListener.x_angl;
        float EMG_data = MessageListener.EMG;
        var SphereRenderer = RepSphere.GetComponent<Renderer>();
        // Vector2 saveEMG = MessageListener.EMG_point;


        switch(_currentState)
        {
            case ArmState.Start:
            {
                //stay in state
                if (x2 >= start_angl)
                {
                    _currentState = ArmState.Start;
                }

                //leave state, go to relax state
                else if (x2 < start_angl)
                {
                    _currentState = ArmState.Relax;
                    Debug.Log("State: Start --> Relax");
                    green_reps = 0;
                    red_reps = 0;
                }

                break;
            }

            case ArmState.Relax:
            {
   
                //Transition
                //stay in state:
                if (x2 <= relax_angl) {
                    _currentState = ArmState.Relax; 
                    // Debug.Log("State: Relax");
                    if (save_cnt < 3)
                    {
                        saveEMG_datas.Add(EMG_data / 4.0f);
                        save_cnt++;
                    }
                }
                //leave state go to lift state
                // if (x2 < peak_angl && x2 > (peak_angl*-1.0f))
                else if (x2 > relax_angl)
                {  
                    _currentState = ArmState.Lift;
                    save_cnt = 0;
                    Debug.Log("State: Lift");
                }
                // else //if angle of motion is not the above
                // {
                //     _currentState = ArmState.Relax;
                // }
                
                

                break;
            }

            case ArmState.Lift:
            {
                // //Action
                // for(int i = 0; i < 3; i++)
                // {
                //     saveEMG_points.Add(saveEMG);
                // }


                //Stay in state:
                // if (x2 < peak_angl && x2 > (peak_angl*-1.0f))
                if (x2 > relax_angl && x2 < peak_angl)  
                {
                    //&& x1 < -50.0f
                    _currentState = ArmState.Lift;
                    if (EMG_data > EMG_threshold && !flag_set && x1 < -65.0f) 
                    {
                        activate_flag = 1; //green
                        flag_set = true;
                    }
                    else if (EMG_data < EMG_threshold && !flag_set)
                    {
                        activate_flag = 2; //red
                    }
                    else if (!flag_set)
                    {
                        activate_flag = 0; //neutral --> FSM error
                    }
                    // Debug.Log("State: Lift"); 
                    if (x2 < 0 && Mathf.Abs(Mathf.Abs(x2) - (48.0f)) <= 2.0f && !flag_save1) // accepts -50 <= x2 <= -46 
                    // if (Mathf.Abs(65.0f - Mathf.Abs(x2)) >= 17.0f && Mat hf.Abs(65.0f - Mathf.Abs(x2)) >= 15.0f && !flag_save) 
                    { 
                        saveEMG_datas.Add(EMG_data / 4.0f);
                        flag_save1 = true;
                    }
                    // else if ((65.0f - x2 <= 17.0f) && (65.0f - x2 >= 15.0f) && !flag_save)
                    else if (x2 < 0 && Mathf.Abs(Mathf.Abs(x2) - (31.0f)) <= 2.0f && !flag_save2) // accepts -33 <= x2 <= -29
                    {
                        saveEMG_datas.Add(EMG_data / 4.0f);
                        flag_save2 = true;
                    }

                    else if (x2 < 0 && Mathf.Abs(Mathf.Abs(x2) - (14.0f)) <= 2.0f && !flag_save3) // accepts  -16 <= x2 <= -12 
                    {
                        saveEMG_datas.Add(EMG_data / 4.0f);
                        flag_save3 = true;
                    }
                    else if (x2 > 0 && Mathf.Abs(x2 - (3.0f)) <= 2.0f && !flag_save4) // accepts  1 <= x2 <= 5 
                    {
                        saveEMG_datas.Add(EMG_data / 4.0f);
                        flag_save4 = true;
                    }
                    else if (x2 > 0 && Mathf.Abs(x2 - (10.0f)) <= 2.0f && !flag_save5) // accepts  8 <= x2 <= 12
                    {
                        saveEMG_datas.Add(EMG_data / 4.0f);
                        flag_save5 = true;
                    }

                }
                //Leave state go to peak state
                else if (x2 >= peak_angl)
                {
                    _currentState = ArmState.Peak;
                    flag_save1 = false;
                    flag_save2 = false;
                    flag_save3 = false;
                    flag_save4 = false;
                    flag_save5 = false;
                    // Debug.Log("State: Peak");
                    Debug.Log("State: Peak and Color: " + activate_flag);        
                }

                break;
            }

            case ArmState.Peak:
            {
                //stay in state
                if (x2 >= peak_angl) 
                {
                    _currentState = ArmState.Peak;
                    if (EMG_data > EMG_threshold && !flag_set && x1 < -65.0f) 
                    {
                        activate_flag = 1; //green
                        flag_set = true;
                    }
                    else if (EMG_data < EMG_threshold && !flag_set)
                    {
                        activate_flag = 2; //red
                    }
                    else if (!flag_set)
                    {
                        activate_flag = 0; //neutral --> FSM error
                    }
                    // 20 < x2 < 60 //positiveeee
                    if (Mathf.Abs(x2 - 28.0f) <= 2.0f && !flag_save1) // accepts 26 <= x2 <= 30 
                    {
                        saveEMG_datas.Add(EMG_data / 4.0f);
                        flag_save1 = true;
                    }
                    else if (Mathf.Abs(x2 - 36.0f) <= 2.0f && !flag_save2) // accepts 34 <= x2 <= 38
                    {
                        saveEMG_datas.Add(EMG_data / 4.0f);
                        flag_save2 = true;
                    }
                    else if (Mathf.Abs(x2 - 44.0f) <= 2.0f && !flag_save3) // accepts  42 <= x2 <= 46 
                    {
                        saveEMG_datas.Add(EMG_data / 4.0f);
                        flag_save3 = true;
                    }
                    else if (Mathf.Abs(x2 - 52.0f) <= 2.0f && !flag_save4) // accepts  50 <= x2 <= 54 
                    {
                        saveEMG_datas.Add(EMG_data / 4.0f);
                        flag_save4 = true;
                    }
                    else if (Mathf.Abs(x2 - 60.0f) <= 2.0f && !flag_save5) // accepts  58 <= x2 <= 62
                    {
                        saveEMG_datas.Add(EMG_data / 4.0f);
                        flag_save5 = true;
                    }
                }

                //leave state go to lower state
                else if (x2 < peak_angl)
                {
                    _currentState = ArmState.Lower;
                    flag_save1 = false;
                    flag_save2 = false;
                    flag_save3 = false;
                    flag_save4 = false;
                    flag_save5 = false;
                    // Debug.Log("State: Lower");
                    Debug.Log("State: Lower and Color: " + activate_flag);  
                }

                break;
            }

            case ArmState.Lower:
            {
                //stay in state
                if (x2 > relax_angl && x2 < peak_angl)
                {
                    _currentState = ArmState.Lower;
                    // if (save_cnt < 20)
                    // {
                    //     saveEMG_datas.Add(EMG_data / 4.0f);
                    //     save_cnt++;
                    // }

                    if (x2 < 0 && Mathf.Abs(Mathf.Abs(x2) - (48.0f)) <= 2.0f && !flag_save1) // accepts -50 < x2 < -46 
                    // if (Mathf.Abs(65.0f - Mathf.Abs(x2)) >= 17.0f && Mathf.Abs(65.0f - Mathf.Abs(x2)) >= 15.0f && !flag_save) 
                    {
                        saveEMG_datas.Add(EMG_data / 4.0f);
                        flag_save1 = true;
                    }
                    // else if ((65.0f - x2 <= 17.0f) && (65.0f - x2 >= 15.0f) && !flag_save)
                    else if (x2 < 0 && Mathf.Abs(Mathf.Abs(x2) - (31.0f)) <= 2.0f && !flag_save2) // accepts -33 <= x2 <= -29
                    {
                        saveEMG_datas.Add(EMG_data / 4.0f);
                        flag_save2 = true;
                    }
                    else if (x2 < 0 && Mathf.Abs(Mathf.Abs(x2) - (14.0f)) <= 2.0f && !flag_save3) // accepts  -16 <= x2 <= -12 
                    {
                        saveEMG_datas.Add(EMG_data / 4.0f);
                        flag_save3 = true;
                    }
                    else if (x2 > 0 && Mathf.Abs(x2 - (3.0f)) <= 2.0f && !flag_save4) // accepts  1 <= x2 <= 5 
                    {
                        saveEMG_datas.Add(EMG_data / 4.0f);
                        flag_save4 = true;
                    }
                    else if (x2 > 0 && Mathf.Abs(x2 - (10.0f)) <= 2.0f && !flag_save5) // accepts  8 <= x2 <= 12
                    {
                        saveEMG_datas.Add(EMG_data / 4.0f);
                        flag_save5 = true;
                    }
                }
                // Leave state, go to Relax
                else if (x2 <= relax_angl) // && EMG_data < low_threshold
                {
                    _currentState = ArmState.Relax;
                    Rep_cnt++;
                    if (activate_flag == 0) //neutral
                    {
                        // RepSphere.renderer.material.color = Color.white;
                        SphereRenderer.material.SetColor("_Color", Color.white);

                    }
                    else if (activate_flag == 1) //green
                    {
                        // RepSphere.renderer.material.color = Color.green;
                        SphereRenderer.material.SetColor("_Color", Color.green);
                        green_reps++;
                    }
                    else if (activate_flag == 2) //red
                    {
                        // RepSphere.renderer.material.color = Color.red;
                        SphereRenderer.material.SetColor("_Color", Color.red);
                        red_reps++;
                    }
                    flag_set = false;
                    flag_save1 = false;
                    flag_save2 = false;
                    flag_save3 = false;
                    flag_save4 = false;
                    flag_save5 = false;

                    // Saver.good_reps = green_reps;
                    // Saver.bad_reps = red_reps;
                    // Saver.total_reps = Rep_cnt;
                    // Saver.EMG_y =  new List<float>(saveEMG_datas);

                    // Saver.Instance.good_reps = green_reps;
                    // Saver.Instance.bad_reps = red_reps;
                    // Saver.Instance.total_reps = Rep_cnt;
                    // Saver.Instance.EMG_y =  new List<float>(saveEMG_datas);
                    
                    // DontDestroy.Instance.good_reps = green_reps;
                    // DontDestroy.Instance.bad_reps = red_reps;
                    // DontDestroy.Instance.total_reps = Rep_cnt;
                    // DontDestroy.Instance.EMG_y =  new List<float>(saveEMG_datas);
                    
                    Debug.Log("State: Relax");
                }

                break;

            }
        }
    }

    public enum ArmState
    {
        Start,
        Relax,
        Lift,
        Peak,
        Lower
    }


}
