# Motion Capture Research (Main Branch)

> ⚠️ **Note:** This main branch contains an older version of the project and is not the most up-to-date implementation.  
> The latest and actively developed branches are:
> - `test-model`: Uses an imported animated skeleton model (Skeleton Pack).
> - `rudimentary-model`: Procedurally generates a skeleton using spheres for joints and cylinders for bones.

---

## Project Overview

This Unity project visualizes motion capture data collected via Nuitrack software and parsed from CSV files. It enables playback of user-recorded motion by mapping joint data into a Unity scene.

---

## Unity Version

- Developed with: **Unity 6000.0.37f1**

---

## CSV Motion Playback

The motion capture playback uses CSV files under the `Assets/CSV_DATA/` directory. These files contain joint positions for each frame.

### How to Change the CSV File:
1. Select the `MotionCapture` GameObject in the **Hierarchy**.
2. In the **Inspector**, locate the `MotionCapture` script.
3. Update the `Csv File Name` field with the path to your new CSV file (e.g., `CSV_DATA/yourfile.csv`).
4. Make sure your new file is placed in the `Assets/CSV_DATA/` directory.

---

## Scene and Hierarchy

The scene is stored at:  
`Assets/Scenes/SampleScene.unity`

If the scene does not appear in Unity:
- Open `Assets/Scenes/`
- Double-click on `SampleScene` to load it.

### Hierarchy Contents in This Branch:
- `Main Camera`
- `Directional Light`
- `MotionCapture (Script)`
- `MotionCapturePlayBack (Script)`
- `MotionCaptureModel (Script)`

> This structure was used to drive a custom model made of GameObjects, before introducing the animated humanoid or procedural skeleton representations.

---

## Data Format

- CSVs used here follow the Nuitrack output format.
- Fields include joint names and (x, y, z) position values.
- Example joint: `JOINT_LEFT_SHOULDER.x`, `JOINT_LEFT_SHOULDER.y`, `JOINT_LEFT_SHOULDER.z`

---

## Notes

- This branch has not been updated per instruction and is preserved for reference only.
- Use `rudimentary-model` or `test-model` for the latest functionality and testing.

