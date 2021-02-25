using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using MySql.Data.MySqlClient;
using System;
using System.IO;
using System.Text;

public class Conexiones : MonoBehaviour
{
    public static Conexiones conexion;
    public static String connetionString = "host=ulearnet.org; Port =3306; UserName=reim_ulearnet; Password=KsclS$AcSx.20Cv83xT; Database=ulearnet_reim_pilotaje;";
    public static MySqlConnection cnn = new MySqlConnection(connetionString);
    public static String id_per = "201902";
    public static String id_user = "1";
    public static String id_reim = "3";
    public static String correcto = "2";
    public static String sesion_id;
    public static String id_actividad="0";
    public static String id_elemento = "0";
    public static int posx, posy;
    public int secondsToCount=60;
    public float secondsCounter = 0;
    public bool jugando = true;
    public Touch touch;
    public static string ahora;
    public static string termino;
    public static string inicio;
    TouchPhase touchPhase = TouchPhase.Ended;
    internal static string tipo_usuario;
    

    void Awake()
    {
        if (conexion == null)
        {
            conexion = this;
            DontDestroyOnLoad(gameObject);
        }else if(conexion != this)
        {
            Destroy(gameObject);
        }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        CrearConexion();
    }

    // Update is called once per frame
    void Update()
    {
        if (jugando)
        {
            secondsCounter += Time.deltaTime;
            if (secondsCounter >= secondsToCount)
            {
                secondsCounter = 0;
                ahora = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffffff");
                MySqlCommand query = cnn.CreateCommand();
                query.CommandText = ("UPDATE `asigna_reim_alumno` SET `datetime_termino` = '" + ahora + "' WHERE `sesion_id` = '" + sesion_id + "';");
                if (SceneManager.GetActiveScene().name == "Progreso")
                {
                    id_actividad = 3000.ToString();
                }
                else if (SceneManager.GetActiveScene().name == "Actividad Tesoro")
                {
                    id_actividad = 3005.ToString();
                }
                else if (SceneManager.GetActiveScene().name == "Actividad Drag And Drop")
                {
                    id_actividad = 3006.ToString();
                }
                else if (SceneManager.GetActiveScene().name == "Actividad Laberinto")
                {
                    id_actividad = 3004.ToString();
                }
                else if (SceneManager.GetActiveScene().name == "Actividad Lancha")
                {
                    id_actividad = 3002.ToString();
                }
                else if (SceneManager.GetActiveScene().name == "Actividad suma")
                {
                    id_actividad = 3003.ToString();
                }
                else if (SceneManager.GetActiveScene().name == "MainMenu")
                {
                    id_actividad = 3001.ToString();
                }
                else if (SceneManager.GetActiveScene().name == "Actividad Destreza")
                {
                    id_actividad = 3077.ToString();
                }
                query.CommandText = ("UPDATE `ulearnet_reim_pilotaje`.`tiempoxactividad` SET `final` = '" + ahora + "' WHERE (`inicio` = '" + inicio + "' AND `usuario_id` =  '" + id_user + "' AND `reim_id` = '" + id_reim + "' AND `actividad_id` = '" + id_actividad + "');");
                //print(inicio);
                //print(ahora);
                query.ExecuteNonQuery();
            }
        }
        //if (Input.touchCount == 1 && Input.GetTouch(0).phase == touchPhase)
        //{
        //    Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
        //    RaycastHit hit;
        //    Debug.DrawRay(ray.origin, ray.direction * 100, Color.yellow, 100f);
        //    if (Physics.Raycast(ray, out hit))
        //    {
        //        Debug.Log(hit.transform.name);
        //        if (hit.collider != null)
        //        {

        //            GameObject touchedObject = hit.transform.gameObject;

        //            Debug.Log("Touched " + touchedObject.transform.name);
        //            if (touchedObject.transform.name == "Boton play")
        //            {
        //                id_elemento = 3000.ToString();
        //            }
        //            else if (touchedObject.transform.name == "Boton volver")
        //            {
        //                id_elemento = 3001.ToString();
        //            }
        //            else if (touchedObject.transform.name == "Boton avanzar")
        //            {
        //                id_elemento = 3002.ToString();
        //            }
        //            else if (touchedObject.transform.name == "actividadLancha")
        //            {
        //                id_elemento = 3003.ToString();
        //            }
        //            else if (touchedObject.transform.name == "actividadLaberinto")
        //            {
        //                id_elemento = 3004.ToString();
        //            }
        //            else if (touchedObject.transform.name == "actividadTesoro")
        //            {
        //                id_elemento = 3005.ToString();
        //            }
        //            else if (touchedObject.transform.name == "actividadSuma")
        //            {
        //                id_elemento = 3006.ToString();
        //            }
        //            else if (touchedObject.transform.name == "Boton musica" || touchedObject.transform.name == "Mute" || touchedObject.transform.name == "Toggle")
        //            {
        //                id_elemento = 3007.ToString();
        //            }
        //            else if (touchedObject.transform.name == "Boton pausa")
        //            {
        //                id_elemento = 3008.ToString();
        //            }
        //            else if (touchedObject.transform.name == "Boton reinicio")
        //            {
        //                id_elemento = 3009.ToString();
        //            }
        //            else if (touchedObject.transform.name == "Respuesta alumno actsuma")
        //            {
        //                id_elemento = 3010.ToString();
        //            }
        //            else if (touchedObject.transform.name == "Respuesta correcta actsuma")
        //            {
        //                id_elemento = 3011.ToString();
        //            }
        //            else if (touchedObject.transform.name == "Pez payaso" || touchedObject.transform.name == "Clownfish_prefab(Clone)")
        //            {
        //                id_elemento = 3012.ToString();
        //            }
        //            else if (touchedObject.transform.name == "Boton instrucciones audio")
        //            {
        //                id_elemento = 3013.ToString();
        //            }
        //            else if (touchedObject.transform.name == "actividadDrag")
        //            {
        //                id_elemento = 3014.ToString();
        //            }
        //            else if (touchedObject.transform.name == "Background")
        //            {
        //                id_elemento = 3015.ToString();
        //            }
        //            else if (touchedObject.transform.name == "Agua" || touchedObject.transform.name == "Waves")
        //            {
        //                id_elemento = 3016.ToString();
        //            }
        //            else if (touchedObject.transform.name == "Coral")
        //            {
        //                id_elemento = 3017.ToString();
        //            }
        //            else if (touchedObject.transform.name == "RocaGrande")
        //            {
        //                id_elemento = 3018.ToString();
        //            }
        //            else if (touchedObject.transform.name == "Basura Barril" || touchedObject.transform.name == "Barril" || touchedObject.transform.name == "BarrilPrefab" || touchedObject.transform.name == "Basura5" || touchedObject.transform.name == "Basura9")
        //            {
        //                id_elemento = 3019.ToString();
        //            }
        //            else if (touchedObject.transform.name == "RocaPequeña")
        //            {
        //                id_elemento = 3020.ToString();
        //            }
        //            else if (touchedObject.transform.name == "Ballena")
        //            {
        //                id_elemento = 3021.ToString();
        //            }
        //            else if (touchedObject.transform.name == "Shark" || touchedObject.transform.name == "Tiburon blanco" || touchedObject.transform.name == "Tiburon Blanco")
        //            {
        //                id_elemento = 3022.ToString();
        //            }
        //            else if (touchedObject.transform.name == "Tiburon toro")
        //            {
        //                id_elemento = 3023.ToString();
        //            }
        //            else if (touchedObject.transform.name == "Orca")
        //            {
        //                id_elemento = 3024.ToString();
        //            }
        //            else if (touchedObject.transform.name == "Delfin")
        //            {
        //                id_elemento = 3025.ToString();
        //            }
        //            else if (touchedObject.transform.name == "Pulpo")
        //            {
        //                id_elemento = 3026.ToString();
        //            }
        //            else if (touchedObject.transform.name == "Calamar")
        //            {
        //                id_elemento = 3027.ToString();
        //            }
        //            else if (touchedObject.transform.name == "Pez sol" || touchedObject.transform.name == "Pez sol(Clone)")
        //            {
        //                id_elemento = 3028.ToString();
        //            }
        //            else if (touchedObject.transform.name == "Pez cofre" || touchedObject.transform.name == "Pez cofre(Clone)")
        //            {
        //                id_elemento = 3029.ToString();
        //            }
        //            else if (touchedObject.transform.name == "Pez mariposa")
        //            {
        //                id_elemento = 3030.ToString();
        //            }
        //            //else if (touchedObject.transform.name == "Pez mariposa")
        //            //{
        //            //    id_elemento = 3031.ToString();
        //            //}
        //            else if (touchedObject.transform.name == "Pez luna")
        //            {
        //                id_elemento = 3031.ToString();
        //            }
        //            else if (touchedObject.transform.name == "Pez leon")
        //            {
        //                id_elemento = 3032.ToString();
        //            }
        //            else if (touchedObject.transform.name == "Mantarraya")
        //            {
        //                id_elemento = 3033.ToString();
        //            }
        //            else if (touchedObject.transform.name == "Tortuga" || touchedObject.transform.name == "Green_turtle_prefab(Clone)")
        //            {
        //                id_elemento = 3034.ToString();
        //            }
        //            else if (touchedObject.transform.name == "Salmon" || touchedObject.transform.name == "Salmon_prefab(Clone)")
        //            {
        //                id_elemento = 3035.ToString();
        //            }
        //            else if (touchedObject.transform.name == "Sardina")
        //            {
        //                id_elemento = 3036.ToString();
        //            }
        //            else if (touchedObject.transform.name == "Pez idolo moro" || touchedObject.transform.name == "Moorish_idol_prefab(Clone)")
        //            {
        //                id_elemento = 3037.ToString();
        //            }
        //            else if (touchedObject.transform.name == "Pez cirujano purpura")
        //            {
        //                id_elemento = 3038.ToString();
        //            }
        //            else if (touchedObject.transform.name == "Salmon rojo")
        //            {
        //                id_elemento = 3039.ToString();
        //            }
        //            else if (touchedObject.transform.name == "Basura Vaso" || touchedObject.transform.name == "Vaso" || touchedObject.transform.name == "Basura1" || touchedObject.transform.name == "Basura4" || touchedObject.transform.name == "Basura13" || touchedObject.transform.name == "Basura17")
        //            {
        //                id_elemento = 3040.ToString();
        //            }
        //            else if (touchedObject.transform.name == "Basura Bolsa" || touchedObject.transform.name == "Bolsa" || touchedObject.transform.name == "BolsaPrefab" || touchedObject.transform.name == "Basura2" || touchedObject.transform.name == "Basura8" || touchedObject.transform.name == "Basura12" || touchedObject.transform.name == "Basura16" || touchedObject.transform.name == "Basura19" || touchedObject.transform.name == "BasuraPrefab" || touchedObject.transform.name == "BasuraPrefab1" || touchedObject.transform.name == "BasuraPrefab2" || touchedObject.transform.name == "BasuraPrefab3" || touchedObject.transform.name == "BasuraPrefab4" || touchedObject.transform.name == "BasuraPrefab5" || touchedObject.transform.name == "BasuraPrefab6")
        //            {
        //                id_elemento = 3041.ToString();
        //            }
        //            else if (touchedObject.transform.name == "Basura Botella azul" || touchedObject.transform.name == "Botella azul" || touchedObject.transform.name == "Basura3" || touchedObject.transform.name == "Basura11")
        //            {
        //                id_elemento = 3042.ToString();
        //            }
        //            else if (touchedObject.transform.name == "Basura Botella verde" || touchedObject.transform.name == "Botella verde" || touchedObject.transform.name == "Basura6" || touchedObject.transform.name == "Basura7" || touchedObject.transform.name == "Basura10" || touchedObject.transform.name == "Basura15" || touchedObject.transform.name == "Basura18")
        //            {
        //                id_elemento = 3043.ToString();
        //            }
        //            else if (touchedObject.transform.name == "Basura spray" || touchedObject.transform.name == "Basura14" || touchedObject.transform.name == "Basura20" || touchedObject.transform.name == "Spray")
        //            {
        //                id_elemento = 3044.ToString();
        //            }
        //            else if (touchedObject.transform.name == "Pelota actividades")
        //            {
        //                id_elemento = 3045.ToString();
        //            }
        //            else if (touchedObject.transform.name == "isla actLancha")
        //            {
        //                id_elemento = 3046.ToString();
        //            }
        //            else if (touchedObject.transform.name == "Lancha" || touchedObject.transform.name == "BoatPrefab")
        //            {
        //                id_elemento = 3047.ToString();
        //            }
        //            else if (touchedObject.transform.name == "Pelota actLab")
        //            {
        //                id_elemento = 3048.ToString();
        //            }
        //            else if (touchedObject.transform.name == "Petroleo actLab")
        //            {
        //                id_elemento = 3049.ToString();
        //            }
        //            else if (touchedObject.transform.name == "Isla actLab")
        //            {
        //                id_elemento = 3050.ToString();
        //            }
        //            else if (touchedObject.transform.name == "Faro actLab")
        //            {
        //                id_elemento = 3051.ToString();
        //            }
        //            else if (touchedObject.transform.name == "Elefante marino actLab")
        //            {
        //                id_elemento = 3052.ToString();
        //            }
        //            else if (touchedObject.transform.name == "Button_faro")
        //            {
        //                id_elemento = 3053.ToString();
        //            }
        //            else if (touchedObject.transform.name == "Button_volcan")
        //            {
        //                id_elemento = 3054.ToString();
        //            }
        //            else if (touchedObject.transform.name == "Panelpregunta actSuma")
        //            {
        //                id_elemento = 3055.ToString();
        //            }
        //            else if (touchedObject.transform.name == "Pez cirujano azul" || touchedObject.transform.name == "Blue_tang_prefab(Clone)")
        //            {
        //                id_elemento = 3056.ToString();
        //            }
        //            else if (touchedObject.transform.name == "Panel respuestas actSuma")
        //            {
        //                id_elemento = 3057.ToString();
        //            }
        //            else if (touchedObject.transform.name == "Panel recompensa")
        //            {
        //                id_elemento = 3058.ToString();
        //            }
        //            else if (touchedObject.transform.name == "Recompensa")
        //            {
        //                id_elemento = 3059.ToString();
        //            }
        //            else if (touchedObject.transform.name == "Concha actDibujo")
        //            {
        //                id_elemento = 3060.ToString();
        //            }
        //            else if (touchedObject.transform.name == "Estrella de mar" || touchedObject.transform.name == "Starfish_v1_prefab (1)(Clone)")
        //            {
        //                id_elemento = 3061.ToString();
        //            }
        //            else if (touchedObject.transform.name == "Erizo de mar" || touchedObject.transform.name == "Sea_urchin_prefab(Clone)")
        //            {
        //                id_elemento = 3062.ToString();
        //            }
        //            else if (touchedObject.transform.name == "Cangrejo" || touchedObject.transform.name == "Crab_prefab(Clone)")
        //            {
        //                id_elemento = 3063.ToString();
        //            }
        //            else if (touchedObject.transform.name == "Cangrejo ermitaño" || touchedObject.transform.name == "Ammonite_prefab(Clone)")
        //            {
        //                id_elemento = 3064.ToString();
        //            }
        //            else if (touchedObject.transform.name == "Pieza pregunta")
        //            {
        //                id_elemento = 3065.ToString();
        //            }
        //            else if (touchedObject.transform.name == "Pieza respuesta")
        //            {
        //                id_elemento = 3066.ToString();
        //            }
        //            else if (touchedObject.transform.name == "Boton instrucciones texto" || touchedObject.transform.name == "Boton instrucciones")
        //            {
        //                id_elemento = 3067.ToString();
        //            }
        //            else if (touchedObject.transform.name == "BasuraRueda" || touchedObject.transform.name == "RuedaPrefab")
        //            {
        //                id_elemento = 3068.ToString();
        //            }
        //            else if (touchedObject.transform.name == "Basura Leche")
        //            {
        //                id_elemento = 3069.ToString();
        //            }
        //            else if (touchedObject.transform.name == "Respuesta1")
        //            {
        //                id_elemento = 3070.ToString();
        //            }
        //            else if (touchedObject.transform.name == "Respuesta2")
        //            {
        //                id_elemento = 3071.ToString();
        //            }
        //            else if (touchedObject.transform.name == "Respuesta3")
        //            {
        //                id_elemento = 3072.ToString();
        //            }
        //            else if (touchedObject.transform.name == "Button_calavera")
        //            {
        //                id_elemento = 3073.ToString();
        //            }
        //            else if (touchedObject.transform.name == "Button_Tesoro")
        //            {
        //                id_elemento = 3074.ToString();
        //            }

        //            // PANTALLAS DEL JUEGO

        //            if (SceneManager.GetActiveScene().name == "Progreso")
        //            {
        //                id_actividad = 3000.ToString();
        //            }
        //            else if (SceneManager.GetActiveScene().name == "Actividad Tesoro")
        //            {
        //                id_actividad = 3005.ToString();
        //            }
        //            else if (SceneManager.GetActiveScene().name == "Actividad Drag And Drop")
        //            {
        //                id_actividad = 3006.ToString();
        //            }
        //            else if (SceneManager.GetActiveScene().name == "Actividad Laberinto")
        //            {
        //                id_actividad = 3004.ToString();
        //            }
        //            else if (SceneManager.GetActiveScene().name == "Actividad Lancha")
        //            {
        //                id_actividad = 3002.ToString();
        //            }
        //            else if (SceneManager.GetActiveScene().name == "Actividad suma")
        //            {
        //                id_actividad = 3003.ToString();
        //            }
        //            else if (SceneManager.GetActiveScene().name == "MainMenu")
        //            {
        //                id_actividad = 3001.ToString();
        //            }
        //            else if (SceneManager.GetActiveScene().name == "Actividad Destreza")
        //            {
        //                id_actividad = 3077.ToString();
        //            }

        //            if (id_actividad != "0" && id_elemento != "0")
        //            {
        //                //cnn.Open();
        //                Debug.Log("Voy a registrar");
        //                ahora = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffffff");
        //                MySqlCommand query = cnn.CreateCommand();
        //                touch = Input.GetTouch(0);
        //                query.CommandText = "INSERT INTO `ulearnet_reim_pilotaje`.`alumno_respuesta_actividad` (`id_per`, `id_user`, `id_reim`, `id_actividad`, `id_elemento`, `datetime_touch`, `fila`, `columna`, `correcta`) VALUES ('" + id_per + "', '" + id_user + "', '" + id_reim + "', '" + id_actividad + "', '" + id_elemento + "', '" + ahora + "', '" + touch.position.y + "', '" + touch.position.x + "','2');";
        //                query.ExecuteNonQuery();
        //                //cnn.Close();
        //                Debug.Log("Connection Closed!");
        //                print("REGISTRÉ");
        //                id_elemento = "0";
        //                id_actividad = "0";
        //            }

        //            }
        //        }
        //    }
    }
    public void CrearConexion()
    {
        string connetionString = null;
        connetionString = "host=ulearnet.org; Port =3306; UserName=reim_ulearnet; Password=KsclS$AcSx.20Cv83xT; Database=ulearnet_reim_pilotaje;";
        //"server=localhost;database=testDB;uid=root;pwd=abc123;";
        cnn = new MySqlConnection(connetionString);
        try
        {
            cnn.Open();
            Debug.Log("Connection Open ! ");
        }
        catch (Exception ex)
        {
            Debug.Log("Can not open connection ! " + ex);
        }
    }

