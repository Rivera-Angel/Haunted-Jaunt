using UnityEngine;

public class RainbowLight : MonoBehaviour
{
    public Light directionalLight;  // Reference to the Directional Light
    public float transitionDuration = 5f;  // Duration to transition through each color

    private Color[] rainbowColors = new Color[]
    {
        Color.red,
        Color.yellow,
        Color.green,
        Color.cyan,
        Color.blue,
        Color.magenta
    };

    private int currentColorIndex = 0;
    private int nextColorIndex = 1;
    private float t = 0f;  // Transition time tracker

    void Start()
    {
        if (directionalLight == null)
        {
            directionalLight = GetComponent<Light>(); // If no light is set, get the attached one
        }
    }

    void Update()
    {
        // Smoothly transition between the current and next color
        t += Time.deltaTime / transitionDuration;

        directionalLight.color = Color.Lerp(rainbowColors[currentColorIndex], rainbowColors[nextColorIndex], t);

        // Once the transition is complete, move to the next color in the rainbow
        if (t >= 1f)
        {
            t = 0f;
            currentColorIndex = nextColorIndex;
            nextColorIndex = (nextColorIndex + 1) % rainbowColors.Length;  // Loop back to the first color after the last one
        }
    }
}
