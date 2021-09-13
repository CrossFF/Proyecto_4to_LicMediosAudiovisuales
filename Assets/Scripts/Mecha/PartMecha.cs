using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartMecha
{
    public PartName name;
    public PartPosition position;
    public float atack;
    public float defense;
    public List<SystemMecha> systems;
    private int systemCapacity;

    //Constructores
    public PartMecha(PartName name)
    {
        this.name = name;
        this.systems = new List<SystemMecha>();
        switch (name)
        {
            case PartName.Cabina:
                this.position = PartPosition.Cabina;
                this.atack = 0f;
                this.defense = 10f;
                this.systemCapacity = 3;
                break;
            case PartName.Brazo_Derecho:
                this.position = PartPosition.Brazo_Derecho;
                this.atack = 10f;
                this.defense = 10f;
                this.systemCapacity = 3;
                break;
            case PartName.Brazo_Izquierdo:
                this.position = PartPosition.Brazo_Izquierdo;
                this.atack = 10f;
                this.defense = 10f;
                this.systemCapacity = 3;
                break;
            case PartName.Pierna_Derecha:
                this.position = PartPosition.Pierna_Derecha;
                this.atack = 10f;
                this.defense = 10f;
                this.systemCapacity = 3;
                break;
            case PartName.Pierna_Izquierda:
                this.position = PartPosition.Pierna_Izquierda;
                this.atack = 10f;
                this.defense = 10f;
                this.systemCapacity = 3;
                break;
        }
    }

    public bool CheckSystemCapacity()
    {
        if(this.systems.Count < systemCapacity)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    // funciones estaticas
    public static PartName GetName(BlueprintName name)
    {
        PartName thePart = PartName.Cabina;
        switch (name)
        {
            case BlueprintName.Cabina:
                thePart = PartName.Cabina;
                break;
            case BlueprintName.Brazo_Derecho:
                thePart = PartName.Brazo_Derecho;
                break;
            case BlueprintName.Brazo_Izquierdo:
                thePart = PartName.Brazo_Izquierdo;
                break;
            case BlueprintName.Pierna_Derecha:
                thePart = PartName.Pierna_Derecha;
                break;
            case BlueprintName.Pierna_Izquierda:
                thePart = PartName.Pierna_Izquierda;
                break;
        }
        return thePart;
    }
}