    public void Boton()
    {
        print("Boton");
        correcto = "2";
        posx = 0;
        posy = 0;
        InsertQuery();
    }

    public void SetActividad(String act)
    {
        if (act=="" || act == null)
        {
                    if (SceneManager.GetActiveScene().name == "Progreso")
                    {
                        id_actividad = 3000.ToString();
                    }
                    else if (SceneManager.GetActiveScene().name == "Actividad Tesoro")
                    {
                        id_actividad = 3005.ToString();
                    }
                    else if (SceneManager.GetActiveScene().name == "Actividad Drag And Drop")
                    {
                        id_actividad = 3006.ToString();
                    }
                    else if (SceneManager.GetActiveScene().name == "Actividad Laberinto")
                    {
                        id_actividad = 3004.ToString();
                    }
                    else if (SceneManager.GetActiveScene().name == "Actividad Lancha")
                    {
                        id_actividad = 3002.ToString();
                    }
                    else if (SceneManager.GetActiveScene().name == "Actividad suma")
                    {
                        id_actividad = 3003.ToString();
                    }
                    else if (SceneManager.GetActiveScene().name == "MainMenu")
                    {
                        id_actividad = 3001.ToString();
                    }
                    else if (SceneManager.GetActiveScene().name == "Actividad Destreza")
                    {
                       id_actividad = 3077.ToString();
                    }
        }
        id_actividad = act;
    }

