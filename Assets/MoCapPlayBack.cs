// using System.Collections.Generic;
// using UnityEngine;

// #if UNITY_EDITOR
// using UnityEditor; // For drawing Gizmos and labels in the Editor.
// #endif

// /// <summary>
// /// Plays back motion capture data by smoothly interpolating a skeleton's joint positions
// /// between frames from CSV data. It uses a fallback mechanism when CSV data is missing.
// /// </summary>
// public class MotionCapturePlayback : MonoBehaviour
// {
//     [Tooltip("Reference to the skeleton creation component (MoCapModel).")]
//     public MoCapModel skeleton;

//     [Tooltip("Reference to the CSV data reader (MotionCapture).")]
//     public MotionCapture csvReader;

//     [Tooltip("Frame interval in seconds, e.g. ~0.033 for 30 FPS.")]
//     public float frameInterval = 0.033f;

//     private int currentFrame = 0;
//     private float timer = 0f;

//     // Dictionary to store last known valid positions for each joint.
//     private Dictionary<string, Vector3> lastKnownPositions = new Dictionary<string, Vector3>();

//     void FixedUpdate()
//     {
//         // Ensure CSV data is loaded and we have at least two frames.
//         if (csvReader == null || csvReader.allData == null || csvReader.allData.Count < 2)
//             return;

//         timer += Time.deltaTime;
//         // Compute interpolation factor.
//         float t = timer / frameInterval;

//         // Determine indices for previous and next frames.
//         int prevFrame = (currentFrame - 1 + csvReader.allData.Count) % csvReader.allData.Count;
//         int nextFrame = currentFrame;

//         // Update each joint using Lerp between previous and next frame values.
//         UpdateJointLerp("JOINT_WAIST", csvReader.allData[prevFrame], csvReader.allData[nextFrame], t);
//         UpdateJointLerp("JOINT_TORSO", csvReader.allData[prevFrame], csvReader.allData[nextFrame], t);
//         UpdateJointLerp("JOINT_NECK",  csvReader.allData[prevFrame], csvReader.allData[nextFrame], t);
//         UpdateJointLerp("JOINT_HEAD",  csvReader.allData[prevFrame], csvReader.allData[nextFrame], t);
//         UpdateJointLerp("JOINT_RIGHT_COLLAR", csvReader.allData[prevFrame], csvReader.allData[nextFrame], t);
//         UpdateJointLerp("JOINT_LEFT_COLLAR",  csvReader.allData[prevFrame], csvReader.allData[nextFrame], t);
//         UpdateJointLerp("JOINT_LEFT_SHOULDER", csvReader.allData[prevFrame], csvReader.allData[nextFrame], t);
//         UpdateJointLerp("JOINT_LEFT_ELBOW",    csvReader.allData[prevFrame], csvReader.allData[nextFrame], t);
//         UpdateJointLerp("JOINT_LEFT_WRIST",    csvReader.allData[prevFrame], csvReader.allData[nextFrame], t);
//         UpdateJointLerp("JOINT_LEFT_HAND",     csvReader.allData[prevFrame], csvReader.allData[nextFrame], t);
//         UpdateJointLerp("JOINT_RIGHT_SHOULDER",csvReader.allData[prevFrame], csvReader.allData[nextFrame], t);
//         UpdateJointLerp("JOINT_RIGHT_ELBOW",   csvReader.allData[prevFrame], csvReader.allData[nextFrame], t);
//         UpdateJointLerp("JOINT_RIGHT_WRIST",   csvReader.allData[prevFrame], csvReader.allData[nextFrame], t);
//         UpdateJointLerp("JOINT_RIGHT_HAND",    csvReader.allData[prevFrame], csvReader.allData[nextFrame], t);
//         UpdateJointLerp("JOINT_LEFT_HIP",      csvReader.allData[prevFrame], csvReader.allData[nextFrame], t);
//         UpdateJointLerp("JOINT_LEFT_KNEE",     csvReader.allData[prevFrame], csvReader.allData[nextFrame], t);
//         UpdateJointLerp("JOINT_LEFT_ANKLE",    csvReader.allData[prevFrame], csvReader.allData[nextFrame], t);
//         UpdateJointLerp("JOINT_RIGHT_HIP",     csvReader.allData[prevFrame], csvReader.allData[nextFrame], t);
//         UpdateJointLerp("JOINT_RIGHT_KNEE",    csvReader.allData[prevFrame], csvReader.allData[nextFrame], t);
//         UpdateJointLerp("JOINT_RIGHT_ANKLE",   csvReader.allData[prevFrame], csvReader.allData[nextFrame], t);

