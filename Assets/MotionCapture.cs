using System.Collections.Generic;
using UnityEngine;
using System.IO;
using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;
using CsvHelper.Configuration.Attributes;

public class MotionCapture : MonoBehaviour
{
    [SerializeField] private string csvFileName = "CSV_DATA/01.csv";

    // Stores every row (frame) of CSV data.
    public List<MoCapRow> allData = new List<MoCapRow>();

    void Start()
    {
        string filePath = Path.Combine(Application.dataPath, csvFileName);
        Debug.Log("Looking for CSV at: " + filePath);

        if (!File.Exists(filePath))
        {
            Debug.LogError("CSV file was not found at: " + filePath);
            return;
        }

        var config = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            Delimiter = ",",
            HasHeaderRecord = true,
            MissingFieldFound = null,
        };

        using (var reader = new StreamReader(filePath))
        using (var csv = new CsvReader(reader, config))
        {
            allData = new List<MoCapRow>(csv.GetRecords<MoCapRow>());
        }

        Debug.Log($"Loaded {allData.Count} rows from the CSV file.");
    }
}

public class MoCapRow
{
    [Name("timestamp")]
    public long? Timestamp { get; set; }
    [Name("serial")]
    public long? Serial { get; set; }
    [Name("id")]
    public int? Id { get; set; }

    [Name("JOINT_WAIST.x"), Optional]
    public float? JOINT_WAIST_x { get; set; }
    [Name("JOINT_WAIST.y"), Optional]
    public float? JOINT_WAIST_y { get; set; }
    [Name("JOINT_WAIST.z"), Optional]
    public float? JOINT_WAIST_z { get; set; }

    [Name("JOINT_TORSO.x"), Optional]
    public float? JOINT_TORSO_x { get; set; }
    [Name("JOINT_TORSO.y"), Optional]
    public float? JOINT_TORSO_y { get; set; }
    [Name("JOINT_TORSO.z"), Optional]
    public float? JOINT_TORSO_z { get; set; }
    [Name("JOINT_TORSO.rot"), Optional]
    public float? JOINT_TORSO_rot { get; set; }

    [Name("JOINT_NECK.x"), Optional]
    public float? JOINT_NECK_x { get; set; }
    [Name("JOINT_NECK.y"), Optional]
    public float? JOINT_NECK_y { get; set; }
    [Name("JOINT_NECK.z"), Optional]
    public float? JOINT_NECK_z { get; set; }
    [Name("JOINT_NECK.rot"), Optional]
    public float? JOINT_NECK_rot { get; set; }

    [Name("JOINT_HEAD.x"), Optional]
    public float? JOINT_HEAD_x { get; set; }
    [Name("JOINT_HEAD.y"), Optional]
    public float? JOINT_HEAD_y { get; set; }
    [Name("JOINT_HEAD.z"), Optional]
    public float? JOINT_HEAD_z { get; set; }

    [Name("JOINT_RIGHT_COLLAR.x"), Optional]
    public float? JOINT_RIGHT_COLLAR_x { get; set; }
    [Name("JOINT_RIGHT_COLLAR.y"), Optional]
    public float? JOINT_RIGHT_COLLAR_y { get; set; }
    [Name("JOINT_RIGHT_COLLAR.z"), Optional]
    public float? JOINT_RIGHT_COLLAR_z { get; set; }

    [Name("JOINT_LEFT_COLLAR.x"), Optional]
    public float? JOINT_LEFT_COLLAR_x { get; set; }
    [Name("JOINT_LEFT_COLLAR.y"), Optional]
    public float? JOINT_LEFT_COLLAR_y { get; set; }
    [Name("JOINT_LEFT_COLLAR.z"), Optional]
    public float? JOINT_LEFT_COLLAR_z { get; set; }

    [Name("JOINT_LEFT_SHOULDER.x"), Optional]
    public float? JOINT_LEFT_SHOULDER_x { get; set; }
    [Name("JOINT_LEFT_SHOULDER.y"), Optional]
    public float? JOINT_LEFT_SHOULDER_y { get; set; }
    [Name("JOINT_LEFT_SHOULDER.z"), Optional]
    public float? JOINT_LEFT_SHOULDER_z { get; set; }
    [Name("JOINT_LEFT_SHOULDER.rot"), Optional]
    public float? JOINT_LEFT_SHOULDER_rot { get; set; }

    [Name("JOINT_LEFT_ELBOW.x"), Optional]
    public float? JOINT_LEFT_ELBOW_x { get; set; }
    [Name("JOINT_LEFT_ELBOW.y"), Optional]
    public float? JOINT_LEFT_ELBOW_y { get; set; }
    [Name("JOINT_LEFT_ELBOW.z"), Optional]
    public float? JOINT_LEFT_ELBOW_z { get; set; }
    [Name("JOINT_LEFT_ELBOW.rot"), Optional]
    public float? JOINT_LEFT_ELBOW_rot { get; set; }

    [Name("JOINT_LEFT_WRIST.x"), Optional]
    public float? JOINT_LEFT_WRIST_x { get; set; }
    [Name("JOINT_LEFT_WRIST.y"), Optional]
    public float? JOINT_LEFT_WRIST_y { get; set; }
    [Name("JOINT_LEFT_WRIST.z"), Optional]
    public float? JOINT_LEFT_WRIST_z { get; set; }