    public void SetElemento(String elm)
    {
        id_elemento = elm;
    }

    public void AlmacenaCorrecto(string SceneActive, string respuesta="false")
    {
        if (SceneActive == "Actividad Tesoro")
        {
            id_actividad = 3005.ToString();
            id_elemento = "3005";
        }
        else if (SceneActive == "Actividad Drag and Drop")
        {
            id_actividad = 3006.ToString();
            id_elemento = "3059";
        }
        else if (SceneActive == "Actividad Laberinto")
        {
            id_actividad = 3004.ToString();
            if (respuesta == "RocaE")
            {
                id_elemento = 3052.ToString();
            }
            else if (respuesta == "Faro actLab")
            {
                id_elemento = 3051.ToString();
            }
            else if (respuesta == "Isla actLab")
            {
                id_elemento = 3050.ToString();
            }
        }
        else if (SceneActive == "Actividad Lancha")
        {
            id_actividad = 3002.ToString();
            id_elemento = "3059";
        }
        else if (SceneActive == "Actividad suma")
        {
            id_actividad = 3003.ToString();
            if (respuesta == "Respuesta1")
            {
                id_elemento = 3070.ToString();
            }
            else if (respuesta == "Respuesta2")
            {
                id_elemento = 3071.ToString();
            }
            else if (respuesta == "Respuesta3")
            {
                id_elemento = 3072.ToString();
            }
        }
        else if (SceneActive == "Actividad Destreza")
        {
            id_actividad = 3007.ToString();
            id_elemento = "3041";
        }
        else if(SceneActive == "Actividad Patrones")
        {
            id_actividad = "3008";
            id_elemento = "3082";
        }
        correcto = "1";
        posx = 0;
        posy = 0;
        InsertQuery();
        //ahora = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffffff");
        //MySqlCommand query = cnn.CreateCommand();
        //query.CommandText = "INSERT INTO `ulearnet_reim_pilotaje`.`alumno_respuesta_actividad` (`id_per`, `id_user`, `id_reim`, `id_actividad`, `id_elemento`, `datetime_touch`, `fila`, `columna`, `correcta`) VALUES ('" + id_per + "', '" + id_user + "', '" + id_reim + "', '" + id_actividad + "', '" + id_elemento + "', '" + ahora + "', '" + 0 + "', '" + 0 + "','1');";
        //query.ExecuteNonQuery();
        //print("REGISTRÉ CORRECTO");

    }
    public void AlmacenaIncorrecto(string SceneActive, string respuesta="false")
    {
        if (SceneActive == "Actividad Tesoro")
        {
            id_actividad = 3005.ToString();
            id_elemento = "3059";
        }
        else if (SceneActive == "Actividad Drag and Drop")
        {
            id_actividad = 3006.ToString();
            id_elemento = "3059";
        }
        else if (SceneActive == "Actividad Laberinto")
        {
            id_actividad = 3004.ToString();
            id_elemento = 3049.ToString();
        }
        else if (SceneActive == "Actividad Lancha")
        {
            id_actividad = 3002.ToString();
            id_elemento = "3059";
        }
        else if (SceneActive == "Actividad suma")
        {
            id_actividad = 3003.ToString();
            if (respuesta == "Respuesta1")
            {
                id_elemento = 3070.ToString();
            }
            else if (respuesta == "Respuesta2")
            {
                id_elemento = 3071.ToString();
            }
            else if (respuesta == "Respuesta3")
            {
                id_elemento = 3072.ToString();
            }
        }
        else if (SceneActive == "Actividad Destreza")
        {
            id_actividad = 3007.ToString();
            id_elemento = "3041";
        }
        else if(SceneActive == "Actividad Patrones")
        {
            id_actividad = "3008";
            id_elemento = "3082";
        }
        correcto = "0";
        posx = 0;
        posy = 0;
        InsertQuery();
        // Debug.Log("Connection Open ! ");
        // ahora = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffffff");
        // MySqlCommand query = cnn.CreateCommand();
        // query.CommandText = "INSERT INTO `ulearnet_reim_pilotaje`.`alumno_respuesta_actividad` (`id_per`, `id_user`, `id_reim`, `id_actividad`, `id_elemento`, `datetime_touch`, `fila`, `columna`, `correcta`) VALUES ('" + id_per + "', '" + id_user + "', '" + id_reim + "', '" + id_actividad + "', '" + id_elemento + "', '" + ahora + "', '" + 0 + "', '" + 0 + "','0');";
        // query.ExecuteNonQuery();

    }

