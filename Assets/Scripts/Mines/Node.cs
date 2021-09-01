using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TypeOfNode
{
    Basic,
    Medium,
    Advanced
}

public enum StatusNode
{
    Active,
    Inactive,
    Blocked,
    Working,
    Empty
}

public class Node
{
    public TypeOfNode type;
    public string name;
    public List<Resource> resources;
    public List<Mine> trails;
    public StatusNode status;

    // Constructores
    public Node(TypeOfNode type, string name)
    {
        this.type = type;
        this.name = name;
        this.status = StatusNode.Inactive;

        switch (type)
        {
            case TypeOfNode.Basic:
                List<Resource> temp1 = new List<Resource>();
                temp1.Add(new Resource(TypeResource.BasicOre1));
                temp1.Add(new Resource(TypeResource.BasicOre2));
                temp1.Add(new Resource(TypeResource.BasicOre3));
                this.resources = temp1;
                break;
            case TypeOfNode.Medium:
                List<Resource> temp2 = new List<Resource>();
                temp2.Add(new Resource(TypeResource.MediumOre1));
                temp2.Add(new Resource(TypeResource.MediumOre2));
                temp2.Add(new Resource(TypeResource.MediumOre3));
                this.resources = temp2;
                break;
            case TypeOfNode.Advanced:
                List<Resource> temp3 = new List<Resource>();
                temp3.Add(new Resource(TypeResource.AdvancedOre1));
                temp3.Add(new Resource(TypeResource.AdvancedOre2));
                temp3.Add(new Resource(TypeResource.AdvancedOre3));
                this.resources = temp3;
                break;
        }
    }

    // metodos propios
    public List<Resource> GetResources()
    {
        List<Resource> extractedResources = new List<Resource>();
        foreach (var item in this.resources)
        {
            Resource temp = item.GetResource();
            if (temp != null)
            {
                extractedResources.Add(temp);
            }
        }
        return extractedResources;
    }

    public void SetMachine(Machine machine, int indexResource)
    {
        this.resources[indexResource].SetMachine(machine);
    }

    // metodos estaticos 
    public static List<Mine> GetTypeNodes(List<Mine> mines, StatusNode type)
    {
        List<Mine> theMines = new List<Mine>();
        foreach (var item in mines)
        {
            if (item.node.status == type)
            {
                theMines.Add(item);
            }
        }
        return theMines;
    }

    public static bool Instalable(Machine machine, Node mine)
    {
        bool instalable = false;
        switch (mine.type)
        {
            case TypeOfNode.Basic:
                if (machine.name == MachineName.Dron_Excavador || machine.name == MachineName.Excavadora || machine.name == MachineName.Excavadora_Avanzada)
                    instalable = true;
                break;
            case TypeOfNode.Medium:
                if (machine.name == MachineName.Excavadora || machine.name == MachineName.Excavadora_Avanzada)
                    instalable = true;
                break;
            case TypeOfNode.Advanced:
                if (machine.name == MachineName.Excavadora_Avanzada)
                    instalable = true;
                break;
        }
        return instalable;
    }
}
