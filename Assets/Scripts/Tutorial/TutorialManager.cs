using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class TutorialManager : MonoBehaviour
{
    public GameObject pointer;
    public RectTransform pointerManagerRectTransform;
    public GameObject[] interactions;
    public RectTransform[] pointerLocations;
    public PopUpMessage popUpMessage;
    public TextMeshProUGUI continueText;

    private int popUpIndex = 0;
    public GameObject player;
    public PhoneDisplay phone;
    private GameObject fridge;
    private GameTimer gameTimer;
    private bool shouldDoTutorial;


    string[] TextDatabase = new string[]
    {
        "Ahoj, vítáme tě u naší hry, která se ti pokusí přiblížit život studentů na FITu, fakultě informatiky ČVUT. Tvým úkolem bude vyzkoušet si poslední tři dny prvního semestru našeho studia.",
        "Začneme jednoduchým tutoriálem. Pohyb je jako všude pomocí <b>WASD</b>, popřípadě šipek, tak si to zkus.",
        "Super, další klávesou, kterou při hraní využiješ, je klávesa <b>E</b>, sloužící k interakci. Zkus si vzít něco z ledničky.",
        "Krom podezřele zapáchajícího guláše a týden staré pizzy z polotovaru toho v ledničce moc není. Další klávesou, kterou při hraní využiješ, je <b>M</b>. Zmáčkni <b>M</b> a sleduj, co se stane.",
        "Asi víš, k čemu normálně slouží mobil, ale na tomhle typu si Instagram nebo Tinder neotevřeš. Místo toho ti naopak bude důležitým pomocníkem.",
        "První důležitou herní mechanikou je čas. Stejně jako v životě, tak ani ve hře ho nemáš neomezený. Dej si pozor a snaž se stihnout všechny přednášky včas a nezapomeň být před půlnocí zpátky doma v posteli.",
        "Tohle je jeden ze tří atributů, o který se musíš starat. Energie. Čím víc se budeš učit, tím víc ti bude klesat. Nenech jí klesnout na 0, to nebude mít dobrý důsledek! Dobiješ ji spánkem nebo kávou v menze či kavárně.",
        "Nasycenost je tvůj druhý atribut. Zvyšuješ jí samozřejmě jídlem. Můžeš zkusit ten zbytek pizzy v ledničce, nebo radši navštiv místní menzu. Pokud nevíš, kde je, zkus se někoho zeptat. Také jí nenech klesnout na 0.",
        "Poslední atribut je sociální status. Najdi si nové kamarády, choď na párty, bal holky, bav se. Jen tak ho udržíš vysoko.",
        "Vykřičníček ti napovídá, že existuje menu úkolů. Každé ráno dostaneš nové úkoly dle tvého rozvrhu. Po skončení tutoriálu se hned na ty dnešní pomocí klávesy <b>Q</b> podívej. Také si pomocí klávesy <b>TAB</b> můžeš během hry zobrazit časově nejbližší úkoly. Mobil zavřeš opět stisknutím <b>M</b>.",
        "Nyní už víš veškeré potřebné základy. Tvým cílem je přežít následující 3 dny. Jak to uděláš, záleží jen a jen na tobě. Hodně štěstí!"
    };

    string[] ContinueDatabase = new string[]
    {
        "Pro pokračování stiskni mezerník",
        "Zkus se pohnout",
        "Prozkoumej ledničku",
        "Otevři mobil stisknutím M",
        "Zavři mobil stisknutím M"
    };

    const int TAB_END = 4;
    const int DEFAULT_CONTINUE = 0;

    void Start()
    {
        fridge = GameObject.Find("FridgeInteractive");
        fridge.SetActive(false);
        gameTimer = GameObject.FindGameObjectWithTag("StatusController").GetComponent<GameTimer>();
        shouldDoTutorial = StatusController.Instance.GetComponent<PlayerStatus>().doTutorial;
        if (shouldDoTutorial)
        {
            pointer.SetActive(false);
            lockInteraction();
            lockPlayer();
            popUpMessage.Open(new Dialogue("[ME]AAh, to už je ráno? Asi mám ještě kocovi... Moment, jak se sem dostal ten bazén?"));
        }
    }

    private void Update()
    {
        if (shouldDoTutorial)
        {
            DoTutorial();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void DoTutorial()
    {
        phone.SetPhoneState(canOpenPhone());

        if (popUpMessage.isActive())
            return;
        DisplayPointer();
        DisplayText();
        progress();

        if (popUpIndex >= TextDatabase.Length)
        {
            StatusController.Instance.GetComponent<PlayerStatus>().doTutorial = false;
            unlockInteraction();
            gameTimer.StartTimer();
            Destroy(gameObject); //destroys this object and no tutorial is called anymore
            return;
        }
    }

    private void DisplayPointer()
    {
        if (popUpIndex >= 5 && popUpIndex <= 9)
        {
            pointer.SetActive(true);
            pointerManagerRectTransform.position = pointerLocations[popUpIndex - 5].position;
        }
        else
            pointer.SetActive(false);
    }

    private void DisplayText()
    {
        popUpMessage.Open(new Dialogue(TextDatabase[popUpIndex]));

        if (popUpIndex < 4)
        {
            continueText.text = ContinueDatabase[popUpIndex];
        }
        else if (popUpIndex == 9)
        {
            continueText.text = ContinueDatabase[TAB_END];
        }
        else
        {
            continueText.text = ContinueDatabase[DEFAULT_CONTINUE];
        }
    }

    private void progress()
    {
        if (popUpIndex == 1)
        {
            unlockPlayer();
            popUpMessage.dismissFunc = (() =>  IsPressedKeyOrMouse (KeyCode.W, KeyCode.A, KeyCode.S, KeyCode.D,
                KeyCode.UpArrow, KeyCode.LeftArrow, KeyCode.DownArrow, KeyCode.RightArrow));
            fridge.SetActive(true);
        }
        else if (popUpIndex == 2)
        {
            unlockPlayer();
            popUpMessage.dismissFunc = (() => fridge.GetComponent<FridgeInteract>().firstEnter && IsPressedKeyOrMouse(KeyCode.E));
        }
        else if (popUpIndex == 3)
        {
            popUpMessage.dismissFunc = (() => Input.GetKeyDown(KeyCode.M));
        }
        else if (popUpIndex >= 4 && popUpIndex <= 8)
        {
            popUpMessage.dismissFunc = null;
        }
        else if (popUpIndex == 9)
        {
            popUpMessage.dismissFunc = () => IsPressedKeyOrMouse(KeyCode.M) || (GameObject.FindGameObjectWithTag("UI_Quests")?.GetComponent<QuestDisplay>()?.isActive() ?? false);// if player opens quest menu proceed
        }
        else if (popUpIndex == 10)
        {
            phone.SetPhoneState(false);
            popUpMessage.dismissFunc = null;
        }
        popUpIndex++;
    }
    
    public bool canOpenPhone()
    {
        if (popUpIndex >= 5 && popUpIndex <= 10)
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