    public void Colision(string SceneActive, string respuesta = "false")
    {
        if (SceneActive == "Actividad Drag and Drop")
        {
            id_elemento = 3066.ToString();
            id_actividad = 3006.ToString();
        }
        else if (SceneActive == "Actividad Laberinto")
        {
            id_actividad = 3004.ToString();
            id_elemento = "3048";
        }
        else if (SceneActive == "Actividad Lancha")
        {
            if (respuesta == "BasuraPrefab")
            {
                id_elemento = 3041.ToString();
            }
            else if (respuesta == "RuedaPrefab")
            {
                id_elemento = 3068.ToString();
            }
            else if (respuesta == "BarrilPrefab")
            {
                id_elemento = 3019.ToString();
            }
            id_actividad = 3002.ToString();
        }
        correcto = "3";
        posx = 0;
        posy = 0;
        InsertQuery();

    }
    public void Pasos()
    {
        correcto = "4";
        posx = 0;
        posy = 0;
        id_actividad = "3005";
        id_elemento = "3005";
        InsertQuery();
    }
    public void Saltos()
    {
        correcto = "5";
        posx = 0;
        posy = 0;
        id_actividad = "3005";
        id_elemento = "3005";
        InsertQuery();
    }

    //public MySqlDataReader Select(string _select)
    //{
    //    //String connetionString = "host=ulearnet.org; Port =3306; UserName=reim_ulearnet; Password=KsclS$AcSx.20Cv83xT; Database=ulearnet_reim_pruebas;";
    //    MySqlCommand cmd = cnn.CreateCommand();
    //    cmd.CommandText = _select;
    //    MySqlDataReader resultado = cmd.ExecuteReader();
    //    return resultado;

