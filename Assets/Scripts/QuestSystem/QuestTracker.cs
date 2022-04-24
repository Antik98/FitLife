using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GradingSystem;
using JetBrains.Annotations;
using UnityEngine;

public class QuestTracker : MonoBehaviour
{

    public delegate void QuestChanged(int id);
    public event EventHandler<int> HandleQuestChanged;
    public GameTimer gameTimer;

    public Quest[] quests = new Quest[] { new SchoolQuest(0, "Přednáška BI-CAO", "Běž na přednášku z číslicových a analogových obvodů v 9:15-10:45. Můžeš přijít 15 minut před a po začátku.", new TimeSpan(0,9,15,0), Quest.Type.lecture, SchoolSubjectType.CAO, 1, "CAOPrednaska"),
                                          new SchoolQuest(1, "Cvičení BI-ZMA", "Běž na cvičení ze základů matematické analýzy v 11:00. Můžeš přijít 15 minut před a po začátku.", new TimeSpan(0,11,00,0), Quest.Type.practice, SchoolSubjectType.ZMA,1,"ZMASeminar"),
                                          new SchoolQuest(2, "Test BI-PS1", "Běž na test z programování v shellu v 16:15. Můžeš přijít 15 minut před a po začátku.",new TimeSpan(0,16,15,0),  Quest.Type.exam, SchoolSubjectType.PS1, 5, "PS1_test"),
                                          new SchoolQuest(3, "Cvičení BI-MLO", "Běž na cvičení z matematické logiky v 7:30. Můžeš přijít 15 minut před a po začátku.", new TimeSpan(1,7,30,0), Quest.Type.practice, SchoolSubjectType.MLO, 1, "MLOCviko"),
                                          new SchoolQuest(4, "Proseminář BI-PA1", "Běž na proseminář z programování a algoritmizace v 9:15. Můžeš přijít 15 minut před a po začátku.", new TimeSpan(1,9,15,0), Quest.Type.proseminar, SchoolSubjectType.PA1, 1, "PA1Proseminar"),
                                          new SchoolQuest(5, "Test BI-PAI", "Běž na test z práva a informatiky v 16:15. Můžeš přijít 15 minut před a po začátku.", new TimeSpan(1,16,15,0), Quest.Type.exam, SchoolSubjectType.PAI,5, "PAI_test"),
                                          new SchoolQuest(6, "Zkouška BI-ZMA", "Běž na zkoušku ze základů matematické analýzy v 9:15. Můžeš přijít 15 minut před a po začátku.", new TimeSpan(2,9,15,0), Quest.Type.exam, SchoolSubjectType.ZMA, 4, "ZMA_MinigameStartScreen"),
                                          new SchoolQuest(7, "Zkouška BI-CAO", "Běž na zkoušku z číslicových a analogových obvodů v 11:00. Můžeš přijít 15 minut před a po začátku.", new TimeSpan(2,11,00,0), Quest.Type.exam, SchoolSubjectType.CAO, 4, "CAO_Minigame"),
                                          new SchoolQuest(8, "Zkouška BI-MLO", "Běž na zkoušku z matematické logiky v 16:15. Můžeš přijít 15 minut před a po začátku.", new TimeSpan(2,16,15,0), Quest.Type.exam, SchoolSubjectType.MLO, 4,  "MLO_zkouska"),
                                          new ProgtestQuest(9, "Progtest BI-PA1","Dodělej Progtestovou úlohu v NTK. Čas máš do 23:59 druhého dne.", new TimeSpan(1,23,59,0)),
                                          new QuestInteraction(10, "Klubovna Fit--","Najdi způsob, jak uplatit Kostu. Zkus mu přinést něco dobrého k jídlu.", "Najdi způsob, jak uplatit Kostu s Filipem. Zkus jim přinést něco dobrého k jídlu.","Dnes mají v menze gyros s tzatzikami. Gyros sníš a tzatziki si necháš zabalit sebou, třeba se budou hodit.","Quest splněn: Dal jsi Kostovi tzatziki."),
                                          new QuestInteraction(11, "Fit-- Kalba","Vrať se v 18:30 na kalbu", "Na kalbu je trochu brzo, přijdu později","Jde se pařit!","To byla paradní kalba. Nejradši bych si dal 20."),
                                          new QuestInteraction(12, "Vstupenka do NTK", "Najdi turniket na lístek do knihovny.", "Nemáš ISIC, najdi způsob, jak se dostat do knihovny.", "Získal jsi lístek do knihovny." ,"Quest splněn: Použil jsi lístek do knihovny."),
                                          new QuestInteraction(13, "Osamocený pes", "Ten pes v klubu FIT-- vypadá smutně. Kdybych mu dal něco dal, možná by si mě oblíbil.", "Najdi něco pro Fída.", "Sebral jsi kost." ,"Quest splněn: Ochočil sis Fída. Nyní ho najdeš u sebe doma."),
                                          new QuestInteraction(14, "Ztracený květináč", "Zhulenec v klubu FIT-- ztratil květináč s jeho rostlinkami. Zkus se po něm podívat.", "Najdi květináč s rostlinkami pro zhulence.", "Sebral jsi květináč s rostlinkami." ,"Quest splněn: Odevzdal jsi květináč s rostlinkami."),
                                          new QuestInteraction(15, "Ztracená skripta", "Týpek na kampusu se shání po skriptech z AAG. Zkus je najít. Prý hodně pařil.", "Najdi skripta z AAG.", "Sebral jsi skripta z AAG." ,"Quest splněn: Odevzdal jsi skripta z AAG.", deadline: new TimeSpan(1,23,59,59)),
                                          new QuestInteraction(16, "Po zavíračce", "Zaspal jsi v NTK, rychle najdi cestu ven.", "Utíkej, ať tě tady nezavřou!", "Ale neee... No tak tady asi přespím no..." ,"Quest splněn: Stihl jsi včas utéct."),
                                          new QuestInteraction(17, "Zapomenutý ISIC", "Stav se ve škole, jdi na přednášku, prohledej třetí řadu a najdi zapomenutý ISIC. Máš na to čas pouze do konce dnešního dne.", "Jdi do školy na nějakou přednášku a získej zapomenutý ISIC. Máš na to čas pouze do konce dnešního dne.", "Už na vrátnici vidíš zapomenutý ISIC a bereš ho.", "Quest splněn: Odevzdal jsi zapomenutý ISIC.", deadline: new TimeSpan(0,23,59,59)),
                                          new QuestInteraction(18, "Beer pong", "Týpek z klubu FIT-- někde ztratil ping pongový míček na beer pong. Zkus ho najít.", "Podívej se po mapách a najdi míček na beer pong.", "Sebral jsi míček na beerpong.", "Quest splněn: Odevzdal jsi míček na beer pong.")
                                        };

    

