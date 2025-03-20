using UnityEngine;

// ExecuteAlways ensures the bone updates even in Edit Mode.
[ExecuteAlways]
public class BoneUpdater : MonoBehaviour
{
    [Tooltip("The first joint this bone connects.")]
    public Transform jointA;

    [Tooltip("The second joint this bone connects.")]
    public Transform jointB;

    [Tooltip("The thickness of the bone (cylinder).")]
    public float boneWidth = 0.05f;

    void Update()
    {
        // If either joint is not assigned, do nothing.
        if (jointA == null || jointB == null)
            return;

        // Get positions of both joints.
        Vector3 posA = jointA.position;
        Vector3 posB = jointB.position;

        // Position the bone at the midpoint between the two joints.
        transform.position = (posA + posB) / 2f;

        // Orient the bone so its local Y-axis points from jointA to jointB.
        transform.up = (posB - posA).normalized;

        // Adjust the bone's scale so its height matches the distance between joints.
        float distance = Vector3.Distance(posA, posB);
        // Note: Unity's default Cylinder height is 2, so we scale Y by distance/2.
        transform.localScale = new Vector3(boneWidth, distance / 2f, boneWidth);
    }
}