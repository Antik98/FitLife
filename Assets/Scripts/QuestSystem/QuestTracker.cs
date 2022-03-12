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

    public Quest[] quests = new Quest[] { new SchoolQuest(0, "Přednáška BI-CAO", "Běž na přednášku z číslicových a analogových obvodů v 9:15.", new TimeSpan(0,9,15,0), Quest.Type.lecture, SchoolSubjectType.CAO, 1, "CAOPrednaska"),
                                          new SchoolQuest(1, "Cvičení BI-ZMA", "Běž na cvičení ze základů matematické analýzy v 11:00.", new TimeSpan(0,11,00,0), Quest.Type.practice, SchoolSubjectType.ZMA,1,"ZMASeminar"),
                                          new SchoolQuest(2, "Test BI-PS1", "Běž na test z programování v shellu v 16:15.",new TimeSpan(0,16,15,0),  Quest.Type.exam, SchoolSubjectType.PS1, 5, ""),
                                          new SchoolQuest(3, "Cvičení BI-MLO", "Běž na cvičení z matematické logiky v 7:30.", new TimeSpan(1,7,30,0), Quest.Type.practice, SchoolSubjectType.MLO, 1, "MLOCviko"),
                                          new SchoolQuest(4, "Proseminář BI-PA1", "Běž na proseminář z programování a algoritmizace v 9:15.", new TimeSpan(1,9,15,0), Quest.Type.proseminar, SchoolSubjectType.PA1, 1, "PA1Proseminar"),
                                          new SchoolQuest(5, "Test BI-PAI", "Běž na test z práva a informatiky v 16:15.", new TimeSpan(1,16,15,0), Quest.Type.exam, SchoolSubjectType.PAI,5, ""),
                                          new SchoolQuest(6, "Zkouška BI-ZMA", "Běž na zkoušku ze základů matematické analýzy v 9:15.", new TimeSpan(2,9,15,0), Quest.Type.exam, SchoolSubjectType.ZMA, 4, "ZMA_MinigameStartScreen"),
                                          new SchoolQuest(7, "Zkouška BI-CAO", "Běž na zkoušku z číslicových a analogových obvodů v 11:00.", new TimeSpan(2,11,00,0), Quest.Type.exam, SchoolSubjectType.CAO, 4, "CAO_Minigame"),
                                          new SchoolQuest(8, "Zkouška BI-MLO", "Běž na zkoušku z matematické logiky v 16:15.", new TimeSpan(2,16,15,0), Quest.Type.exam, SchoolSubjectType.MLO, 4,  "MLO_zkouska"),
                                          new ProgtestQuest(9, "Progtest BI-PA1","Dodělej Progtestovou úlohu v NTK do 23:59 druhého dne.", new TimeSpan(1,23,59,0)),
                                          new QuestInteraction(10, "Klubovna Fit--","Kup v menze tzatziki pro Kostu.", "Hmm, možná bych ho mohl nějak podplatit...","Nesmím zapomenout Kostovi koupit ty tzatziki.","Dal jsi Kostovi tzatziki."),
                                          new QuestInteraction(11, "Fit-- Kalba","Vrať se v 18:30 na kalbu", "Na kalbu je trochu brzo, přijdu později","Jde se pařit!","To byla paradní kalba. Nejradši bych si dal 20."),
                                          new QuestInteraction(12, "Vstupenka do NTK", "Najdi turniket na lístek do knihovny.", "Nemáš ISIC, najdi způsob, jak se dostat do knihovny.", "Eh, jak se to... A hele, vytisklo mi to lístek." ,"Tak, jde se na Progtest."),
                                          new QuestInteraction(13, "Osamocený pes", "Ten pes v klubu FIT-- vypadá smutně. Kdybych mu dal něco dal, možná by si mě oblíbil.", "Najdi něco pro Fída.", "Hmm, tahle kost by se Fídovi mohla líbit." ,"Fído, koukej, co pro tebe mám!"),
                                          new QuestInteraction(14, "Ztracený květináč", "Zhulenec v klubu FIT-- ztratil květináč s jeho rostlinkami. Zkus se po něm podívat.", "Najdi květináč pro zhulence.", "Oo, to je vůně, to se zhulencovi bude líbit." ,"Čus, koukej, tohle ti zlepší náladu."),
                                          new QuestInteraction(15, "Ztracená skripta", "Týpek na kampusu se shání po skriptech z AAG. Zkus je najít. Prý hodně pařil.", "Najdi skripta z AAG.", "Hele, skripta z automatů a gramatik, to po mně někdo chtěl ne?" ,"Čau, mám pro tebe ta skripta!"),
                                          new QuestInteraction(16, "NTK Uzavírka", "Zaspal jsi v NTK, stihni to ven!", "Sakra no, tak tady asi přespím...", "Utíkej ať tě tady nezavřou!" ,"Uff, stihl jsem to"),
                                        };

    

    public void SendUpdateEvent(int id)
    {
        HandleQuestChanged?.Invoke(this, id);
    }


    public void Start()
    {
        gameTimer = StatusController.Instance.GetComponent<GameTimer>();
        StartQuestDay1();
        gameTimer.BroadcastDayPassed += HandleDayPassed;
        gameTimer.Broadcast15MinutesPassed += Handle15MinuteIntervalPassed;
    }

    public void OnDisable()
    {
        gameTimer = StatusController.Instance.GetComponent<GameTimer>();
        gameTimer.BroadcastDayPassed -= HandleDayPassed;
        gameTimer.Broadcast15MinutesPassed -= Handle15MinuteIntervalPassed;
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

    private void Handle15MinuteIntervalPassed()
    {
        CheckQuestsTimeout();
    }
    
    public void StartQuestDay1()
    {
        AcceptQuest(0);
        AcceptQuest(1);
        AcceptQuest(2);
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
