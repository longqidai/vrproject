using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AccountManager : MonoBehaviour
{
    public DataConnector dataConnector;

    [Header("��¼")]
    public TMP_InputField userName_login_InputField;
    public TMP_InputField password_login_InputField;
    public Button loginBtn;

    [Header("ע��")]
    public TMP_InputField userName_register_InputField;
    public TMP_InputField password_register_InputField;
    public TMP_InputField confirmPassword_register_InputField;
    public Button registerBtn;

    private void Awake()
    {
        // 1Ϊ��¼��ť�󶨵���¼�
        loginBtn.onClick.AddListener(OnLoginBtnClick);
        registerBtn.onClick.AddListener(OnRegisterBtnClick);
        // ���ݵڶ���ͼ��δ��ʾע�ᰴť�󶨣���ͨ����Ҫ�󶨣��˴���ͼƬ��ʾ����
        // �����ע�ᰴť����ӣ�registerBtn.onClick.AddListener(OnRegisterBtnClick);
    }
    public void OnRegisterBtnClick()
    {
        // ֱ�ӵ������ݿ���֤
        Register();

        // ״̬������ͨ���¼����ƴ���
    }
    public void OnLoginBtnClick()
    {
        // ֱ�ӵ������ݿ���֤
        Login();

        // ״̬������ͨ���¼����ƴ���
    }

    public void Login()
    {
        // ����dataConnector��Login��������ȷ��dataConnector�Ѹ�ֵ��
        dataConnector.Login(userName_login_InputField.text, password_login_InputField.text);
    }

    private void Register()
    {
        string pw = password_register_InputField.text;
        string cpw = confirmPassword_register_InputField.text;
        if (pw.Equals(cpw))
        {
            dataConnector.InsertUser(userName_register_InputField.text, pw);
        }
        else
        {
            Debug.LogError("different Password!");
        }
    }
}
