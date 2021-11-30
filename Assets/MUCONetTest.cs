using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Phenomenal.MUCONet;

public class MUCONetTest : MonoBehaviour
{
    public MUCOClient Client;

    enum ServerPackets : int
    {
        HelloFromServer
    }

    enum ClientPackets : int
    {
        HelloFromClient
    }

    private void Start()
    {
        MUCOLogger.LogEvent += Log;

        Client = new MUCOClient();
        Client.RegisterPacketHandler((int)ServerPackets.HelloFromServer, HandleHelloFromServer);
        Client.Connect("127.0.0.1", 1000);
    }

    private void HandleHelloFromServer(MUCOPacket packet)
    {
        Debug.Log("HelloFromServer");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            MUCOPacket packet = new MUCOPacket((int)ClientPackets.HelloFromClient);
            Client.SendPacket(packet, true);
        }
    }

    private void Log(MUCOLogMessage message)
    {
        Debug.Log(message.Message);
    }
}