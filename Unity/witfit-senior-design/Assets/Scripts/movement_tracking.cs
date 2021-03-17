using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement_tracking : MonoBehaviour
{
    public  Transform s_joint, e_joint;
    public float speed = 6f;
    public BT_MessageListener MessageListener;

    // public float recv_angl = 0f;
    // public float recv_angl2 = 0f;

    void Update()
    {
        float x1 = MessageListener.x_angl;
        float y1 = MessageListener.y_angl;
        float z1 = MessageListener.z_angl;

        float x2 = MessageListener.x2_angl;
        float y2 = MessageListener.y2_angl;
        float z2 = MessageListener.z2_angl;

        s_joint.transform.eulerAngles = new Vector3 (0,0,x1);
        // if (x2 < x1) {
        //     x2 = x1;
        // }
        e_joint.transform.eulerAngles = new Vector3 (0,0,x2);


        // float recv_angl2 = MessageListener.y_angl;
        // char mflag = MessageListener.msg_flag;
        // float recv_angl3 = MessageListener.z_angl;
        // float recv_angl4 = MessageListener.w_angl;
        
        // s_joint.transform.eulerAngles = new Vector3 (0, recv_angl, 0);
        // e_joint.transform.eulerAngles = new Vector3 (0,  s_joint.transform.eulerAngles.y + recv_angl, 0);
        // if (recv_angl2 < 0)
        // {
        //     recv_angl2 = 0;
        // }
        // else if (recv_angl2 > 160)
        // {
        //     recv_angl2 = 160;
        // }
        
        // s_joint.transform.rotation = new Quaternion(recv_angl2, recv_angl3, recv_angl, recv_angl4);
        // s_joint.transform.rotation = new Quaternion(0, 0, recv_angl, recv_angl4);



        // s_joint.transform.eulerAngles = new Vector3 (recv_angl2, recv_angl3, recv_angl);
        // if (recv_angl <= -180.0f) {
        //     recv_angl = -180.0f;
        // }
        // else if (recv_angl <= 110.0f) {
        //     recv_angl = 110.0f;
        // }

        // if (mflag == 'f') {
        //     s_joint.transform.eulerAngles = new Vector3 (0, 0, recv_angl);
        // }
        // else if (mflag == 'b') {
        //     if (recv_angl < 0) {
        //         recv_angl = 0;
        //     }
        //     e_joint.transform.eulerAngles = new Vector3 (0, 0, recv_angl);
        // }
        


        
        
        // s_joint.transform.eulerAngles = new Vector3 (0, recv_angl3, 0);
        // if (recv_angl < 0)
        // {
        //     recv_angl = 0;
        // }
        // e_joint.transform.eulerAngles = new Vector3 (0, 0, s_joint.transform.eulerAngles.z + recv_angl);
        // e_joint.transform.eulerAngles = new Vector3 (0,0,recv_angl2);
    }

    

}