//         // Advance to the next frame when the timer exceeds the frame interval.
//         if (timer >= frameInterval)
//         {
//             currentFrame = (currentFrame + 1) % csvReader.allData.Count;
//             timer = 0f;
//         }
//     }

//     /// <summary>
//     /// Interpolates and updates a joint's position using Lerp and fallback logic.
//     /// </summary>
//     /// <param name="jointKey">The key identifying the joint in the skeleton dictionary.</param>
//     /// <param name="prevFrame">The previous frame of data.</param>
//     /// <param name="nextFrame">The next frame of data.</param>
//     /// <param name="t">Interpolation factor between 0 and 1.</param>
//     private void UpdateJointLerp(string jointKey, MoCapRow prevFrame, MoCapRow nextFrame, float t)
//     {
//         Vector3 prevPos, nextPos;
//         bool hasPrev = GetJointPosition(prevFrame, jointKey, out prevPos);
//         bool hasNext = GetJointPosition(nextFrame, jointKey, out nextPos);

//         Vector3 newPos = Vector3.zero;
//         if (hasPrev && hasNext)
//         {
//             newPos = Vector3.Lerp(prevPos, nextPos, t);
//         }
//         else if (hasPrev)
//         {
//             newPos = prevPos;
//         }
//         else if (hasNext)
//         {
//             newPos = nextPos;
//         }
//         else if (lastKnownPositions.TryGetValue(jointKey, out Vector3 fallback))
//         {
//             newPos = fallback;
//         }

//         // Update the last known position.
//         lastKnownPositions[jointKey] = newPos;

//         if (skeleton.jointVectors.TryGetValue(jointKey, out GameObject joint))
//         {
//             joint.transform.position = newPos;
//         }
//         else
//         {
//             Debug.LogWarning($"{jointKey} not found in skeleton.jointVectors!");
//         }
//     }

