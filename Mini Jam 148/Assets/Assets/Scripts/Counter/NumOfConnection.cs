using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumOfConnection 
{
    //Fields
    private int currentConnection;
    private int MaxConnection;
    //properties
    public int Connection
    {
        get
        {
            return currentConnection;
        }
        set
        {
            currentConnection = value;
        }
    }
    public int currentMaxConnection
    {
        get
        {
            return MaxConnection;
        }
        set
        {
            MaxConnection = value;
        }
    }
    
    // Constructor
    public NumOfConnection(int curConnection, int maxcon)
    {
        currentConnection = curConnection;
        MaxConnection = maxcon;
    }
    
    //Methods
    public void ConnectionCost(int costCount)
    {
        if (currentConnection > 0)
        {
            currentConnection -= costCount;
        }
    }

    public void ConnectionRestore(int restore)
    {
        if (currentConnection < MaxConnection)
        {
            currentConnection += restore;
        }
        if (currentConnection > MaxConnection)
        {
            currentConnection = MaxConnection;
        }
    }
}