    //}
    public MySqlDataReader Select(string _select)
    {
        //String connetionString = "host=ulearnet.org; Port =3306; UserName=reim_ulearnet; Password=KsclS$AcSx.20Cv83xT; Database=ulearnet_reim_pruebas;";
        MySqlConnection conexion = new MySqlConnection(connetionString);

        conexion.Open();
        MySqlCommand cmd = conexion.CreateCommand();
        cmd.CommandText = _select;
        MySqlDataReader resultado = cmd.ExecuteReader();
        return resultado;

    }
    public void PlayTimeLancha()
    {
        ahora = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffffff");
        inicio = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffffff");
        MySqlCommand query = cnn.CreateCommand();
        ahora = inicio;
        //INSERT INTO `ulearnet_reim_pilotaje`.`tiempoxactividad` (`inicio`, `final`, `causa`, `usuario_id`, `reim_id`, `actividad_id`) VALUES('2019-12-07', '2019-12-07', '0', '1', '1', '1');
        query.CommandText = "INSERT INTO `ulearnet_reim_pilotaje`.`tiempoxactividad` (`inicio`, `final`, `causa`, `usuario_id`, `reim_id`, `actividad_id`) VALUES ('" + ahora + "', '" + ahora + "', '" + 0 + "', '" + id_user + "', '" + id_reim + "', '" + 3002 + "');";
        query.ExecuteNonQuery();
    }
    public void PlayTimeSuma()
    {
        ahora = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffffff");
        inicio = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffffff");
        MySqlCommand query = cnn.CreateCommand();
        print(ahora);
        ahora = inicio;
        print(inicio);
        //INSERT INTO `ulearnet_reim_pilotaje`.`tiempoxactividad` (`inicio`, `final`, `causa`, `usuario_id`, `reim_id`, `actividad_id`) VALUES('2019-12-07', '2019-12-07', '0', '1', '1', '1');
        query.CommandText = "INSERT INTO `ulearnet_reim_pilotaje`.`tiempoxactividad` (`inicio`, `final`, `causa`, `usuario_id`, `reim_id`, `actividad_id`) VALUES ('" + ahora + "', '" + ahora + "', '" + 0 + "', '" + id_user + "', '" + id_reim + "', '" + 3003 + "');";
        query.ExecuteNonQuery();
    }

