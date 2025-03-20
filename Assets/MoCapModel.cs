// using System.Collections.Generic;
// using UnityEngine;

// #if UNITY_EDITOR
// using UnityEditor; // For drawing Gizmos and context menu functionality in the Editor
// #endif

// // ExecuteAlways ensures this script runs in both Edit Mode and Play Mode.
// [ExecuteAlways]
// public class MoCapModel : MonoBehaviour
// {
//     // Dictionary to store joint GameObjects, keyed by their joint name.
//     public Dictionary<string, GameObject> jointVectors = new Dictionary<string, GameObject>();

//     // Array of joint keys. The order must correspond to the jointPositions array.
//     private readonly string[] jointKeys = new string[]
//     {
//         "JOINT_WAIST",
//         "JOINT_TORSO",
//         "JOINT_NECK",
//         "JOINT_HEAD",
//         "JOINT_RIGHT_COLLAR",
//         "JOINT_LEFT_COLLAR",
//         "JOINT_LEFT_SHOULDER",
//         "JOINT_LEFT_ELBOW",
//         "JOINT_LEFT_WRIST",
//         "JOINT_LEFT_HAND",
//         "JOINT_RIGHT_SHOULDER",
//         "JOINT_RIGHT_ELBOW",
//         "JOINT_RIGHT_WRIST",
//         "JOINT_RIGHT_HAND",
//         "JOINT_LEFT_HIP",
//         "JOINT_LEFT_KNEE",
//         "JOINT_LEFT_ANKLE",
//         "JOINT_RIGHT_HIP",
//         "JOINT_RIGHT_KNEE",
//         "JOINT_RIGHT_ANKLE"
//     };

//     // Array of joint positions. Adjust these Vector3 values to fix the static pose.
//     private readonly Vector3[] jointPositions = new Vector3[]
//     {
//         new Vector3(0f, 1f, 0f),     // JOINT_WAIST
//         new Vector3(0f, 1.2f, 0f),   // JOINT_TORSO
//         new Vector3(0f, 1.4f, 0f),   // JOINT_NECK
//         new Vector3(0f, 1.6f, 0f),   // JOINT_HEAD
//         new Vector3(0.15f, 1.4f, 0f), // JOINT_RIGHT_COLLAR
//         new Vector3(-0.15f, 1.4f, 0f),// JOINT_LEFT_COLLAR
//         new Vector3(-0.3f, 1.3f, 0f), // JOINT_LEFT_SHOULDER
//         new Vector3(-0.5f, 1.1f, 0f), // JOINT_LEFT_ELBOW
//         new Vector3(-0.7f, 1.1f, 0f), // JOINT_LEFT_WRIST
//         new Vector3(-0.8f, 1.1f, 0f), // JOINT_LEFT_HAND
//         new Vector3(0.3f, 1.3f, 0f),  // JOINT_RIGHT_SHOULDER
//         new Vector3(0.5f, 1.1f, 0f),  // JOINT_RIGHT_ELBOW
//         new Vector3(0.7f, 1.1f, 0f),  // JOINT_RIGHT_WRIST
//         new Vector3(0.8f, 1.1f, 0f),  // JOINT_RIGHT_HAND
//         new Vector3(-0.15f, 0.8f, 0f),// JOINT_LEFT_HIP
//         new Vector3(-0.15f, 0.5f, 0f),// JOINT_LEFT_KNEE
//         new Vector3(-0.15f, 0.2f, 0f),// JOINT_LEFT_ANKLE
//         new Vector3(0.15f, 0.8f, 0f), // JOINT_RIGHT_HIP
//         new Vector3(0.15f, 0.5f, 0f), // JOINT_RIGHT_KNEE
//         new Vector3(0.15f, 0.2f, 0f)  // JOINT_RIGHT_ANKLE
//     };

//     // OnEnable runs when the script is loaded or a value is changed in the Inspector.
//     // It runs in both Edit and Play Mode.
//     void OnEnable()
//     {
//         // When not playing (i.e. in Edit Mode), clear and recreate joints.
//         if (!Application.isPlaying)
//         {
//             RemoveAllJointsImmediate();
//             RecreateJoints();
//         }
//     }

//     // Start runs once when the scene starts in Play Mode.
//     void Start()
//     {
//         if (Application.isPlaying)
//         {
//             RemoveAllJoints();
//             RecreateJoints();
//         }
//     }

//     // Remove all child GameObjects instantly (suitable for Edit Mode).
//     private void RemoveAllJointsImmediate()
//     {
//         for (int i = transform.childCount - 1; i >= 0; i--)
//         {
//             DestroyImmediate(transform.GetChild(i).gameObject);
//         }
//         jointVectors.Clear();
//     }

//     // Remove all child GameObjects normally (suitable for runtime).
//     private void RemoveAllJoints()
//     {
//         for (int i = transform.childCount - 1; i >= 0; i--)
//         {
//             Destroy(transform.GetChild(i).gameObject);
//         }
//         jointVectors.Clear();
//     }

