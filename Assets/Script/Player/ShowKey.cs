using UnityEngine;

public class Showkey : MonoBehaviour
{
    private string teclaPressionada = "";

    // Update is called once per frame
    void Update()
    {
        // Verifica se qualquer tecla foi pressionada
        if (Input.anyKeyDown)
        {
            // Detecta qual tecla foi pressionada
            teclaPressionada = Input.inputString; // Captura a tecla pressionada
        }
    }

    // Exibe a tecla pressionada na tela
    void OnGUI()
    {
        // Exibe a tecla pressionada no centro da tela
        GUI.Label(new Rect(Screen.width / 2 - 50, Screen.height / 2 - 25, 100, 50), "Tecla: " + teclaPressionada);
    }
}
