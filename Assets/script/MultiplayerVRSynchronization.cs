
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class MultiplayerVRSynchronization : MonoBehaviour, IPunObservable
{
    private PhotonView m_PhotonView;

    //Main VRPlayer Transform Synch
    [Header("Main VRPlayer Transform Synch")]
    public Transform generalVRPlayerTransform;

    //Position
    private float m_Distance_GeneralVRPlayer;
    private Vector3 m_Direction_GeneralVRPlayer;
    private Vector3 m_NetworkPosition_GeneralVRPlayer;
    private Vector3 m_StoredPosition_GeneralVRPlayer;

    //Rotation
    private Quaternion m_NetworkRotation_GeneralVRPlayer;
    private float m_Angle_GeneralVRPlayer;

    //Main Avatar Transform Synch
    [Header("Main Avatar Transform Synch")]
    public Transform mainAvatarTransform;

    //Position
    private float m_Distance_MainAvatar;
    private Vector3 m_Direction_MainAvatar;
    private Vector3 m_NetworkPosition_MainAvatar;
    private Vector3 m_StoredPosition_MainAvatar;

    //Rotation
    private Quaternion m_NetworkRotation_MainAvatar;
    private float m_Angle_MainAvatar;

    //Head Synchron
    [Header("Avatar Head Transform Synch")]
    public Transform headTransform;

    //Rotation
    private Quaternion m_NetworkRotation_Head;
    private float m_Angle_Head;

    //Body Synchron
    [Header("Avatar Body Transform Synch")]
    public Transform bodyTransform;

    //Rotation
    private Quaternion m_NetworkRotation_Body;
    private float m_Angle_Body;

    //Hands Synchron
    [Header("Hands Transform Synch")]
    public Transform leftHandTransform;
    public Transform rightHandTransform;

    //Left Hand Synchron
    //Position
    private float m_Distance_LeftHand;
    private Vector3 m_Direction_LeftHand;
    private Vector3 m_NetworkPosition_LeftHand;
    private Vector3 m_StoredPosition_LeftHand;

    //Rotation
    private Quaternion m_NetworkRotation_LeftHand;
    private float m_Angle_LeftHand;

    //Right Hand Synch
    //Position
    private float m_Distance_RightHand;
    private Vector3 m_Direction_RightHand;
    private Vector3 m_NetworkPosition_RightHand;
    private Vector3 m_StoredPosition_RightHand;

    //Rotation
    private Quaternion m_NetworkRotation_RightHand;
    private float m_Angle_RightHand;

    private bool m_firstTake = false;


    void Awake()
    {
        m_PhotonView = GetComponent<PhotonView>();

        //Main VRPlayer Synch Init
        m_StoredPosition_GeneralVRPlayer = generalVRPlayerTransform.position;
        m_NetworkPosition_GeneralVRPlayer = Vector3.zero;
        m_NetworkRotation_GeneralVRPlayer = Quaternion.identity;
        m_Angle_GeneralVRPlayer = 0f;

        //Main Avatar Synchron Init
        m_StoredPosition_MainAvatar = mainAvatarTransform.localPosition;
        m_NetworkPosition_MainAvatar = Vector3.zero;
        m_NetworkRotation_MainAvatar = Quaternion.identity;
        m_Angle_MainAvatar = 0f;

        //Head Synchron Init
        m_NetworkRotation_Head = Quaternion.identity;
        m_Angle_Head = 0f;

        //Body Synchron Init
        m_NetworkRotation_Body = Quaternion.identity;
        m_Angle_Body = 0f;

        //Left Hand Synchron Init
        m_StoredPosition_LeftHand = leftHandTransform.localPosition;
        m_NetworkPosition_LeftHand = Vector3.zero;
        m_NetworkRotation_LeftHand = Quaternion.identity;
        m_Angle_LeftHand = 0f;

        //Right Hand Synchron Init
        m_StoredPosition_RightHand = rightHandTransform.localPosition;
        m_NetworkPosition_RightHand = Vector3.zero;
        m_NetworkRotation_RightHand = Quaternion.identity;
        m_Angle_RightHand = 0f;
    }

    void OnEnable()
    {
        m_firstTake = true;
    }

    void Update()
    {
        if (!this.m_PhotonView.IsMine)
        {
            // General VRPlayer position interpolation
            generalVRPlayerTransform.position = Vector3.MoveTowards(
            generalVRPlayerTransform.position,
            this.m_NetworkPosition_GeneralVRPlayer,
            this.m_Distance_GeneralVRPlayer * Time.deltaTime
            );

            // General VRPlayer rotation interpolation
            generalVRPlayerTransform.rotation = Quaternion.RotateTowards(
            generalVRPlayerTransform.rotation,
            this.m_NetworkRotation_GeneralVRPlayer,
            this.m_Angle_GeneralVRPlayer * Time.deltaTime
            );

            // Main Avatar position interpolation
            mainAvatarTransform.localPosition = Vector3.MoveTowards(
            mainAvatarTransform.localPosition,
            this.m_NetworkPosition_MainAvatar,
            this.m_Distance_MainAvatar * Time.deltaTime
            );

            // Main Avatar rotation interpolation
            mainAvatarTransform.localRotation = Quaternion.RotateTowards(
            mainAvatarTransform.localRotation,
            this.m_NetworkRotation_MainAvatar,
            this.m_Angle_MainAvatar * Time.deltaTime
            );

            // Head rotation interpolation (only rotation)
            headTransform.localRotation = Quaternion.RotateTowards(
            headTransform.localRotation,
            this.m_NetworkRotation_Head,
            this.m_Angle_Head * Time.deltaTime
            );

            // Body rotation interpolation (only rotation)
            bodyTransform.localRotation = Quaternion.RotateTowards(
            bodyTransform.localRotation,
            this.m_NetworkRotation_Body,
            this.m_Angle_Body * Time.deltaTime
            );

            // Left Hand position interpolation
            leftHandTransform.localPosition = Vector3.MoveTowards(
            leftHandTransform.localPosition,
            this.m_NetworkPosition_LeftHand,
            this.m_Distance_LeftHand * Time.deltaTime
            );

            // Left Hand rotation interpolation
            leftHandTransform.localRotation = Quaternion.RotateTowards(
            leftHandTransform.localRotation,
            this.m_NetworkRotation_LeftHand,
            this.m_Angle_LeftHand * Time.deltaTime
            );

            // Right Hand position interpolation
            rightHandTransform.localPosition = Vector3.MoveTowards(
            rightHandTransform.localPosition,
            this.m_NetworkPosition_RightHand,
            this.m_Distance_RightHand * Time.deltaTime
            );

            // Right Hand rotation interpolation
            rightHandTransform.localRotation = Quaternion.RotateTowards(
            rightHandTransform.localRotation,
            this.m_NetworkRotation_RightHand,
            this.m_Angle_RightHand * Time.deltaTime
            );
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            // 发送 Main VRPlayer 数据
            this.m_Direction_GeneralVRPlayer = generalVRPlayerTransform.position - this.m_Direction_GeneralVRPlayer;
            this.m_StoredPosition_GeneralVRPlayer = generalVRPlayerTransform.position;
            stream.SendNext(generalVRPlayerTransform.position);
            stream.SendNext(this.m_Direction_GeneralVRPlayer);
            stream.SendNext(generalVRPlayerTransform.position);

            // 发送 Main Avatar 数据
            this.m_Direction_MainAvatar = mainAvatarTransform.localPosition - this.m_StoredPosition_MainAvatar;
            this.m_StoredPosition_MainAvatar = mainAvatarTransform.localPosition;
            stream.SendNext(mainAvatarTransform.localPosition);
            stream.SendNext(this.m_Direction_MainAvatar);
            stream.SendNext(mainAvatarTransform.localRotation);

            // 发送 Head 数据
            stream.SendNext(headTransform.localRotation);

            // 发送 Body 数据
            stream.SendNext(bodyTransform.localRotation);

            // 发送 Left Hand 数据
            this.m_Direction_LeftHand = leftHandTransform.localPosition - this.m_StoredPosition_LeftHand;
            this.m_StoredPosition_LeftHand = leftHandTransform.localPosition;
            stream.SendNext(leftHandTransform.localPosition);
            stream.SendNext(this.m_Direction_LeftHand);
            stream.SendNext(leftHandTransform.localRotation);

            // 发送 Right Hand 数据
            this.m_Direction_RightHand = rightHandTransform.localPosition - this.m_StoredPosition_RightHand;
            this.m_StoredPosition_RightHand = rightHandTransform.localPosition;
            stream.SendNext(rightHandTransform.localPosition);
            stream.SendNext(this.m_Direction_RightHand);
            stream.SendNext(rightHandTransform.localRotation);
        }
        else
        {
            // 接收 Main VRPlayer 数据
            m_NetworkPosition_GeneralVRPlayer = (Vector3)stream.ReceiveNext();
            m_NetworkRotation_GeneralVRPlayer = (Quaternion)stream.ReceiveNext();

            // 计算位置距离用于插值
            m_Distance_GeneralVRPlayer = Vector3.Distance(
                generalVRPlayerTransform.position,
                m_NetworkPosition_GeneralVRPlayer
            );

            // 计算角度差用于插值
            m_Angle_GeneralVRPlayer = Quaternion.Angle(
                generalVRPlayerTransform.rotation,
                m_NetworkRotation_GeneralVRPlayer
            );

            // 接收 Main Avatar 数据
            m_NetworkPosition_MainAvatar = (Vector3)stream.ReceiveNext();
            m_NetworkRotation_MainAvatar = (Quaternion)stream.ReceiveNext();

            m_Distance_MainAvatar = Vector3.Distance(
                mainAvatarTransform.localPosition,
                m_NetworkPosition_MainAvatar
            );

            m_Angle_MainAvatar = Quaternion.Angle(
                mainAvatarTransform.localRotation,
                m_NetworkRotation_MainAvatar
            );

            // 接收 Head 数据
            m_NetworkRotation_Head = (Quaternion)stream.ReceiveNext();
            m_Angle_Head = Quaternion.Angle(
                headTransform.localRotation,
                m_NetworkRotation_Head
            );

            // 接收 Body 数据
            m_NetworkRotation_Body = (Quaternion)stream.ReceiveNext();
            m_Angle_Body = Quaternion.Angle(
                bodyTransform.localRotation,
                m_NetworkRotation_Body
            );

            // 接收 Left Hand 数据
            m_NetworkPosition_LeftHand = (Vector3)stream.ReceiveNext();
            m_NetworkRotation_LeftHand = (Quaternion)stream.ReceiveNext();

            m_Distance_LeftHand = Vector3.Distance(
                leftHandTransform.localPosition,
                m_NetworkPosition_LeftHand
            );

            m_Angle_LeftHand = Quaternion.Angle(
                leftHandTransform.localRotation,
                m_NetworkRotation_LeftHand
            );

            // 接收 Right Hand 数据
            m_NetworkPosition_RightHand = (Vector3)stream.ReceiveNext();
            m_NetworkRotation_RightHand = (Quaternion)stream.ReceiveNext();

            m_Distance_RightHand = Vector3.Distance(
                rightHandTransform.localPosition,
                m_NetworkPosition_RightHand
            );

            m_Angle_RightHand = Quaternion.Angle(
                rightHandTransform.localRotation,
                m_NetworkRotation_RightHand
            );

            // 第一次接收数据时，直接设置目标值避免跳跃
            if (m_firstTake)
            {
                generalVRPlayerTransform.position = m_NetworkPosition_GeneralVRPlayer;
                generalVRPlayerTransform.rotation = m_NetworkRotation_GeneralVRPlayer;
                mainAvatarTransform.localPosition = m_NetworkPosition_MainAvatar;
                mainAvatarTransform.localRotation = m_NetworkRotation_MainAvatar;
                headTransform.localRotation = m_NetworkRotation_Head;
                bodyTransform.localRotation = m_NetworkRotation_Body;
                leftHandTransform.localPosition = m_NetworkPosition_LeftHand;
                leftHandTransform.localRotation = m_NetworkRotation_LeftHand;
                rightHandTransform.localPosition = m_NetworkPosition_RightHand;
                rightHandTransform.localRotation = m_NetworkRotation_RightHand;

                m_firstTake = false;
            }
        }
    }

    // 关键修复点：访问修饰符添加
    private float QuaternionAngle(Quaternion a, Quaternion b)
    {
        return Quaternion.Angle(a, b);
    }

    // 关键修复点：添加访问修饰符
    [Header("Smoothing Parameters")]
    [Range(0.1f, 5f)]
    private float positionSmoothingSpeed = 2f;

    [Range(0.1f, 5f)]
    private float rotationSmoothingSpeed = 2f;
} // 类结束大括号
