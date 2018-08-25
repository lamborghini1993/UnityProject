using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalVar
{
    private static GlobalVar m_instance;
    private float m_mapX, m_mapY;

    public float MapX
    {
        get
        {
            return m_mapX;
        }

        set
        {
            m_mapX = value;
        }
    }

    public float MapY
    {
        get
        {
            return m_mapY;
        }

        set
        {
            m_mapY = value;
        }
    }

    private GlobalVar() { }
    public static GlobalVar Instance
    {
        get
        {
            if(m_instance == null)
                m_instance = new GlobalVar();
            return m_instance;
        }
        
    }
    
  
}
