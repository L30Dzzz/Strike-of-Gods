using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

//[CreateAssetMenu(fileName = "MenuScreen", menuName = "ScriptableObjects/MenuScreenChanger", order = 3)]
public class MenuScreenChanger : MonoBehaviour
{ 
    private Button ScreenChanger1;
    public TextMeshProUGUI ScreenUno;
    public TextMeshProUGUI ScreenDuo;
    public bool isButtonWorking = true;
    private AudioSource buttonSound;
    public AudioClip errorSound;
    public AudioClip selectSound;

    
    // Start is called before the first frame update
    void Awake()
    {
       ScreenChanger1 = GetComponent<Button>();

       if(ScreenChanger1 != null)
       {
            ScreenChanger1.onClick.AddListener(ScreenChange);
       }

        
    }

    void Start()
    {
        buttonSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ScreenChange()
    {
        Debug.Log("here");
        if (isButtonWorking == true)
        {
             
         ScreenDuo.gameObject.SetActive(true);
         ScreenUno.gameObject.SetActive(false);
         //buttonSound.PlayOneShot(selectSound, 1.5f);
        }
        /*
        else
        {
           buttonSound.PlayOneShot(errorSound, 3.0f); 
        }
        */
    }
}