    public void PlayTimeLaberinto()
    {
        ahora = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffffff");
        inicio = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffffff");
        MySqlCommand query = cnn.CreateCommand();
        ahora = inicio;
        //INSERT INTO `ulearnet_reim_pilotaje`.`tiempoxactividad` (`inicio`, `final`, `causa`, `usuario_id`, `reim_id`, `actividad_id`) VALUES('2019-12-07', '2019-12-07', '0', '1', '1', '1');
        query.CommandText = "INSERT INTO `ulearnet_reim_pilotaje`.`tiempoxactividad` (`inicio`, `final`, `causa`, `usuario_id`, `reim_id`, `actividad_id`) VALUES ('" + ahora + "', '" + ahora + "', '" + 0 + "', '" + id_user + "', '" + id_reim + "', '" + 3004 + "');";
        query.ExecuteNonQuery();
    }
    public void PlayTimeTesoro()
    {
        ahora = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffffff");
        inicio = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffffff");
        MySqlCommand query = cnn.CreateCommand();
        ahora = inicio;
        //INSERT INTO `ulearnet_reim_pilotaje`.`tiempoxactividad` (`inicio`, `final`, `causa`, `usuario_id`, `reim_id`, `actividad_id`) VALUES('2019-12-07', '2019-12-07', '0', '1', '1', '1');
        query.CommandText = "INSERT INTO `ulearnet_reim_pilotaje`.`tiempoxactividad` (`inicio`, `final`, `causa`, `usuario_id`, `reim_id`, `actividad_id`) VALUES ('" + ahora + "', '" + ahora + "', '" + 0 + "', '" + id_user + "', '" + id_reim + "', '" + 3005 + "');";
        query.ExecuteNonQuery();
    }
    public void PlayTimeArrastrar()
    {
        ahora = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffffff");
        inicio = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffffff");
        MySqlCommand query = cnn.CreateCommand();
        ahora = inicio;
        //INSERT INTO `ulearnet_reim_pilotaje`.`tiempoxactividad` (`inicio`, `final`, `causa`, `usuario_id`, `reim_id`, `actividad_id`) VALUES('2019-12-07', '2019-12-07', '0', '1', '1', '1');
        query.CommandText = "INSERT INTO `ulearnet_reim_pilotaje`.`tiempoxactividad` (`inicio`, `final`, `causa`, `usuario_id`, `reim_id`, `actividad_id`) VALUES ('" + ahora + "', '" + ahora + "', '" + 0 + "', '" + id_user + "', '" + id_reim + "', '" + 3006 + "');";
        query.ExecuteNonQuery();
    }
    public void PlayTimeDestreza()
    {
        ahora = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffffff");
        inicio = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffffff");
        MySqlCommand query = cnn.CreateCommand();
        ahora = inicio;
        print(inicio);
        //INSERT INTO `ulearnet_reim_pilotaje`.`tiempoxactividad` (`inicio`, `final`, `causa`, `usuario_id`, `reim_id`, `actividad_id`) VALUES('2019-12-07', '2019-12-07', '0', '1', '1', '1');
        query.CommandText = "INSERT INTO `ulearnet_reim_pilotaje`.`tiempoxactividad` (`inicio`, `final`, `causa`, `usuario_id`, `reim_id`, `actividad_id`) VALUES ('" + ahora + "', '" + ahora + "', '" + 0 + "', '" + id_user + "', '" + id_reim + "', '" + 3007 + "');";
        query.ExecuteNonQuery();
    }

