using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeDmgRedSCreen : MonoBehaviour
{
    public bool takenShieldDamage, takenLifeDamage;
    private bool startedShieldDmg, startedLifeDmg;
    public GameObject takeDmgRedWindow;
    public GameObject takeDmgBlueWindow;
    [SerializeField] private CanvasGroup redWinCanvGroup,blueWinCanvGroup;
    // Start is called before the first frame update
    void Start()
    {
        redWinCanvGroup = takeDmgRedWindow.GetComponent<CanvasGroup>();
        blueWinCanvGroup = takeDmgBlueWindow.GetComponent<CanvasGroup>();
    }

    // Update is called once per frame
    void Update()
    {
        if (takenShieldDamage)
        {
            if (!startedShieldDmg)
            {
                startedShieldDmg = true;
                StartCoroutine(StartTakeDmgBlueWindow());
            }
        }
        if (takenLifeDamage)
        {
            if (!startedLifeDmg)
            {
                startedLifeDmg = true;
                StartCoroutine(StartTakeDmgRedWindow());
            }
        }
    }

    private IEnumerator StartTakeDmgRedWindow()
    {
        if (redWinCanvGroup.alpha < 1f)
        {
            redWinCanvGroup.alpha += 1f * Time.deltaTime;
        }
        yield return new WaitForSeconds(2.5f);
        if (redWinCanvGroup.alpha > 0f)
        {
            redWinCanvGroup.alpha -= 0.1f * Time.deltaTime;
        }
        yield return new WaitForSeconds(2.5f);
        redWinCanvGroup.alpha = 0f;
        takenLifeDamage = false;
        startedLifeDmg = false;

    }
    private IEnumerator StartTakeDmgBlueWindow()
    {
        if (blueWinCanvGroup.alpha < 1f)
        {
            blueWinCanvGroup.alpha += 1f * Time.deltaTime;
        }
        yield return new WaitForSeconds(2.5f);
        if (blueWinCanvGroup.alpha > 0f)
        {
            blueWinCanvGroup.alpha -= 0.1f * Time.deltaTime;
        }
        yield return new WaitForSeconds(2.5f);
        blueWinCanvGroup.alpha = 0f;
        takenShieldDamage = false;
        startedShieldDmg = false;
    }
}
