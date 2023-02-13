using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Text;
using UnityEngine.UI;

public class PageSwitchAnimation : MonoBehaviour
{
    public string alphabet = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
    public string numbers = "0123456789";
    public string specialCharacters = "{}:',./?[]()*&%#!%-=+_ ";

    public List<char> allCharacters;

    [TextArea(20,30)]
    public string toEnumerate;

    public bool enumerate;

    public TextMeshProUGUI textElement;
    public Image background;

    public int iterationsToWait;
    public int totalIterationsDEBUG;

    public bool Active { get; set; }

    public delegate void OnPageSwitchFinished();

    public OnPageSwitchFinished onPageSwitchFinished;

    private void Start()
    {
        allCharacters.AddRange(alphabet.ToCharArray());
        allCharacters.AddRange(numbers.ToCharArray());
        allCharacters.AddRange(specialCharacters.ToCharArray());
    }

    private void Update()
    {
        if (enumerate && Active == false)
        {
            enumerate = false;
            StartCoroutine(StartEnumeration(toEnumerate));
        }
    }

    IEnumerator StartEnumeration(string toEnumerate)
    {
        if (Active) yield break;

        textElement.gameObject.SetActive(true);
        background.color = new Color(0, 0, 0, 1);

        Active = true;

        bool completed = false;

        int waitIterations = 0;

        int currentCharacter = 0;

        var sb = new StringBuilder();
        while (completed == false)
        {
            if (currentCharacter >= toEnumerate.Length)
            {
                completed = true;
                break;
            }

            char current = toEnumerate[currentCharacter];

            sb.Append("1");
            for (int i = 0; i < allCharacters.Count; i++)
            {
                totalIterationsDEBUG++;

                waitIterations++;

                sb[currentCharacter] = allCharacters[i];

                if (allCharacters[i] == current)
                {
                    textElement.text = sb.ToString();
                    break;
                }

                if(waitIterations >= this.iterationsToWait)
                {
                    waitIterations = 0;

                    textElement.text = sb.ToString();
                    yield return null;
                }

            }

            currentCharacter++;
        }

        Active = false;

        textElement.gameObject.SetActive(false);

        textElement.text = " ";
        background.color = new Color(0, 0, 0, 0);
        onPageSwitchFinished?.Invoke();
    }

    public void Enable()
    {
        StartCoroutine(StartEnumeration(toEnumerate));
    }
}
