using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TrainStatisticsManager : MonoBehaviour
{
    public TextMeshProUGUI t;

    private void Start () {
        List<trainRecord> stats = SaveSystem.LoadTrainStatistics ();

        t.text = JsonConvert.SerializeObject (stats, Formatting.Indented);
    }
}