    public void FTimeVolver()
    {
        if (SceneManager.GetActiveScene().name == "Actividad Tesoro")
        {
            //null
            id_actividad = 3005.ToString();
            print("tesoro");
        }
        else if (SceneManager.GetActiveScene().name == "Actividad Drag and Drop")
        {
            //null
            id_actividad = 3006.ToString();
            print("Drag");
        }
        else if (SceneManager.GetActiveScene().name == "Actividad Laberinto")
        {
            //null
            id_actividad = 3004.ToString();
            print("laberinto");
        }
        else if (SceneManager.GetActiveScene().name == "Actividad Lancha")
        {
            //funciona
            id_actividad = 3002.ToString();
            print("lancha");
        }
        else if (SceneManager.GetActiveScene().name == "Actividad suma")
        {
            //funciona
            id_actividad = 3003.ToString();
            print("suma");
        }
        else if (SceneManager.GetActiveScene().name == "Actividad Destreza")
        {
            
            id_actividad = 3007.ToString();
            print("destreza");
        }
        else
        {
            print("chupalo");
            print((SceneManager.GetActiveScene().name));
        }
        if (SceneManager.GetActiveScene().name != "Progreso" && SceneManager.GetActiveScene().name != "MainMenu")
        {
            termino = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffffff");
            MySqlCommand query = cnn.CreateCommand();
            print("despues");

            query.CommandText = "UPDATE `ulearnet_reim_pilotaje`.`tiempoxactividad` SET `final` = '" + termino + "' , `causa` = '" + "1" + "' WHERE (`inicio` = '" + inicio + "' AND `usuario_id` =  '" + id_user + "' AND `reim_id` = '" + id_reim + "' AND `actividad_id` =  '" + id_actividad + "');";
            query.ExecuteNonQuery();
            print(termino);
        }

    }

