# Overview  

### Dot Product 
Explain here how dot product is used and triggered.

### Linear Interpolation
__Linear interpolation__ was implemented to transition the colors of the directional light. The relevant line of code uses the `Color.Lerp` function to interpolate between two colors, cycling around the colors of the rainbow. It follows the equation: **Interpolated Value=A+(B−A)×t**. As the t value increases, the color returned shifts from `rainbowColors[currentColorIndex]` to `rainbowColors[nextColorIndex]`. When t reaches 1, the color will have completely transitioned to the next color. t starts at 0 and is increased by `Time.deltaTime / transitionDuration`, which means the time it takes to fully transition from one color to the next is controlled by transitionDuration.  

`directionalLight.color = Color.Lerp(rainbowColors[currentColorIndex], rainbowColors[nextColorIndex], t);`   

### Particle + Sound Effects  
Press space to trigger the fart particle and sound effect! Done by applying a custom particle system to the playergame object and adding sound by....  

__Levi__: Dot product, sound effect  
__Angel__: Linear Interpolation, particle effect
