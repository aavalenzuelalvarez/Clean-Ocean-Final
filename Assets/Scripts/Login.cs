using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MySql.Data.MySqlClient;
using UnityEngine.SceneManagement;

public class Login : MonoBehaviour
{
    public InputField usr;
    public InputField pass;
    public GameObject error;


    void Start()
    {
        error.SetActive(false);

    }
    public void Logear()
    {

        string _log = "Select * from usuario WHERE username = '" + usr.text + "' AND  password = '" + pass.text + "';";
        //Conexiones _conexion = GameObject.Find("Conexiones").GetComponent<Conexiones>();
        //MySqlDataReader resultado = _conexion.Select(_log);
        MySqlDataReader resultado = GameObject.Find("Conexiones").GetComponent<Conexiones>().Select(_log);

        if (resultado.HasRows)
        {
            if (resultado.Read())
            {
                int tipo = (int) resultado["tipo_usuario_id"];
                print("tipo " + tipo);
                Debug.Log("Conectado.");
                if (tipo == 3)
                {
                    Conexiones.id_user = resultado["id"].ToString();
                    SceneManager.LoadScene(2);
                    Conexiones.tipo_usuario = tipo.ToString();
                    GameObject.Find("Conexiones").GetComponent<Conexiones>().GameStart();

                }
                else if (tipo == 2 || tipo == 1)
                {
                    Conexiones.id_user = resultado["id"].ToString();
                    SceneManager.LoadScene(1);
                    Conexiones.tipo_usuario = tipo.ToString();
                }
            }

        }
        else
        {
            Debug.Log("nop");
            error.SetActive(true);
            Invoke("desactivar", 2f);

        }


    }
    // Start is called before the first frame update
   

    // Update is called once per frame
    public void desactivar()
    {
        error.SetActive(false);
    }

}
