﻿using System;
using System.Collections;
using System.Collections.Generic;

public interface ISaveAndLoad
{
    void Save(int indexToSave);
    void Load(int indexToLoad);
}


public class Player : ISaveAndLoad
{
    public int health;
    public float POSITIONx;
    public float POSITIONy;
    public float POSITIONz;

    public void Load(int LoadingIndex)
    {
        PlayerData pd = SavingMechanism.LoadData<PlayerData>(PlayerData.key, LoadingIndex);

        POSITIONx = pd.POSx;
        POSITIONy = pd.POSy;
        POSITIONz = pd.POSz;

        health = pd.life;
    }

    public void Save(int SavingIndex)
    {
        PlayerData pd = new PlayerData(POSITIONx, POSITIONy, POSITIONz, health);
        SavingMechanism.SaveData<PlayerData>(pd, SavingIndex);
    }
}

[System.Serializable]
public class PlayerData : SaveObject
{
    public static string key = "Player";

    public float POSx;
    public float POSy;
    public float POSz;

    public int life;

    public PlayerData(float x, float y, float z, int life)
    {
        POSx = x;
        POSy = y;
        POSz = z;

        this.life = life;
    }

    public override string GetKey()
    {
        return key;
    }

    public override string GetPrefabPath()
    {
        return "Path of the prefab that you want to instantiate";
    }

    // use this function only if your using unity, you can use this function along if GetPrefabPath to Instantiate all entities that you want when you're loading the game
    public override bool isInstantiatable()
    {
        return true;
    }
}
