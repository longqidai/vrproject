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
            if (enabled) // ���������δ����ʱ����
            {
                SetText();
            }
        }

        void SetText()
        {
            // ���ϸ�Ŀ�ֵ��飨��������Ϊ�պ�Ԫ��Ϊ�յ������
            if (m_VersionTextComponents == null || m_VersionTextComponents.Length == 0)
            {
                Utils.Log("VersionText: Missing Text components", 2);
                return;
            }

            string versionText = $"{m_Prefix}{Application.version}{m_Suffix}";

            foreach (TMP_Text t in m_VersionTextComponents)
            {
                // �ؼ��޸���ȷ��Ԫ�ز�Ϊ�գ�
                if (t != null && t.isActiveAndEnabled)
                {
                    t.text = versionText;
                }
                else
                {
                    // �ṩ������Ĵ�����Ϣ
                    string objName = t != null ? t.name : "[null element]";
                    Debug.LogWarning($"VersionText: Found invalid text component '{objName}'", this);
                }
            }
        }
    }
}
