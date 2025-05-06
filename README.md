# Motion Capture Research Project

This repository contains a Unity-based motion capture visualization tool. There are two primary branches with different visualization methods for recorded `.csv` motion data captured using **Nuitrack**.

---
## ✅ Branch: `rudimentary-model` — Spheres & Cylinders for Joints and Bones

This branch uses a custom script (`MotionCaptureModel.cs`) to create joint and bone representations.

### ✅ Unity Setup
- **Unity Version**: `6000.0.37f1` or higher
- **Visualization**: Spheres as joints, Cylinders as bones

### 📁 Folder Structure

```
Assets/
├── CSV_DATA/               # Motion data
├── Scenes/
│   └── SampleScene.unity   # Main Unity scene
```

### 🔧 Change Motion CSV File

1. Add `.csv` to `Assets/CSV_DATA/`
2. Select `MotionCapture` in the Hierarchy
3. Set **Csv File Name** to: `CSV_DATA/yourfile.csv`
4. Press Play

> ⚠️ CSV file must follow Nuitrack output format

### 🧠 Scene Components

- `MotionCapture`: CSV loader
- `MotionCapturePlayBack`: Frame stepping
- `MotionCaptureModel`: Generates visual joint structure

---

## 🔗 Additional Notes

- Data is based on motion tracking captured via Nuitrack sensors
- Scene file: `SampleScene.unity` is used in both branches
- To visualize different files, simply swap the CSV path in the `MotionCapture` component

---
