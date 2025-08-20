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

        // ���ݵڶ���ͼ��δ��ʾע�ᰴť�󶨣���ͨ����Ҫ�󶨣��˴���ͼƬ��ʾ����
        // �����ע�ᰴť����ӣ�registerBtn.onClick.AddListener(OnRegisterBtnClick);
    }

    public void OnLoginBtnClick()
    {
        // ֱ�ӵ������ݿ���֤
        bool isValid = dataConnector.Login(
            userName_login_InputField.text,
            password_login_InputField.text
        );

        // ״̬������ͨ���¼����ƴ���
    }

    public void Login()
    {
        // ����dataConnector��Login��������ȷ��dataConnector�Ѹ�ֵ��
        dataConnector.Login(userName_login_InputField.text, password_login_InputField.text);
    }

    // ע�⣺�ڶ���ͼ��δ��ʾע�᷽�������ݵ�һ��ͼ��ע�Ჿ��UIԪ�ش��ڵ�δʵ�ֹ���
    // ����ע�᷽��Ϊ��ѡ��ӣ�ͼƬ��δ���֣����ݳ����䣩
    /*
    private void OnRegisterBtnClick()
    {
        // ʵ��ע���߼�
        // ʾ����dataConnector.Register(...);
    }
    */
}
