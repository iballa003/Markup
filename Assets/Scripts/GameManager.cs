using System.Collections;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    public Slot[] slots; // Referencia a todos los slots en la escena
    public Button levelButton;
    private Button checkButton;
    private Button nextButton;
    public static GameManager instance;
    public bool winLevel;
    private int levelNumber;
    void Awake()
    {
        winLevel = false;
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        updateLevel();
        checkButton = GameObject.FindWithTag("CheckButton").GetComponent<Button>();
        nextButton = GameObject.FindWithTag("NextButton").GetComponent<Button>();
    }

    public void PressCheckButton()
    {
        checkButton.interactable = false;
        StartCoroutine(CheckAllSlots());
    }

    public void PressNextButton()
    {
        updateLevel();
        winLevel = false;
    }
    public void updateLevel()
    {
        levelNumber += 1;
        levelButton.GetComponentInChildren<TextMeshProUGUI>().text = "Nivel: "+ levelNumber;
    }
    IEnumerator CheckAllSlots()
    {
        bool isCorrect = true;

        foreach (Slot slot in slots)
        {
            // Verifica si hay un objeto en el slot
            Debug.Log("El slot " + slot.name + " está vacío.");
            if (slot.GetCurrentElement() == null)
            {
                isCorrect = false;
                Debug.Log("El slot " + slot.name + " está vacío.");
                break;
            }

            // Obtiene el objeto arrastrado en el slot
            DraggableUI draggable = slot.GetCurrentElement().GetComponent<DraggableUI>();

            if (draggable == null || draggable.GetButtonText() != slot.expectedTag)
            {
                isCorrect = false;
                Debug.Log("El objeto en " + slot.name + " no es el correcto.");
                break;
            }
        }

        if (isCorrect)
        {
            AudioManager.instance.PlayCorrect();
            winLevel = true;
            Debug.Log("¡Orden correcto!");
            ChangeButtonColor(Color.green);
            checkButton.GetComponentInChildren<TextMeshProUGUI>().text = "Correcto";
            nextButton.interactable = true;
        }
        else
        {
            //AudioManager.instance.PlayBuzzer();
            Debug.Log("Orden incorrecto. Inténtalo de nuevo.");
            ChangeButtonColor(Color.red);
            checkButton.GetComponentInChildren<TextMeshProUGUI>().text = "Incorrecto";
            yield return new WaitForSeconds(2f);
            ChangeButtonColor(Color.white);
            checkButton.GetComponentInChildren<TextMeshProUGUI>().text = "Comprobar";
            checkButton.interactable = true;
        }
    }
    public void ChangeButtonColor(Color newColor)
    {
        checkButton.GetComponent<Image>().color = newColor;
        
    }
}