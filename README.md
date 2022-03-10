Learning Ray Tracing (from scratch) and adapting [The Ray Tracing in One Weekend series](https://raytracing.github.io/) to C#.

The main goals here is to learn ray tracing and improve my C# skills.

For now I'm trying to improve performance.

Execution time (for the image below): **61.16 minutes**.<br>
Aspect ratio: 3:2 | Image Width: 1200px | Samples per pixel: 100 | Max ray bounce: 50 | Number of spheres: 488

<img src="./image_final_scene.png" width="800">

---

First round of optimizations.

Execution time (for the image below): **42.01 minutes**.<br>
Aspect ratio: 3:2 | Image Width: 1200px | Samples per pixel: 100 | Max ray bounce: 50 | Number of spheres: 488

<img src="./image_final_first_opt.png" width="800">

---

Second round of optimizations.

Execution time (for the image below): **38.29 minutes**.<br>
Aspect ratio: 3:2 | Image Width: 1200px | Samples per pixel: 100 | Max ray bounce: 50 | Number of spheres: 488

<img src="./image_final_second_opt.png" width="800">