//     // Recreate joints and then create bones.
//     public void RecreateJoints()
//     {
//         // Loop through the joint keys and positions to create each joint.
//         for (int i = 0; i < jointKeys.Length && i < jointPositions.Length; i++)
//         {
//             CreateJoint(jointPositions[i], jointKeys[i]);
//         }
//         Debug.Log("Skeleton created in MoCapModel with all joints.");
//         CreateBones(); // After joints are created, create bones between selected joints.
//     }

//     // Creates an individual joint (a sphere) at the specified position and names it.
//     private void CreateJoint(Vector3 pos, string key, float radius = 0.1f)
//     {
//         // Create a sphere primitive.
//         GameObject joint = GameObject.CreatePrimitive(PrimitiveType.Sphere);
//         // Set the GameObject's name to the joint key.
//         joint.name = key;
//         // Set position and scale.
//         joint.transform.position = pos;
//         joint.transform.localScale = Vector3.one * radius;

//         // Remove collider to avoid unnecessary physics calculations.
//         if (!Application.isPlaying)
//             DestroyImmediate(joint.GetComponent<Collider>());
//         else
//             Destroy(joint.GetComponent<Collider>());

//         // Set the material color.
//         Renderer rend = joint.GetComponent<Renderer>();
//         if (rend != null)
//         {
//             if (Application.isPlaying)
//                 rend.material.color = Color.cyan;
//             else
//                 rend.sharedMaterial.color = Color.cyan;
//         }

//         // Parent the joint to this GameObject and store it in the dictionary.
//         joint.transform.parent = transform;
//         jointVectors[key] = joint;
//     }

//     // Create bones (cylinders) between specific joints.
//     private void CreateBones()
//     {
//         // Define bone connections by specifying pairs of joint keys.
//         CreateBone("JOINT_WAIST", "JOINT_TORSO");
//         CreateBone("JOINT_TORSO", "JOINT_NECK");
//         CreateBone("JOINT_NECK", "JOINT_HEAD");
//         CreateBone("JOINT_LEFT_SHOULDER", "JOINT_LEFT_ELBOW");
//         CreateBone("JOINT_LEFT_ELBOW", "JOINT_LEFT_WRIST");
//         CreateBone("JOINT_LEFT_WRIST", "JOINT_LEFT_HAND");
//         CreateBone("JOINT_RIGHT_SHOULDER", "JOINT_RIGHT_ELBOW");
//         CreateBone("JOINT_RIGHT_ELBOW", "JOINT_RIGHT_WRIST");
//         CreateBone("JOINT_RIGHT_WRIST", "JOINT_RIGHT_HAND");
//         CreateBone("JOINT_LEFT_HIP", "JOINT_LEFT_KNEE");
//         CreateBone("JOINT_LEFT_KNEE", "JOINT_LEFT_ANKLE");
//         CreateBone("JOINT_RIGHT_HIP", "JOINT_RIGHT_KNEE");
//         CreateBone("JOINT_RIGHT_KNEE", "JOINT_RIGHT_ANKLE");
//     }

//     // Creates a bone (a cylinder) connecting two joints.
//     private void CreateBone(string keyA, string keyB)
//     {
//         // Check that both joint keys exist.
//         if (!jointVectors.ContainsKey(keyA) || !jointVectors.ContainsKey(keyB))
//             return;

//         // Create a cylinder primitive to represent the bone.
//         GameObject bone = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
//         bone.name = $"Bone_{keyA}_{keyB}";
//         bone.transform.parent = transform;

//         // Remove the collider.
//         if (!Application.isPlaying)
//             DestroyImmediate(bone.GetComponent<Collider>());
//         else
//             Destroy(bone.GetComponent<Collider>());

//         // Attach the BoneUpdater component to update the bone's transform dynamically.
//         BoneUpdater updater = bone.AddComponent<BoneUpdater>();
//         updater.jointA = jointVectors[keyA].transform;
//         updater.jointB = jointVectors[keyB].transform;
//         updater.boneWidth = 0.05f;

//         // Set the bone's color.
//         Renderer rend = bone.GetComponent<Renderer>();
//         if (rend != null)
//         {
//             if (Application.isPlaying)
//                 rend.material.color = Color.white;
//             else
//                 rend.sharedMaterial.color = Color.white;
//         }
//     }

// #if UNITY_EDITOR
//     // Optional: Draw Gizmos for visual debugging in the Scene view.
//     void OnDrawGizmos()
//     {
//         if (jointVectors == null)
//             return;

//         Gizmos.color = Color.yellow;
//         foreach (var kvp in jointVectors)
//         {
//             if (kvp.Value != null)
//             {
//                 Vector3 pos = kvp.Value.transform.position;
//                 Gizmos.DrawSphere(pos, 0.04f);
//                 Handles.Label(pos, kvp.Key);
//             }
//         }
//     }
// #endif
// }