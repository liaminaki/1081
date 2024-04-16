using UnityEngine;
using UnityEngine.UI;

public class StarsProgressController : MonoBehaviour
{
    public Image timer_linear_image;
    public float maxTime;
    public int timeFor3Stars, timeFor2Stars;
    public Animator thirdStar, secondStar, firstStar;

    private float timeRemaining;
    public int possibleStars = 3;

    void Start()
    {
        timeRemaining = maxTime;
    }

    void Update()
    {
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            timer_linear_image.fillAmount = timeRemaining / maxTime;

            if (possibleStars == 3 && maxTime - timeRemaining >= timeFor3Stars)
            {
                thirdStar.SetTrigger("shrinkStar");
                possibleStars--;
            }
            else if (possibleStars == 2 && maxTime - timeRemaining >= timeFor2Stars)
            {
                secondStar.SetTrigger("shrinkStar");
                possibleStars--;
            }
        }
    }
}
