# Motion Capture Research (test-model branch)

## Overview

This Unity project processes real-world motion capture data (from Nuitrack) to animate a humanoid skeleton model inside Unity.

### Unity Version
- **Required Unity Version:** `6000.0.37f1`
- **Supported Package Model:** Skeleton Pack (purchased on March 26, 2025)
  - **Size:** 14.43 MB, 53 files
  - **Includes:** 4 variants and 7 animations

### Animation Clip Ranges (Pre-Split in Model) - Not Used
| Animation          | Frame Range |
|--------------------|-------------|
| Idle               | 0–240       |
| Run                | 241–263     |
| Waiting for Battle | 264–314     |
| Attack             | 315–350     |
| Get Hit            | 351–386     |
| Die                | 387–457     |
| Club Dancing       | 458–1275    |

## How to Load the Scene

If the scene doesn't auto-load in Unity:
1. Go to `Assets/Scenes/`.
2. Open the `SampleScene` file manually.
3. The hierarchy should contain:
   - `Main Camera`
   - `Directional Light`
   - `MotionCapture` (Script)
   - `AnimatedHumanoid`
   - `MoCapManager`

## How to Change CSV Data

Motion data is loaded from a CSV file located in the `Assets/CSV_DATA/` folder.

### To Use a Different CSV File:
1. Place your `.csv` file in `Assets/CSV_DATA/`.
2. Select the `MoCapManager` object in the scene.
3. In the **Inspector**, under the **Motion Capture (Script)**, change the **Csv File Name** field.
   - Example: `CSV_DATA/vr9.csv`

### Requirements for CSV:
- Must be `.csv` format
- Should match the expected structure from **Nuitrack** exports
- Ensure the CSV has appropriate joint names and columns to match `MoCapRow` class fields in code

## Notes
- This branch (`test-model`) focuses on using real skeletal animation instead of visualized spheres.
- To preview or debug animation data, use Unity's Animator window and `HumanoidDriver.cs`.

## Contact
If you encounter missing objects after a scene load (like the skeleton disappearing), double-check scene reference, branch merge conflicts, or file corruption. Reimporting the Skeleton Pack may resolve it.

---