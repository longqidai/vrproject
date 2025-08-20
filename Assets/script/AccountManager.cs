using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AccountManager : MonoBehaviour
{
    public DataConnector dataConnector;

    [Header("登录")]
    public TMP_InputField userName_login_InputField;
    public TMP_InputField password_login_InputField;
    public Button loginBtn;

    [Header("注册")]
    public TMP_InputField userName_register_InputField;
    public TMP_InputField password_register_InputField;
    public TMP_InputField confirmPassword_register_InputField;
    public Button registerBtn;

    private void Awake()
    {
        // 1为登录按钮绑定点击事件
        loginBtn.onClick.AddListener(OnLoginBtnClick);

        // 根据第二张图，未显示注册按钮绑定，但通常需要绑定，此处按图片显示保留
        // 若需绑定注册按钮可添加：registerBtn.onClick.AddListener(OnRegisterBtnClick);
    }

    public void OnLoginBtnClick()
    {
        // 直接调用数据库验证
        bool isValid = dataConnector.Login(
            userName_login_InputField.text,
            password_login_InputField.text
        );

        // 状态更新已通过事件机制处理
    }

    public void Login()
    {
        // 调用dataConnector的Login方法（需确保dataConnector已赋值）
        dataConnector.Login(userName_login_InputField.text, password_login_InputField.text);
    }

    // 注意：第二张图中未显示注册方法，根据第一张图，注册部分UI元素存在但未实现功能
    // 以下注册方法为可选添加（图片中未出现，根据常理补充）
    /*
    private void OnRegisterBtnClick()
    {
        // 实现注册逻辑
        // 示例：dataConnector.Register(...);
    }
    */
}