    public void SendUpdateEvent(int id)
    {
        HandleQuestChanged?.Invoke(this, id);
    }

    private void OnEnable()
    {
        StartCoroutine(OnEnableCoroutine());
    }

    IEnumerator OnEnableCoroutine()
    {
        yield return new WaitUntil(() => StatusController.initialized);
        gameTimer = StatusController.Instance.GetComponent<GameTimer>();
        StartQuestDay1();
        gameTimer.BroadcastDayPassed += HandleDayPassed;
        gameTimer.Broadcast15MinutesPassed += HandleMinuteIntervalPassed;
    }

    public void OnDisable()
    {
        gameTimer = StatusController.Instance.GetComponent<GameTimer>();
        gameTimer.BroadcastDayPassed -= HandleDayPassed;
        gameTimer.Broadcast15MinutesPassed -= HandleMinuteIntervalPassed;
    }

    private void HandleDayPassed()
    {
        switch (StatusController.Instance.GetComponent<GameTimer>().gameTime.Days)
        {
            case 1: // day 2 quests;
                StartQuestDay2();
                break;

            case 2: // day 3 quests;
                StartQuestDay3();
                break;
        }
    }

    private void HandleMinuteIntervalPassed()
    {
        CheckQuestsTimeout();
    }
    
    public void StartQuestDay1()
    {
        AcceptQuest(0);
        AcceptQuest(1);
        AcceptQuest(2);
        AcceptQuest(9);
    }

    public void StartQuestDay2()
    {
        AcceptQuest(3);
        AcceptQuest(4);
        AcceptQuest(5);
    }

    public void StartQuestDay3()
    {
        AcceptQuest(6);
        AcceptQuest(7);
        AcceptQuest(8);
    }

    public void AcceptQuest( int num )
    {
        quests[num].AcceptQuest();
        SendUpdateEvent(num);
    }

    public void TurnInQuest(int num) //TODO
    {
        quests[num].TurnInQuest();
        SendUpdateEvent(num);
    }

    public void CompleteQuest( int num )
    {
        var quest = quests[num];
        quest.CompleteQuest();
        SendUpdateEvent(num);
    }

    public void FailQuest( int num )
    {
        quests[num].FailQuest();
        SendUpdateEvent(num);
    }

    public Quest getQuest( int num )
    {
        return quests[num];
    }

    public List<Quest> getQuests()
    {
        return quests.ToList();
    }

    public List<Quest> getActiveQuests()
    {
        return quests.Where(q => q.status == Quest.Status.progress).ToList();
    }

    public int getQuestsDone()
    {
        int ret = 0;
        foreach(Quest q in quests)
        {
            if (q.status == Quest.Status.completed)
                ret++;
        }
        return ret;
    }

    public void CheckQuestsTimeout()
    {

        foreach(Quest q in quests)
        {
            if (q.status == Quest.Status.progress && q.IsQuestTimedOut(gameTimer.gameTime))
            {
                FailQuest(q.questID);
            }
        }
    }

    public void Reset() // for game restart
    {
        foreach (Quest q in quests)
            q.status = Quest.Status.inactive;

        StartQuestDay1();
    }
}
