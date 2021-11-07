using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generador : MonoBehaviour
{
    [SerializeField] GameObject [] posiblesGeneraciones;
    private bool[] disponibles;
    private bool sePuede;
    private int elegido,actuales;
    GameObject objetoElegido;
    public float respawnTime = 1f;
    [SerializeField] bool jugable;

    // Start is called before the first frame update
    void Start()
    {
        jugable = true;
        actuales = posiblesGeneraciones.Length;
        disponibles = new bool[posiblesGeneraciones.Length];
        for (int i = 0; i < actuales; i++)
        {

            disponibles[i] = true;
        }
        StartCoroutine(OleadaDeCosas());
    }

    private void spawnCosa()
    {
        sePuede = false;
        while (!sePuede)
        {
            elegido = Random.Range(0, posiblesGeneraciones.Length);
            sePuede = disponibles[elegido];
            objetoElegido = posiblesGeneraciones[elegido];
        }

        if (objetoElegido.CompareTag("Trofeo"))
        {
            disponibles[elegido] = false;
            actuales--;
        }

        GameObject a = Instantiate(objetoElegido) as GameObject;
        a.transform.position = new Vector2(13f, Random.Range(-4.5f, -1.3f));
        ObjetoMovil compo = a.GetComponent<ObjetoMovil>();
        compo.fueGeneradoPor = this.gameObject;
        compo.posEnArray = elegido;
    }

    public void reincorporarCosa(int pos)
    {
        disponibles[pos] = true;
        actuales++;
    }

    IEnumerator OleadaDeCosas()
    {
        while (jugable)
        {
            yield return new WaitForSeconds(respawnTime);
            if(actuales > 0)
            {
                spawnCosa();
            }
        }
    }


}
