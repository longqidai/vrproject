using TMPro;
using UnityEngine;

namespace XRMultiplayer
{
    public class VersionText : MonoBehaviour
    {
        [SerializeField] TMP_Text[] m_VersionTextComponents;
        [SerializeField] string m_Prefix = "v";
        [SerializeField] string m_Suffix = "";

        void Start()
        {
            SetText();
        }

        private void OnValidate()
        {
            if (enabled) // 避免在组件未启用时调用
            {
                SetText();
            }
        }

        void SetText()
        {
            // 更严格的空值检查（包括数组为空和元素为空的情况）
            if (m_VersionTextComponents == null || m_VersionTextComponents.Length == 0)
            {
                Utils.Log("VersionText: Missing Text components", 2);
                return;
            }

            string versionText = $"{m_Prefix}{Application.version}{m_Suffix}";

            foreach (TMP_Text t in m_VersionTextComponents)
            {
                // 关键修复：确保元素不为空！
                if (t != null && t.isActiveAndEnabled)
                {
                    t.text = versionText;
                }
                else
                {
                    // 提供更具体的错误信息
                    string objName = t != null ? t.name : "[null element]";
                    Debug.LogWarning($"VersionText: Found invalid text component '{objName}'", this);
                }
            }
        }
    }
}
