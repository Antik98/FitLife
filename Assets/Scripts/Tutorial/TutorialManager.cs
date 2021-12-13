using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Zenject;

public class TutorialManager : MonoBehaviour
{
    public GameObject pointer;
    public GameObject[] interactions;
    private int popUpIndex = 0;
    public GameObject player;
    private GameObject fridge;
    private GameTimer gameTimer;

    private bool startTutorial = false;

    private Text TutorialText;
    private Text ContinueBox;
    public GameObject TutorialBox;
    private Vector2 defaultPositionPointer;


    string[] TextDatabase = new string[]
    {
        "Ahoj, vítáme tě u naší hry, která se ti pokusí přiblížit život studentů na FITu, fakultě informatiky ČVUT. Tvým úkolem bude vyzkoušet si poslední tři dny našeho studia prvního semestru.",
        "Začneme jednoduchým tutoriálem. Pohyb je jako všude pomocí WASD, popřípadě šipek, tak si to zkus.",
        "Skvěle, Další klávesou, kterou při hraní využiješ, je klávesa E, sloužící k interakci. Zkus si vzít něco z ledničky.",
        "Krom podezřele zapáchajícího guláše a týden staré pizzy z polotovaru toho v ledničce moc není. Poslední klávesou, kterou při hraní využiješ, je TAB. Zmáčkni TAB a sleduj, co se stane.",
        "Seznam se s věcí, jež ničí všechny moderní vztahy a díky níž nám utíká život mezi prsty. Tento mobil ti ale naopak bude důležitým pomocníkem.",
        "První důležitá věc je čas. Stejně jako v životě, ani ve hře ho nemáš neomezený. Na každý herní den jej máš vymezený, tak si dej pozor, aby jsi stihnul všechny přednášky a byl včas zpátky doma.",
        "Tohle je jeden ze tří atributů, o který se musíš starat. Energie. Čím víc budeš pařit nebo se učit, tím víc budeš unavený. Nenech ho klesnout na 0, to nebude mít dobrý důsledek! Dobiješ ho spánkem, nebo kávou v menze.",
        "Nasycenost je tvůj druhý atribut. Zvyšuješ jí samozřejmě jídlem. Můžeš zkusit ten zbytek pizzy v ledničce, nebo navštiv místní menzu. Pokud nevíš, kde je, zkus se někoho zeptat. Také jí nenech klesnout na 0.",
        "Poslední atribut je sociální status. Najdi si nové kamarády, choď na párty, bal holky, bav se. Jen tak ho udržíš vysoko.",
        "Kliknutím na vykřičníček si zobrazíš menu úkolů. Teď to nedělej, žádné úkoly stejně nemáš. U úkolů si vždy pečlivě přečti jejich popis a hlídej si časy přednášek. Telefon zavřeš opět TABem.",
        "Nyní už víš veškeré potřebné základy. Záleží už jen a jen na tobě, jestli dáš přednost pařbám před učením, nebo se vydáš cestou za červeným diplomem. Hodně štěstí!"
    };

    string[] ContinueDatabase = new string[]
    {
        "Pro pokračování stiskni mezerník",
        "Zkus se pohnout",
        "Prozkoumej ledničku",
        "Otevři mobil TABem",
        "Zavři mobil TABem"
    };

    const int TAB_END = 4;
    const int DEFAULT_CONTINUE = 0;

    void Start()
    {
        fridge = GameObject.Find("FridgeInteractive");
        gameTimer = GameObject.FindGameObjectWithTag("StatusController").GetComponent<GameTimer>();

        if (SceneController.prevScene == "MainMenu")
        {
            lockPlayer();
        }
        else
        {
            popUpIndex = 11;
        }
    }

    void Update()
    {
        if (!startTutorial)
        {
            CheckMorning();
        }
        else
        {
            DoTutorial();
        }
    }



    private void CheckMorning()
    {
        if (!GetComponent<HomeMorningEvent>().isFinished)
            return;

        startTutorial = true;

        lockPlayer();
        gameTimer.StopTimer();
        lockInteraction();

        TutorialBox.SetActive(true);
        TutorialText = GameObject.FindGameObjectWithTag("TutorialText").GetComponent<Text>();
        ContinueBox = GameObject.FindGameObjectWithTag("ContinueText").GetComponent<Text>();

        DisplayText();
    }

