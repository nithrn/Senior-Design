using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UILineRenderer : Graphic
{
    // public SampleMessageListener MessageListener;
    public Vector2Int gridSize;
    public UIGridRenderer grid;
    public List<Vector2> points;
    public ReadCsv DataReader;
    // public SaveAfterCollect saved_Data;
    // public GameObject[] grab;
    // public DontDestroy saved_Data;
    // public static 

    // private int good_reps = 0;
    // private int bad_reps = 0;
    // private int total_reps = 0;
    // private List<float> EMG_y;

    float width;
    float height;
    float unitWidth;
    float unitHeight;
    List<float> x_vals;
    float set_x = 0.1f;
    private bool ReadFlag = false;

    public float thickness = 10f;

    // void Start()
    // {
    //     //set List points = EMG_y (saved from Viz_Arm scene)
    // }
    // void Awake()
    // {
    //     if (saved_Data != null)
    //     {
    //         //copy EMG
    //         // List<float> EMGy = new List<float>(saved_Data.EMG_y); 
    //         for (int i = 0; i < saved_Data.EMG_y.Count; i++)
    //         {
    //             // x_vals.Add(set_x);
    //             points.Add(new Vector2(set_x, saved_Data.EMG_y[i]));
    //             set_x = set_x + 0.1f;
                
    //         }
    //         // points.Add(new Vector2())
    //     }
    // }

    // void Awake()
    // {
    //     if (DataReader != null)
    //     {
    //         //copy EMG
    //         // List<float> EMGy = new List<float>(saved_Data.EMG_y);
    //         points.Clear(); 
    //         Debug.Log("Inserting Data");
    //         for (int i = 0; i < DataReader.EMG_y.Count; i++)
    //         {
    //             // x_vals.Add(set_x);
    //             points.Add(new Vector2(set_x, DataReader.EMG_y[i]));
    //             set_x = set_x + 0.1f;
                
    //         }
    //     }
    // }


    protected override void OnPopulateMesh(VertexHelper vh)
    {
        vh.Clear();
        width = rectTransform.rect.width;
        height = rectTransform.rect.height;

        unitWidth = width / (float)gridSize.x;
        unitHeight = height / (float)gridSize.y;
        
        // grab = GameObject.FindGameObjectsWithTag("Data");
        // saved_Data = GetComponent<DontDestroy>();
        // Debug.Log(saved_Data);
        
        // good_reps = DontDestroy.good_reps;
        // bad_reps = DontDestroy.bad_reps;
        // total_reps = DontDestroy.total_reps;
        // EMG_y = DontDestroy.EMG_y;

        // good_reps = Check_Reps.green_reps;
        // bad_reps = Check_Reps.red_reps;
        // total_reps = Check_Reps.Rep_cnt;
        // EMG_y = new List<float>(Check_Reps.saveEMG_datas);

        // for (int i = 0; i < EMG_y.Count; i++)
        // {
        //     // x_vals.Add(set_x);
        //     points.Add(new Vector2(set_x, EMG_y[i]));
        //     set_x = set_x + 0.1f;
            
        // }

        if (DataReader != null && !ReadFlag)
        {
            //copy EMG
            // List<float> EMGy = new List<float>(saved_Data.EMG_y);
            points.Clear(); 
            points.Add(new Vector2(0f,0f));
            Debug.Log("Inserting Data");
            for (int i = 0; i < DataReader.EMG_y.Count; i++)
            {
                // x_vals.Add(set_x);
                points.Add(new Vector2(set_x, DataReader.EMG_y[i]));
                set_x = set_x + 0.1f;
                
            }
            ReadFlag = true;
        }

        // Debug.Log("Points Updated");

        if(points.Count<2)
        {
           return;
        }

        float angle = 0;
        for(int i=0; i<points.Count; i++)
        {
           Vector2 point = points[i];

           if(i<points.Count - 1)
           {
               angle = GetAngle(points[i], points[i+1]) + 45f;
           }


           DrawVerticesForPoint(point, vh, angle);
        }

        for(int i=0; i<points.Count-1; i++)
        {
            int index = i * 2;
            vh.AddTriangle(index + 0, index + 1, index + 3);
            vh.AddTriangle(index + 3, index + 2, index + 0);

        }

    }

    public float GetAngle(Vector2 me, Vector2 target)
    {
        return (float)(Mathf.Atan2(target.y - me.y, target.x - me.x) * (180 / Mathf.PI));
    }


    void DrawVerticesForPoint(Vector2 point, VertexHelper vh, float angle)
    {
        UIVertex vertex = UIVertex.simpleVert;
        vertex.color = color;
        
        vertex.position = Quaternion.Euler(0, 0, angle) * new Vector3(-thickness / 2, 0);
        vertex.position += new Vector3(unitWidth * point.x, unitHeight * point.y);
        vh.AddVert(vertex);

        vertex.position = Quaternion.Euler(0, 0, angle) * new Vector3(thickness / 2, 0);
        vertex.position += new Vector3(unitWidth * point.x, unitHeight * point.y);
        vh.AddVert(vertex);
    }

    private void Update()
    {
        if(grid != null)
        {
            if(gridSize != grid.gridSize) {
                gridSize = grid.gridSize;
                SetVerticesDirty();
            }
        }

        // if(MessageListener != null)
        // {
        //     if(MessageListener.EMG_points.Count > 0 && MessageListener.EMG_points.Count < 25 && MessageListener.EMG_points[MessageListener.EMG_points.Count - 1] != points[points.Count - 1]) 
        //     {
        //         points.Add(MessageListener.EMG_points[MessageListener.EMG_points.Count - 1]);
        //     }
        // }
    }
}