//     /// <summary>
//     /// Retrieves a joint's position from a MoCapRow based on the joint key.
//     /// </summary>
//     /// <param name="row">A single frame of CSV data.</param>
//     /// <param name="jointKey">The joint key (e.g., "JOINT_WAIST").</param>
//     /// <param name="position">Output position if data is valid.</param>
//     /// <returns>True if valid data was found; otherwise, false.</returns>
//     private bool GetJointPosition(MoCapRow row, string jointKey, out Vector3 position)
//     {
//         position = Vector3.zero;
//         switch (jointKey)
//         {
//             case "JOINT_WAIST":
//                 if (row.JOINT_WAIST_x.HasValue && row.JOINT_WAIST_y.HasValue && row.JOINT_WAIST_z.HasValue)
//                 {
//                     position = new Vector3(row.JOINT_WAIST_x.Value, row.JOINT_WAIST_y.Value, row.JOINT_WAIST_z.Value);
//                     return true;
//                 }
//                 break;
//             case "JOINT_TORSO":
//                 if (row.JOINT_TORSO_x.HasValue && row.JOINT_TORSO_y.HasValue && row.JOINT_TORSO_z.HasValue)
//                 {
//                     position = new Vector3(row.JOINT_TORSO_x.Value, row.JOINT_TORSO_y.Value, row.JOINT_TORSO_z.Value);
//                     return true;
//                 }
//                 break;
//             case "JOINT_NECK":
//                 if (row.JOINT_NECK_x.HasValue && row.JOINT_NECK_y.HasValue && row.JOINT_NECK_z.HasValue)
//                 {
//                     position = new Vector3(row.JOINT_NECK_x.Value, row.JOINT_NECK_y.Value, row.JOINT_NECK_z.Value);
//                     return true;
//                 }
//                 break;
//             case "JOINT_HEAD":
//                 if (row.JOINT_HEAD_x.HasValue && row.JOINT_HEAD_y.HasValue && row.JOINT_HEAD_z.HasValue)
//                 {
//                     position = new Vector3(row.JOINT_HEAD_x.Value, row.JOINT_HEAD_y.Value, row.JOINT_HEAD_z.Value);
//                     return true;
//                 }
//                 break;
//             case "JOINT_RIGHT_COLLAR":
//                 if (row.JOINT_RIGHT_COLLAR_x.HasValue && row.JOINT_RIGHT_COLLAR_y.HasValue && row.JOINT_RIGHT_COLLAR_z.HasValue)
//                 {
//                     position = new Vector3(row.JOINT_RIGHT_COLLAR_x.Value, row.JOINT_RIGHT_COLLAR_y.Value, row.JOINT_RIGHT_COLLAR_z.Value);
//                     return true;
//                 }
//                 break;
//             case "JOINT_LEFT_COLLAR":
//                 if (row.JOINT_LEFT_COLLAR_x.HasValue && row.JOINT_LEFT_COLLAR_y.HasValue && row.JOINT_LEFT_COLLAR_z.HasValue)
//                 {
//                     position = new Vector3(row.JOINT_LEFT_COLLAR_x.Value, row.JOINT_LEFT_COLLAR_y.Value, row.JOINT_LEFT_COLLAR_z.Value);
//                     return true;
//                 }
//                 break;
//             case "JOINT_LEFT_SHOULDER":
//                 if (row.JOINT_LEFT_SHOULDER_x.HasValue && row.JOINT_LEFT_SHOULDER_y.HasValue && row.JOINT_LEFT_SHOULDER_z.HasValue)
//                 {
//                     position = new Vector3(row.JOINT_LEFT_SHOULDER_x.Value, row.JOINT_LEFT_SHOULDER_y.Value, row.JOINT_LEFT_SHOULDER_z.Value);
//                     return true;
//                 }
//                 break;
//             case "JOINT_LEFT_ELBOW":
//                 if (row.JOINT_LEFT_ELBOW_x.HasValue && row.JOINT_LEFT_ELBOW_y.HasValue && row.JOINT_LEFT_ELBOW_z.HasValue)
//                 {
//                     position = new Vector3(row.JOINT_LEFT_ELBOW_x.Value, row.JOINT_LEFT_ELBOW_y.Value, row.JOINT_LEFT_ELBOW_z.Value);
//                     return true;
//                 }
//                 break;
//             case "JOINT_LEFT_WRIST":
//                 if (row.JOINT_LEFT_WRIST_x.HasValue && row.JOINT_LEFT_WRIST_y.HasValue && row.JOINT_LEFT_WRIST_z.HasValue)
//                 {
//                     position = new Vector3(row.JOINT_LEFT_WRIST_x.Value, row.JOINT_LEFT_WRIST_y.Value, row.JOINT_LEFT_WRIST_z.Value);
//                     return true;
//                 }
//                 break;
//             case "JOINT_LEFT_HAND":
//                 if (row.JOINT_LEFT_HAND_x.HasValue && row.JOINT_LEFT_HAND_y.HasValue && row.JOINT_LEFT_HAND_z.HasValue)
//                 {
//                     position = new Vector3(row.JOINT_LEFT_HAND_x.Value, row.JOINT_LEFT_HAND_y.Value, row.JOINT_LEFT_HAND_z.Value);
//                     return true;
//                 }
//                 break;
//             case "JOINT_RIGHT_SHOULDER":
//                 if (row.JOINT_RIGHT_SHOULDER_x.HasValue && row.JOINT_RIGHT_SHOULDER_y.HasValue && row.JOINT_RIGHT_SHOULDER_z.HasValue)
//                 {
//                     position = new Vector3(row.JOINT_RIGHT_SHOULDER_x.Value, row.JOINT_RIGHT_SHOULDER_y.Value, row.JOINT_RIGHT_SHOULDER_z.Value);
//                     return true;
//                 }
//                 break;
//             case "JOINT_RIGHT_ELBOW":
//                 if (row.JOINT_RIGHT_ELBOW_x.HasValue && row.JOINT_RIGHT_ELBOW_y.HasValue && row.JOINT_RIGHT_ELBOW_z.HasValue)
//                 {
//                     position = new Vector3(row.JOINT_RIGHT_ELBOW_x.Value, row.JOINT_RIGHT_ELBOW_y.Value, row.JOINT_RIGHT_ELBOW_z.Value);
//                     return true;
//                 }
//                 break;
//             case "JOINT_RIGHT_WRIST":
//                 if (row.JOINT_RIGHT_WRIST_x.HasValue && row.JOINT_RIGHT_WRIST_y.HasValue && row.JOINT_RIGHT_WRIST_z.HasValue)
//                 {
//                     position = new Vector3(row.JOINT_RIGHT_WRIST_x.Value, row.JOINT_RIGHT_WRIST_y.Value, row.JOINT_RIGHT_WRIST_z.Value);
//                     return true;
//                 }
//                 break;
//             case "JOINT_RIGHT_HAND":
//                 if (row.JOINT_RIGHT_HAND_x.HasValue && row.JOINT_RIGHT_HAND_y.HasValue && row.JOINT_RIGHT_HAND_z.HasValue)
//                 {
//                     position = new Vector3(row.JOINT_RIGHT_HAND_x.Value, row.JOINT_RIGHT_HAND_y.Value, row.JOINT_RIGHT_HAND_z.Value);
//                     return true;
//                 }
//                 break;
//             case "JOINT_LEFT_HIP":
//                 if (row.JOINT_LEFT_HIP_x.HasValue && row.JOINT_LEFT_HIP_y.HasValue && row.JOINT_LEFT_HIP_z.HasValue)
//                 {
//                     position = new Vector3(row.JOINT_LEFT_HIP_x.Value, row.JOINT_LEFT_HIP_y.Value, row.JOINT_LEFT_HIP_z.Value);
//                     return true;
//                 }
//                 break;
//             case "JOINT_LEFT_KNEE":
//                 if (row.JOINT_LEFT_KNEE_x.HasValue && row.JOINT_LEFT_KNEE_y.HasValue && row.JOINT_LEFT_KNEE_z.HasValue)
//                 {
//                     position = new Vector3(row.JOINT_LEFT_KNEE_x.Value, row.JOINT_LEFT_KNEE_y.Value, row.JOINT_LEFT_KNEE_z.Value);
//                     return true;
//                 }
//                 break;
//             case "JOINT_LEFT_ANKLE":
//                 if (row.JOINT_LEFT_ANKLE_x.HasValue && row.JOINT_LEFT_ANKLE_y.HasValue && row.JOINT_LEFT_ANKLE_z.HasValue)
//                 {
//                     position = new Vector3(row.JOINT_LEFT_ANKLE_x.Value, row.JOINT_LEFT_ANKLE_y.Value, row.JOINT_LEFT_ANKLE_z.Value);
//                     return true;
//                 }
//                 break;
//             case "JOINT_RIGHT_HIP":
//                 if (row.JOINT_RIGHT_HIP_x.HasValue && row.JOINT_RIGHT_HIP_y.HasValue && row.JOINT_RIGHT_HIP_z.HasValue)
//                 {
//                     position = new Vector3(row.JOINT_RIGHT_HIP_x.Value, row.JOINT_RIGHT_HIP_y.Value, row.JOINT_RIGHT_HIP_z.Value);
//                     return true;
//                 }
//                 break;
//             case "JOINT_RIGHT_KNEE":
//                 if (row.JOINT_RIGHT_KNEE_x.HasValue && row.JOINT_RIGHT_KNEE_y.HasValue && row.JOINT_RIGHT_KNEE_z.HasValue)
//                 {
//                     position = new Vector3(row.JOINT_RIGHT_KNEE_x.Value, row.JOINT_RIGHT_KNEE_y.Value, row.JOINT_RIGHT_KNEE_z.Value);
//                     return true;
//                 }
//                 break;
//             case "JOINT_RIGHT_ANKLE":
//                 if (row.JOINT_RIGHT_ANKLE_x.HasValue && row.JOINT_RIGHT_ANKLE_y.HasValue && row.JOINT_RIGHT_ANKLE_z.HasValue)
//                 {
//                     position = new Vector3(row.JOINT_RIGHT_ANKLE_x.Value, row.JOINT_RIGHT_ANKLE_y.Value, row.JOINT_RIGHT_ANKLE_z.Value);
//                     return true;
//                 }
//                 break;
//         }
//         return false;
//     }

// #if UNITY_EDITOR
//     void OnDrawGizmos()
//     {
//         if (skeleton == null || skeleton.jointVectors == null)
//             return;

//         Gizmos.color = Color.yellow;
//         foreach (var kvp in skeleton.jointVectors)
//         {
//             if (kvp.Value == null) continue;
//             Vector3 pos = kvp.Value.transform.position;
//             Gizmos.DrawSphere(pos, 0.04f);
//             Handles.Label(pos, kvp.Key);
//         }
//     }
// #endif
// }