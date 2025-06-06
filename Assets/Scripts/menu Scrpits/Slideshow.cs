using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
// All the Timer stuff is optional
public class Slideshow : MonoBehaviour
{
    public Image image;
    public Sprite[] slides;
    private int currentSlide;
    public TextMeshProUGUI slideNumber;
 /*   public float timer = 5.0f;
    public float timerRemaining = 5.0f;
    public bool timerIsRunning = true;
*/
    void OnGUI()
    {
        if(currentSlide >= slides.Length)
        {
            currentSlide = 0;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        currentSlide = 0;
        image.sprite = slides[currentSlide];
    //    bool timerIsRunning = false;
   //     timerRemaining = timer;
    }

    // Update is called once per frame
    void Update()
    {
        slideNumber.text = (currentSlide + 1)  + "/" + slides.Length;
        image.sprite = slides[currentSlide];
      /*  if (timerIsRunning)
        {
            if (timerRemaining > 0)
            {
                timerRemaining -= Time.deltaTime;
            }
            else
            {
                UnityEngine.Debug.Log("Timer Has Finished");
                timerRemaining = timer;
                currentSlide++;
            if (currentSlide >= slides.Length)
            {
                currentSlide = 0;
            }

            }
        }
        */

        if (Input.GetMouseButtonDown(0))
        {
            currentSlide++;
            if (currentSlide >= slides.Length)
            {
                currentSlide = 0;
            }
        }

    }
}
