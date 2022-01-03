using UnityEngine;
using UnityEngine.UI;


public class AnimalFoodBar : MonoBehaviour
{
    public Slider foodSlider;
    public int maxFoodValue;
    private Slider animalSlider;
    private int currentFoodValue = 0;
    private PointSystem pointSystemScript;

    // Start is called before the first frame update
    void Start()
    {
        // Set the initial value of the food bar to 0 and set each animal's maximum food bar
        foodSlider.value = 0;
        foodSlider.maxValue = maxFoodValue;
        // Access all the public methods and variables of the PointSystem class
        pointSystemScript = GameObject.Find("Player").GetComponent<PointSystem>();
    }

    public void IncreaseFoodBar()
    {
        // Increase the food bar of the animal that collided with the projectile
        currentFoodValue += 1;
        foodSlider.value = currentFoodValue;

        // Once the animal has a full food bar, increase the score and destroy it
        if (currentFoodValue >= maxFoodValue)
        {
            pointSystemScript.IncreaseScore();
            Destroy(gameObject, 0.1f);
        }
    }
    // Return the current value of the animal's food bar
    public float GetFoodValue()
    {
        return foodSlider.value;
    }
    // Return the value needed to fill the food bar
    public float GetFoodMaxValue()
    {
        return maxFoodValue;
    }
}
