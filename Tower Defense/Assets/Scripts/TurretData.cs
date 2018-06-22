using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TurretData
{
    public GameObject turretPerfab, turrentUpgradePerfab;
    public int cost, costUpgrade;
	public TurretType type;
}



public enum TurretType
{
    LasserTurret,
    MissileTurret,
    StandardTurret,
}