    public void FTimeRecompensa()
    {
        //print(inicio);
        
        if (SceneManager.GetActiveScene().name == "Actividad Tesoro")
        {
            id_actividad = 3005.ToString();
        }
        else if (SceneManager.GetActiveScene().name == "Actividad Drag and Drop")
        {
            id_actividad = 3006.ToString();
        }
        else if (SceneManager.GetActiveScene().name == "Actividad Laberinto")
        {
            id_actividad = 3004.ToString();
        }
        else if (SceneManager.GetActiveScene().name == "Actividad Lancha")
        {
            id_actividad = 3002.ToString();
        }
        else if (SceneManager.GetActiveScene().name == "Actividad suma")
        {
            id_actividad = 3003.ToString();
            print("alo");
        }
        else if (SceneManager.GetActiveScene().name == "Actividad Destreza")
        {
            id_actividad = 3007.ToString();
        }
        if (SceneManager.GetActiveScene().name != "Progreso" && SceneManager.GetActiveScene().name != "MainMenu")
        {
            termino = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffffff");
            MySqlCommand query = cnn.CreateCommand();

            query.CommandText = "UPDATE `ulearnet_reim_pilotaje`.`tiempoxactividad` SET `final` = '" + termino + "' , `causa` = '" + "2" + "' WHERE (`inicio` = '" + inicio + "' AND `usuario_id` =  '" + id_user + "' AND `reim_id` = '" + id_reim + "' AND `actividad_id` =  '" + id_actividad + "');";

            //UPDATE ulearnet_reim_pilotaje.tiempoxactividad SET final = "2019-12-09 16:40:49.243728" , causa = '2'  WHERE inicio = '2019-12-09 17:05:26.984223' AND usuario_id = '1'  AND reim_id = '2'  AND actividad_id = '14';
            //print(ahora);
            //print(termino);
            //


            query.ExecuteNonQuery();
        }

    }
    public void GameStart()
    {
        ahora = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        MySqlCommand query = cnn.CreateCommand();
        sesion_id = id_user + "-" + ahora;
        query.CommandText = "INSERT INTO `ulearnet_reim_pilotaje`.`asigna_reim_alumno` (`sesion_id`, `usuario_id`, `periodo_id`, `reim_id`, `datetime_inicio`, `datetime_termino`) VALUES ('"+ sesion_id +"', '" + id_user + "', '" + id_per+ "', '" + id_reim + "','" + ahora + "' , '" + ahora + "');";
        print("USUARIO: " + id_user);
        query.ExecuteNonQuery();
    }
    public void InsertQuery()
    {
        try
        {

            // comentar hasta var c para que no se caiga

            //var a = 1;
            //var b = 2;
            //var c = a / (a - 1);
            if (id_elemento!= "0" && id_actividad!="0")
            {
                print("ELEMENTO: " + id_elemento + " ACTIVIDAD: " + id_actividad + " CORRECTO: " + correcto);
                ahora = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffffff");
                MySqlCommand query = cnn.CreateCommand();
                query.CommandText = "INSERT INTO `ulearnet_reim_pilotaje`.`alumno_respuesta_actividad` (`id_per`, `id_user`, `id_reim`, `id_actividad`, `id_elemento`, `datetime_touch`, `fila`, `columna`, `correcta`) VALUES ('" + id_per + "', '" + id_user + "', '" + id_reim + "', '" + id_actividad + "', '" + id_elemento + "', '" + ahora + "', '" + posx + "', '" + posy + "','" + correcto + "');";
                query.ExecuteNonQuery();
                id_elemento = "0";
                correcto = "2";
                id_actividad = "0";
            }

        }
        catch(Exception ex)
        {   
            string path = @"/storage/emulated/0/android/data/cl.CalvoRodriguez.CleanOcean/Datos.txt";
            //string path = @"C:/Users/Alberto/Desktop/Clean Ocean Datos.txt";
            if (!File.Exists(path))
            {
                if (id_elemento!= "0" && id_actividad!="0")
                {
                    File.Create(path).Dispose();
                    TextWriter tw = new StreamWriter(path);
                    ahora = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffffff");
                    MySqlCommand query = cnn.CreateCommand();
                    query.CommandText = "INSERT INTO `ulearnet_reim_pilotaje`.`alumno_respuesta_actividad` (`id_per`, `id_user`, `id_reim`, `id_actividad`, `id_elemento`, `datetime_touch`, `fila`, `columna`, `correcta`) VALUES ('" + id_per + "', '" + id_user + "', '" + id_reim + "', '" + id_actividad + "', '" + id_elemento + "', '" + ahora + "', '" + posx + "', '" + posy + "','" + correcto + "');";
                    tw.WriteLine(query.CommandText);
                    tw.Close();
                    correcto = "2";
                    id_elemento = "0";
                    id_actividad = "0";
                }

            }
            else if (File.Exists(path))
            {
                if (id_elemento!= "0" && id_actividad!="0")
                {
                    using(var tw = new StreamWriter(path, true))
                    {
                        ahora = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffffff");
                        MySqlCommand query = cnn.CreateCommand();
                        query.CommandText = "INSERT INTO `ulearnet_reim_pilotaje`.`alumno_respuesta_actividad` (`id_per`, `id_user`, `id_reim`, `id_actividad`, `id_elemento`, `datetime_touch`, `fila`, `columna`, `correcta`) VALUES ('" + id_per + "', '" + id_user + "', '" + id_reim + "', '" + id_actividad + "', '" + id_elemento + "', '" + ahora + "', '" + posx + "', '" + posy + "','" + correcto + "');";
                        tw.WriteLine(query.CommandText);
                        id_elemento = "0";
                        correcto = "2";
                        id_actividad = "0";
                    }
                }
            }
        }
    }
}
