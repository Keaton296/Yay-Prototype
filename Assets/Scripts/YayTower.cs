using System;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(YayMeshManager))]
public class YayTower : MonoBehaviour
{
    List<Yay> yays;
    public List<Yay> Yays { get { return yays; } }
    public YayMeshManager yayMeshManager;
    public Action OnInitialize;
    void Start()
    {
        Initialize();
    }
    void Initialize()
    {
        yayMeshManager = GetComponent<YayMeshManager>();
        yays = new List<Yay>();
        StartRandom();
        OnInitialize?.Invoke();
    }
    void StartRandom(){
        System.Random generator = new System.Random();
        PutOnTop(new Yay(1, (YayColor)generator.Next(0,3)));
        PutOnTop(new Yay(1, (YayColor)generator.Next(0, 3)));
        PutOnTop(new Yay(1, (YayColor)generator.Next(0, 3)));
        PutOnTop(new Yay(1, (YayColor)generator.Next(0, 3)));
        PutOnTop(new Yay(1, (YayColor)generator.Next(0, 3)));
    }
    public void PutOnTop(Yay yay)
    {
        if(yay != null && yay.height > 0)
        {
            if (yays.Count > 0 && yays[yays.Count - 1].color == yay.color)
            {       
                yays[yays.Count - 1].height += yay.height;
                return;
            }   
            yays.Add(yay);
        }
    }
    public Yay TakeFromTop()
    {
        if (yays.Count > 0)
        {
            Yay result = yays[yays.Count - 1];
            yays.Remove(result);
            return result;
        }
        else return null;
    }
    public Yay GetLastYay()
    {
        if (yays.Count == 0)
        {
            return null;
        }
        else 
        {
            return yays[yays.Count - 1];
        }
        
    }
    public int GetYayCount()
    {
        return yays.Count;
    }
}
