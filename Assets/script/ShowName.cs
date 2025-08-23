using Photon.Pun;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Photon.Realtime;




public class ShowName : MonoBehaviour
{
    public TextMeshPro PlayerName_InputField;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PlayerName_InputField.text = PhotonNetwork.NickName;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
