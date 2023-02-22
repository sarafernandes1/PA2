using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Itens", menuName = "Itens Files Archiver")]
public class ItensDados : ScriptableObject
{
    public string nome;
    public Sprite imagem_objeto;
    public string description;
}
