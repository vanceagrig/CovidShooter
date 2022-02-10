using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AvailableSkillPointsReminder : MonoBehaviour
{
    public float flashInterval;
    public Text textToFlash;
    private string textString;
    public bool flashText;
    private bool flashed;
    // Start is called before the first frame update
    void Start()
    {
        textString = "Skill points Available" + "\n" + "Press "+FindObjectOfType<UiShortcuts>().skillsMenuKey;
    }

    // Update is called once per frame
    void Update()
    {
        if (FindObjectOfType<PlayerStats>().availableSkillPoints>0)
        {
            flashText = true;
        }
        else if (FindObjectOfType<PlayerStats>().availableSkillPoints == 0)
        {
            flashText = false;
            textToFlash.text = "";
        }
        if (flashText)
        {
            if(!flashed)
            {
                flashed = true;
                StartCoroutine(FlashText());
            }
        }
        
    }
    private IEnumerator FlashText()
    {
        
        textToFlash.text = textString;
        yield return new WaitForSeconds(flashInterval);
        textToFlash.text = "";
        yield return new WaitForSeconds(flashInterval);
        flashed = false;
    }
}
