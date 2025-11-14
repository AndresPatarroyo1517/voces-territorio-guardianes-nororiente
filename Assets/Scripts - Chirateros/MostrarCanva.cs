using UnityEngine;

public class MostrarCanva : MonoBehaviour
{
    public GameObject canvasMensaje; // Arrastra aquí tu Canvas o Panel

    void Start()
    {
        if (canvasMensaje != null)
            canvasMensaje.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            MostrarCanvasMensaje();

            
            // Desactivamos este trigger para que no vuelva a activarse
            gameObject.SetActive(false);
        }
    }

    void MostrarCanvasMensaje()
    {
        if (canvasMensaje != null)
        {
            canvasMensaje.SetActive(true);
            Invoke(nameof(OcultarCanvasMensaje), 6f);
        }
    }

    void OcultarCanvasMensaje()
    {
        if (canvasMensaje != null)
            canvasMensaje.SetActive(false);
    }
}