    private void DoTutorial()
    {
        if (!GetAction())
            return;

        popUpIndex++;

        if (popUpIndex >= TextDatabase.Length)
        {
            unlockInteraction();
            gameTimer.StartTimer();
            Destroy(gameObject); //destroys this object and no tutorial is called anymore
            return;
        }

        DisplayText();
        DisplayPointer();
    }

    private void DisplayPointer()
    {

        switch (popUpIndex)
        {
            case 5:
                defaultPositionPointer = pointer.transform.position;
                break;
            case 6:
                pointer.transform.position = defaultPositionPointer + new Vector2(0, -0.8f);
                break;
            case 7:
                pointer.transform.position = defaultPositionPointer + new Vector2(0, -1.2f);
                break;
            case 8:
                pointer.transform.position = defaultPositionPointer + new Vector2(0, -1.6f);
                break;
            case 9:
                pointer.transform.position = defaultPositionPointer + new Vector2(0, -0.3f);
                break;
            default:
                pointer.SetActive(false);
                return;
        }

        pointer.SetActive(true);
    }

    private void DisplayText()
    {
        TutorialText.text = TextDatabase[popUpIndex];

        if (popUpIndex < 4)
        {
            ContinueBox.text = ContinueDatabase[popUpIndex];
        }
        else if (popUpIndex == 9)
        {
            ContinueBox.text = ContinueDatabase[TAB_END];
        }
        else
        {
            ContinueBox.text = ContinueDatabase[DEFAULT_CONTINUE];
        }
    }

    private bool GetAction()
    {
        if (popUpIndex == 0)
        {
            return IsPressedKeyOrMouse(KeyCode.Space);
        }
        else if (popUpIndex == 1)
        {
            if (IsPressedKeyOrMouse(KeyCode.W, KeyCode.A, KeyCode.S, KeyCode.D,
                KeyCode.UpArrow, KeyCode.LeftArrow, KeyCode.DownArrow, KeyCode.RightArrow))
            {
                unlockPlayer();
                return true;
            }
        }
        else if (popUpIndex == 2)
        {
            return fridge.GetComponent<FridgeInteract>().firstEnter && IsPressedKeyOrMouse(KeyCode.E);
        }
        else if (popUpIndex == 3)
        {
            lockPlayer();
            if (IsPressedKeyOrMouse(KeyCode.Tab))
            {
                return true;
            }
            return false;
        }
        else if (popUpIndex >= 4 && popUpIndex <= 8)
        {
            if(!GameObject.FindGameObjectWithTag("UI").GetComponent<PhoneDisplay>().isActive())
                GameObject.FindGameObjectWithTag("UI").GetComponent<PhoneDisplay>().OpenPhone();

            return IsPressedKeyOrMouse(KeyCode.Space);
        }
        else if (popUpIndex == 9)
        {
            if (IsPressedKeyOrMouse(KeyCode.Tab) || (GameObject.FindGameObjectWithTag("UI_Quests") && GameObject.FindGameObjectWithTag("UI_Quests").activeSelf == true))// if player opens quest menu proceed
            {
                return true;
            }
            return false;
        }
        else if (popUpIndex == 10)
        {
            if (GameObject.FindGameObjectWithTag("UI").GetComponent<PhoneDisplay>().isActive())
                GameObject.FindGameObjectWithTag("UI").GetComponent<PhoneDisplay>().ClosePhone();
            // the end of tutorial
            unlockPlayer();
            return IsPressedKeyOrMouse(KeyCode.Space);
        }

        unlockPlayer();
        return false;
    }
    
    public bool canOpenPhone()
    {
        if (popUpIndex == 3 || popUpIndex == 9 || popUpIndex > 10)
            return true;
        return false;
    }

    private void lockPlayer()
    {
        player.GetComponent<playerMovement>().lockPlayer();
    }

    private void unlockPlayer()
    {
        player.GetComponent<playerMovement>().unlockPlayer();
    }

    private void lockInteraction()
    {
        for (int i = 0; i < interactions.Length; i++)
            interactions[i].SetActive(false);
    }

    private void unlockInteraction()
    {
        for (int i = 0; i < interactions.Length; i++)
            interactions[i].SetActive(true);
    }
    
    private   bool IsPressedKeyOrMouse(params  KeyCode [] keys)
    {
        var res =  keys.Any(k => Input.GetKeyDown(k)) ;
        return res; 
    }
}