    [Name("JOINT_LEFT_HAND.x"), Optional]
    public float? JOINT_LEFT_HAND_x { get; set; }
    [Name("JOINT_LEFT_HAND.y"), Optional]
    public float? JOINT_LEFT_HAND_y { get; set; }
    [Name("JOINT_LEFT_HAND.z"), Optional]
    public float? JOINT_LEFT_HAND_z { get; set; }

    [Name("JOINT_RIGHT_SHOULDER.x"), Optional]
    public float? JOINT_RIGHT_SHOULDER_x { get; set; }
    [Name("JOINT_RIGHT_SHOULDER.y"), Optional]
    public float? JOINT_RIGHT_SHOULDER_y { get; set; }
    [Name("JOINT_RIGHT_SHOULDER.z"), Optional]
    public float? JOINT_RIGHT_SHOULDER_z { get; set; }
    [Name("JOINT_RIGHT_SHOULDER.rot"), Optional]
    public float? JOINT_RIGHT_SHOULDER_rot { get; set; }

    [Name("JOINT_RIGHT_ELBOW.x"), Optional]
    public float? JOINT_RIGHT_ELBOW_x { get; set; }
    [Name("JOINT_RIGHT_ELBOW.y"), Optional]
    public float? JOINT_RIGHT_ELBOW_y { get; set; }
    [Name("JOINT_RIGHT_ELBOW.z"), Optional]
    public float? JOINT_RIGHT_ELBOW_z { get; set; }
    [Name("JOINT_RIGHT_ELBOW.rot"), Optional]
    public float? JOINT_RIGHT_ELBOW_rot { get; set; }

    [Name("JOINT_RIGHT_WRIST.x"), Optional]
    public float? JOINT_RIGHT_WRIST_x { get; set; }
    [Name("JOINT_RIGHT_WRIST.y"), Optional]
    public float? JOINT_RIGHT_WRIST_y { get; set; }
    [Name("JOINT_RIGHT_WRIST.z"), Optional]
    public float? JOINT_RIGHT_WRIST_z { get; set; }

    [Name("JOINT_RIGHT_HAND.x"), Optional]
    public float? JOINT_RIGHT_HAND_x { get; set; }
    [Name("JOINT_RIGHT_HAND.y"), Optional]
    public float? JOINT_RIGHT_HAND_y { get; set; }
    [Name("JOINT_RIGHT_HAND.z"), Optional]
    public float? JOINT_RIGHT_HAND_z { get; set; }

    [Name("JOINT_LEFT_HIP.x"), Optional]
    public float? JOINT_LEFT_HIP_x { get; set; }
    [Name("JOINT_LEFT_HIP.y"), Optional]
    public float? JOINT_LEFT_HIP_y { get; set; }
    [Name("JOINT_LEFT_HIP.z"), Optional]
    public float? JOINT_LEFT_HIP_z { get; set; }
    [Name("JOINT_LEFT_HIP.rot"), Optional]
    public float? JOINT_LEFT_HIP_rot { get; set; }

    [Name("JOINT_LEFT_KNEE.x"), Optional]
    public float? JOINT_LEFT_KNEE_x { get; set; }
    [Name("JOINT_LEFT_KNEE.y"), Optional]
    public float? JOINT_LEFT_KNEE_y { get; set; }
    [Name("JOINT_LEFT_KNEE.z"), Optional]
    public float? JOINT_LEFT_KNEE_z { get; set; }
    [Name("JOINT_LEFT_KNEE.rot"), Optional]
    public float? JOINT_LEFT_KNEE_rot { get; set; }

    [Name("JOINT_LEFT_ANKLE.x"), Optional]
    public float? JOINT_LEFT_ANKLE_x { get; set; }
    [Name("JOINT_LEFT_ANKLE.y"), Optional]
    public float? JOINT_LEFT_ANKLE_y { get; set; }
    [Name("JOINT_LEFT_ANKLE.z"), Optional]
    public float? JOINT_LEFT_ANKLE_z { get; set; }

    [Name("JOINT_RIGHT_HIP.x"), Optional]
    public float? JOINT_RIGHT_HIP_x { get; set; }
    [Name("JOINT_RIGHT_HIP.y"), Optional]
    public float? JOINT_RIGHT_HIP_y { get; set; }
    [Name("JOINT_RIGHT_HIP.z"), Optional]
    public float? JOINT_RIGHT_HIP_z { get; set; }
    [Name("JOINT_RIGHT_HIP.rot"), Optional]
    public float? JOINT_RIGHT_HIP_rot { get; set; }

    [Name("JOINT_RIGHT_KNEE.x"), Optional]
    public float? JOINT_RIGHT_KNEE_x { get; set; }
    [Name("JOINT_RIGHT_KNEE.y"), Optional]
    public float? JOINT_RIGHT_KNEE_y { get; set; }
    [Name("JOINT_RIGHT_KNEE.z"), Optional]
    public float? JOINT_RIGHT_KNEE_z { get; set; }
    [Name("JOINT_RIGHT_KNEE.rot"), Optional]
    public float? JOINT_RIGHT_KNEE_rot { get; set; }

    [Name("JOINT_RIGHT_ANKLE.x"), Optional]
    public float? JOINT_RIGHT_ANKLE_x { get; set; }
    [Name("JOINT_RIGHT_ANKLE.y"), Optional]
    public float? JOINT_RIGHT_ANKLE_y { get; set; }
    [Name("JOINT_RIGHT_ANKLE.z"), Optional]
    public float? JOINT_RIGHT_ANKLE_z { get; set; }
}