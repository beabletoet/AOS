﻿using UnityEngine;
using System.Collections;

public class ExampleUnit : MonoBehaviour
{
    private Revealer _Revealer;
    public float VisionRange = 6f;
    public FogOfWar.Players Faction = FogOfWar.Players.Player00;
    [Range(0, 255)]
    public int UpVision = 10;
    public MeshRenderer meshRenderer;
    public float TurnOffDelay = 0.0f;

    public void OnEnable()
    {
        if(!meshRenderer)
            meshRenderer = gameObject.GetComponent<MeshRenderer>();

        _Revealer = new Revealer(VisionRange,
            Faction,
            UpVision,
            gameObject);

        FogOfWar.RegisterRevealer(_Revealer);

        if (FogOfWar.IsPositionRevealedByFaction(transform.position, FogOfWar.RevealFactionInt))
        {
            meshRenderer.enabled = true;
        }
        else {
            meshRenderer.enabled = false;
        }
    }

    private bool Hide = false;
    private float StartTime = 0;
    
    void Update()
    {
        if (Hide)
        {
            if (Time.time >= StartTime + TurnOffDelay)
            {
                meshRenderer.enabled = false;
            }   
        }

        _Revealer.visionRange = VisionRange;

        if (Faction != FogOfWar.RevealFaction)
        {
            if (FogOfWar.IsPositionRevealedByFaction(transform.position, FogOfWar.RevealFactionInt))
            {
                if (Hide)
                {
                    Hide = false;
                }
                meshRenderer.enabled = true;

            } else {
                if (!Hide)
                {
                    StartTime = Time.time;
                    Hide = true;
                }
            }
        } else {
            if (!meshRenderer.enabled)
            {
                meshRenderer.enabled = true;
            }
        }
    }
    
    public void OnDisable()
    {
        if (_Revealer != null)
            FogOfWar.UnRegisterRevealer(_Revealer.sceneReference.GetInstanceID());
    }
    
    void OnDrawGizmos()
    {
        Gizmos.DrawIcon(transform.position + Vector3.up, "Revealer.png", true);
    }

    public void ChangeFaction(FogOfWar.Players _Faction)
    {
        Debug.Log("Changeing to: "+_Faction);
        FogOfWar.UnRegisterRevealer(_Revealer.sceneReference.GetInstanceID());
        _Revealer.faction = _Faction;
        FogOfWar.RegisterRevealer(_Revealer);
    }
}
