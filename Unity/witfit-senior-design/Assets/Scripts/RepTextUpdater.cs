using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RepTextUpdater : MonoBehaviour
{
    public ReadCsvReps DataReader;
    public Text Good_TextField;
    public Text Bad_TextField;
    public Text Total_TextField;



    // Start is called before the first frame update
    void Start()
    {
        if (DataReader != null)
        {
            //Get data from ReadCSV script
            int good = DataReader.good_reps;
            int bad = DataReader.bad_reps;
            int total = DataReader.total_reps;
            
            //Update Text
            Good_TextField.text = good.ToString();
            Bad_TextField.text = bad.ToString();
            Total_TextField.text = total.ToString();
        }
    }

}
