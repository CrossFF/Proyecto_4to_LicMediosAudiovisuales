using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineManager : MonoBehaviour
{
    [Header("Parametros")]
    public int amountOfMines;// cantidad de minas a instanciar
    public float timeCycle;// tiempo entre ciclo de recoleccion

    [Header("Referencias")]
    public GameObject prefabMine;// prefab de la mina
    //public MineUI ui;// referencia a la UI de las minas
    public NewMineUI ui;// referencia a la UI de las minas
    public Inventory pj;// referencia al inventario del jugador
    public GameObject prefabTrail;// prefab del Line Renderer que hace de camino entre mina y mina

    // variables privadas
    private List<Mine> _mines;// refrencia a las minas instanciadas   
    private float _cronometro;// coronometro para control de ciclo   

    void Start()
    {
        _mines = new List<Mine>();
        //calculo la cantidad de cada tipo de mina que va a existir
        int basicMines = (50 * amountOfMines) / 100;
        int medumMines = (30 * amountOfMines) / 100;
        int advancedMines = (20 * amountOfMines) / 100;
        // instancio las minas segun su tipo
        InstantiateMine(basicMines, TypeOfNode.Basic);
        InstantiateMine(medumMines, TypeOfNode.Medium);
        InstantiateMine(advancedMines, TypeOfNode.Advanced);
        // activo la primera mina
        _mines[0].node.status = StatusNode.Active;
        // seteo el cronometro
        _cronometro = timeCycle;
    }

    void Update()
    {
        ResourceControl();
    }

    private void InstantiateMine(int amount, TypeOfNode type)
    {
        // por cada objeto a instanciar:
        for (int i = 0; i < amount; i++)
        {
            Vector3 pos = new Vector3(Random.Range(0f, 10f), 0f, Random.Range(0f, 10f));
            GameObject temp = Instantiate(prefabMine, pos, Quaternion.identity);
            Mine tempMine = temp.GetComponent<Mine>();
            string name = type + "_" + i;
            tempMine.node = new Node(type, name);
            _mines.Add(tempMine);
        }
    }

    private void ResourceControl()
    {
        _cronometro -= Time.deltaTime;
        if (_cronometro <= 0f)
        {
            //extraigo recursos de la minas activas
            List<Mine> activeMines = Node.GetActiveNodes(_mines);
            List<Resource> extractedResources = new List<Resource>();
            // si la cantidad de minas activas es distinta de 0
            if (activeMines.Count != 0)
            {
                foreach (var item in activeMines)
                {
                    List<Resource> temp = item.GetResources();
                    if (temp.Count != 0)
                    {
                        foreach (var itemTemp in temp)
                        {
                            extractedResources.Add(itemTemp);
                        }
                    }
                }
            }
            //guardo los recursos el inventario
            StoreResources(extractedResources);
            // reseteo cronometro
            _cronometro = timeCycle;
        }
    }

    private void StoreResources(List<Resource> resources)
    {
        pj.Store(resources);
    }

    public void ConectMines(Mine startMine, Mine endMine)
    {
        //agrego la mina al camino de la primera
        startMine.node.trails.Add(endMine);
        //activo la mina a conectar
        endMine.node.status = StatusNode.Active;
        //instancio una linea
        GameObject temp = Instantiate(prefabTrail, Vector3.zero, Quaternion.identity);
        LineRenderer line = temp.GetComponent<LineRenderer>();
        line.SetPosition(0, startMine.transform.position);
        line.SetPosition(1, endMine.transform.position);
    }

    public void ActivateMineUI()
    {
        //List<Mine> activeMines = Node.GetActiveNodes(_mines);
        //ui.ShowMenu(activeMines);
        ui.ShowMenu();
    }

    public void HideMineUI()
    {
        ui.HideMenu();
    }

    public List<Mine> GetInactiveMines()
    {
        List<Mine> inactiveMines = Node.GetInactiveNodes(_mines);
        return inactiveMines;
    }

    public List<Mine> GetActiveMines()
    {
        List<Mine> activeMines = Node.GetActiveNodes(_mines);
        return activeMines;
    }

    public void NewPos(Mine mine)
    {
        Vector3 pos = new Vector3(Random.Range(0f, 10f), 0f, Random.Range(0f, 10f));
        mine.transform.position = pos;
    }

    public void ShowMine(Mine mine)
    {
        ui.ShowMine(mine);
    }
}
