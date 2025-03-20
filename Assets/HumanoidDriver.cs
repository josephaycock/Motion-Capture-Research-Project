using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Drives a Humanoid avatar’s bones from CSV data (MotionCapture).
/// Replace your old sphere-based scripts with this if you want
/// to animate a real humanoid model.
/// </summary>
public class HumanoidDriver : MonoBehaviour
{
    [Tooltip("Animator on your humanoid model. Ensure the model's Rig is set to Humanoid.")]
    public Animator animator;

    [Tooltip("Reference to the CSV data reader (MotionCapture component).")]
    public MotionCapture csvReader;

    [Tooltip("Frame interval in seconds (e.g., 0.033 for ~30 FPS).")]
    public float frameInterval = 0.033f;

    private int currentFrame = 0;
    private float timer = 0f;

    // Map your CSV joint keys to Unity’s HumanBodyBones.
    // NOTE: Some keys (e.g., WRIST vs. HAND) point to the same bone
    // because the Humanoid rig doesn't have a separate "wrist" bone.
    private readonly Dictionary<string, HumanBodyBones> csvToBoneMap = new Dictionary<string, HumanBodyBones>()
    {
        // Upper Body
        { "JOINT_WAIST",          HumanBodyBones.Hips },
        { "JOINT_TORSO",          HumanBodyBones.Chest },  // or Spine, Spine2, etc. depending on your avatar
        { "JOINT_NECK",           HumanBodyBones.Neck },
        { "JOINT_HEAD",           HumanBodyBones.Head },

        // Shoulders / Collars
        { "JOINT_RIGHT_COLLAR",   HumanBodyBones.RightShoulder },
        { "JOINT_LEFT_COLLAR",    HumanBodyBones.LeftShoulder },

        // Left Arm
        { "JOINT_LEFT_SHOULDER",  HumanBodyBones.LeftUpperArm },
        { "JOINT_LEFT_ELBOW",     HumanBodyBones.LeftLowerArm },
        { "JOINT_LEFT_WRIST",     HumanBodyBones.LeftHand },
        { "JOINT_LEFT_HAND",      HumanBodyBones.LeftHand },  // same bone as WRIST in humanoid

        // Right Arm
        { "JOINT_RIGHT_SHOULDER", HumanBodyBones.RightUpperArm },
        { "JOINT_RIGHT_ELBOW",    HumanBodyBones.RightLowerArm },
        { "JOINT_RIGHT_WRIST",    HumanBodyBones.RightHand },
        { "JOINT_RIGHT_HAND",     HumanBodyBones.RightHand }, // same bone as WRIST in humanoid

        // Left Leg
        { "JOINT_LEFT_HIP",       HumanBodyBones.LeftUpperLeg },
        { "JOINT_LEFT_KNEE",      HumanBodyBones.LeftLowerLeg },
        { "JOINT_LEFT_ANKLE",     HumanBodyBones.LeftFoot },

        // Right Leg
        { "JOINT_RIGHT_HIP",      HumanBodyBones.RightUpperLeg },
        { "JOINT_RIGHT_KNEE",     HumanBodyBones.RightLowerLeg },
        { "JOINT_RIGHT_ANKLE",    HumanBodyBones.RightFoot }
    };

    void FixedUpdate()
    {
        // Ensure CSV data is available and we have frames.
        if (csvReader == null || csvReader.allData == null || csvReader.allData.Count == 0)
            return;

        timer += Time.deltaTime;
        if (timer >= frameInterval)
        {
            // Get the current CSV frame.
            MoCapRow frame = csvReader.allData[currentFrame];

            // For each CSV joint key we care about, drive the corresponding Humanoid bone.
            foreach (var mapping in csvToBoneMap)
            {
                string csvKey = mapping.Key;
                HumanBodyBones boneEnum = mapping.Value;

                // Retrieve the bone transform from the Animator.
                Transform boneTransform = animator.GetBoneTransform(boneEnum);
                if (boneTransform != null)
                {
                    // Attempt to extract a position from the CSV frame.
                    Vector3 csvPos;
                    if (TryGetCSVPosition(frame, csvKey, out csvPos))
                    {
                        // If your CSV data is world-space, this sets the bone's world position.
                        // For local-space data, you'd do boneTransform.localPosition or apply rotations.
                        boneTransform.position = csvPos;
                    }
                }
            }

            // Advance to the next frame in the CSV data.
            currentFrame = (currentFrame + 1) % csvReader.allData.Count;
            timer = 0f;
        }
    }

