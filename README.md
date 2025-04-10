# Requirements:

### Levi Salgado:  
[20] Add at least one gameplay element that uses a dot product in some way (e.g., calculate length, distance, angle, facing direction).  
[20] Add at least one new sound effect with trigger(s).  

### Angel Rivera:  
**Linear interpolation** was implemented to transition between colors of the directional light. The following line of code uses the `Color.Lerp` function to interpolate between two colors, cycling around the colors of the rainbow. It follows the equation:  

**Interpolated Value=A+(B−A)×t**  

As the t value increases, the color returned shifts from `rainbowColors[currentColorIndex]` to `rainbowColors[nextColorIndex]`. When t reaches 1, the color will have completely transitioned to the next color. t starts at 0 and is increased by `Time.deltaTime / transitionDuration`, which means the time it takes to fully transition from one color to the next is controlled by transitionDuration.  

`directionalLight.color = Color.Lerp(rainbowColors[currentColorIndex], rainbowColors[nextColorIndex], t);`   

[20] Add at least one new particle effect with trigger(s).  


### Both:  
[10] In your GitHub repo readme, describe the use of the dot product, linear interpolation, particle effect, and sound effect and how to make these happen in game. Also include the names of your team members and the contributions from each team member.  