    /// <summary>
    /// Attempts to extract a joint's position from a CSV frame for the given key.
    /// Extend the switch cases for additional joints if needed.
    /// </summary>
    private bool TryGetCSVPosition(MoCapRow frame, string csvKey, out Vector3 position)
    {
        position = Vector3.zero;
        switch (csvKey)
        {
            // Upper Body
            case "JOINT_WAIST":
                if (frame.JOINT_WAIST_x.HasValue && frame.JOINT_WAIST_y.HasValue && frame.JOINT_WAIST_z.HasValue)
                {
                    position = new Vector3(frame.JOINT_WAIST_x.Value, frame.JOINT_WAIST_y.Value, frame.JOINT_WAIST_z.Value);
                    return true;
                }
                break;
            case "JOINT_TORSO":
                if (frame.JOINT_TORSO_x.HasValue && frame.JOINT_TORSO_y.HasValue && frame.JOINT_TORSO_z.HasValue)
                {
                    position = new Vector3(frame.JOINT_TORSO_x.Value, frame.JOINT_TORSO_y.Value, frame.JOINT_TORSO_z.Value);
                    return true;
                }
                break;
            case "JOINT_NECK":
                if (frame.JOINT_NECK_x.HasValue && frame.JOINT_NECK_y.HasValue && frame.JOINT_NECK_z.HasValue)
                {
                    position = new Vector3(frame.JOINT_NECK_x.Value, frame.JOINT_NECK_y.Value, frame.JOINT_NECK_z.Value);
                    return true;
                }
                break;
            case "JOINT_HEAD":
                if (frame.JOINT_HEAD_x.HasValue && frame.JOINT_HEAD_y.HasValue && frame.JOINT_HEAD_z.HasValue)
                {
                    position = new Vector3(frame.JOINT_HEAD_x.Value, frame.JOINT_HEAD_y.Value, frame.JOINT_HEAD_z.Value);
                    return true;
                }
                break;

            // Collars
            case "JOINT_RIGHT_COLLAR":
                if (frame.JOINT_RIGHT_COLLAR_x.HasValue && frame.JOINT_RIGHT_COLLAR_y.HasValue && frame.JOINT_RIGHT_COLLAR_z.HasValue)
                {
                    position = new Vector3(frame.JOINT_RIGHT_COLLAR_x.Value, frame.JOINT_RIGHT_COLLAR_y.Value, frame.JOINT_RIGHT_COLLAR_z.Value);
                    return true;
                }
                break;
            case "JOINT_LEFT_COLLAR":
                if (frame.JOINT_LEFT_COLLAR_x.HasValue && frame.JOINT_LEFT_COLLAR_y.HasValue && frame.JOINT_LEFT_COLLAR_z.HasValue)
                {
                    position = new Vector3(frame.JOINT_LEFT_COLLAR_x.Value, frame.JOINT_LEFT_COLLAR_y.Value, frame.JOINT_LEFT_COLLAR_z.Value);
                    return true;
                }
                break;

            // Left Arm
            case "JOINT_LEFT_SHOULDER":
                if (frame.JOINT_LEFT_SHOULDER_x.HasValue && frame.JOINT_LEFT_SHOULDER_y.HasValue && frame.JOINT_LEFT_SHOULDER_z.HasValue)
                {
                    position = new Vector3(frame.JOINT_LEFT_SHOULDER_x.Value, frame.JOINT_LEFT_SHOULDER_y.Value, frame.JOINT_LEFT_SHOULDER_z.Value);
                    return true;
                }
                break;
            case "JOINT_LEFT_ELBOW":
                if (frame.JOINT_LEFT_ELBOW_x.HasValue && frame.JOINT_LEFT_ELBOW_y.HasValue && frame.JOINT_LEFT_ELBOW_z.HasValue)
                {
                    position = new Vector3(frame.JOINT_LEFT_ELBOW_x.Value, frame.JOINT_LEFT_ELBOW_y.Value, frame.JOINT_LEFT_ELBOW_z.Value);
                    return true;
                }
                break;
            case "JOINT_LEFT_WRIST":
            case "JOINT_LEFT_HAND": // Both map to the same bone
                if (frame.JOINT_LEFT_HAND_x.HasValue && frame.JOINT_LEFT_HAND_y.HasValue && frame.JOINT_LEFT_HAND_z.HasValue)
                {
                    position = new Vector3(frame.JOINT_LEFT_HAND_x.Value, frame.JOINT_LEFT_HAND_y.Value, frame.JOINT_LEFT_HAND_z.Value);
                    return true;
                }
                break;

            // Right Arm
            case "JOINT_RIGHT_SHOULDER":
                if (frame.JOINT_RIGHT_SHOULDER_x.HasValue && frame.JOINT_RIGHT_SHOULDER_y.HasValue && frame.JOINT_RIGHT_SHOULDER_z.HasValue)
                {
                    position = new Vector3(frame.JOINT_RIGHT_SHOULDER_x.Value, frame.JOINT_RIGHT_SHOULDER_y.Value, frame.JOINT_RIGHT_SHOULDER_z.Value);
                    return true;
                }
                break;
            case "JOINT_RIGHT_ELBOW":
                if (frame.JOINT_RIGHT_ELBOW_x.HasValue && frame.JOINT_RIGHT_ELBOW_y.HasValue && frame.JOINT_RIGHT_ELBOW_z.HasValue)
                {
                    position = new Vector3(frame.JOINT_RIGHT_ELBOW_x.Value, frame.JOINT_RIGHT_ELBOW_y.Value, frame.JOINT_RIGHT_ELBOW_z.Value);
                    return true;
                }
                break;
            case "JOINT_RIGHT_WRIST":
            case "JOINT_RIGHT_HAND": // Both map to the same bone
                if (frame.JOINT_RIGHT_HAND_x.HasValue && frame.JOINT_RIGHT_HAND_y.HasValue && frame.JOINT_RIGHT_HAND_z.HasValue)
                {
                    position = new Vector3(frame.JOINT_RIGHT_HAND_x.Value, frame.JOINT_RIGHT_HAND_y.Value, frame.JOINT_RIGHT_HAND_z.Value);
                    return true;
                }
                break;

            // Left Leg
            case "JOINT_LEFT_HIP":
                if (frame.JOINT_LEFT_HIP_x.HasValue && frame.JOINT_LEFT_HIP_y.HasValue && frame.JOINT_LEFT_HIP_z.HasValue)
                {
                    position = new Vector3(frame.JOINT_LEFT_HIP_x.Value, frame.JOINT_LEFT_HIP_y.Value, frame.JOINT_LEFT_HIP_z.Value);
                    return true;
                }
                break;
            case "JOINT_LEFT_KNEE":
                if (frame.JOINT_LEFT_KNEE_x.HasValue && frame.JOINT_LEFT_KNEE_y.HasValue && frame.JOINT_LEFT_KNEE_z.HasValue)
                {
                    position = new Vector3(frame.JOINT_LEFT_KNEE_x.Value, frame.JOINT_LEFT_KNEE_y.Value, frame.JOINT_LEFT_KNEE_z.Value);
                    return true;
                }
                break;
            case "JOINT_LEFT_ANKLE":
                if (frame.JOINT_LEFT_ANKLE_x.HasValue && frame.JOINT_LEFT_ANKLE_y.HasValue && frame.JOINT_LEFT_ANKLE_z.HasValue)
                {
                    position = new Vector3(frame.JOINT_LEFT_ANKLE_x.Value, frame.JOINT_LEFT_ANKLE_y.Value, frame.JOINT_LEFT_ANKLE_z.Value);
                    return true;
                }
                break;

            // Right Leg
            case "JOINT_RIGHT_HIP":
                if (frame.JOINT_RIGHT_HIP_x.HasValue && frame.JOINT_RIGHT_HIP_y.HasValue && frame.JOINT_RIGHT_HIP_z.HasValue)
                {
                    position = new Vector3(frame.JOINT_RIGHT_HIP_x.Value, frame.JOINT_RIGHT_HIP_y.Value, frame.JOINT_RIGHT_HIP_z.Value);
                    return true;
                }
                break;
            case "JOINT_RIGHT_KNEE":
                if (frame.JOINT_RIGHT_KNEE_x.HasValue && frame.JOINT_RIGHT_KNEE_y.HasValue && frame.JOINT_RIGHT_KNEE_z.HasValue)
                {
                    position = new Vector3(frame.JOINT_RIGHT_KNEE_x.Value, frame.JOINT_RIGHT_KNEE_y.Value, frame.JOINT_RIGHT_KNEE_z.Value);
                    return true;
                }
                break;
            case "JOINT_RIGHT_ANKLE":
                if (frame.JOINT_RIGHT_ANKLE_x.HasValue && frame.JOINT_RIGHT_ANKLE_y.HasValue && frame.JOINT_RIGHT_ANKLE_z.HasValue)
                {
                    position = new Vector3(frame.JOINT_RIGHT_ANKLE_x.Value, frame.JOINT_RIGHT_ANKLE_y.Value, frame.JOINT_RIGHT_ANKLE_z.Value);
                    return true;
                }
                break;
        }
        return false;
    }
}