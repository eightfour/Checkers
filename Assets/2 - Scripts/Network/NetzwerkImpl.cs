using UnityEngine;
using System.Net.Sockets;
using System.Net;
using System;
using System.Text;
using System.Threading;
using System.Linq;
using Logik;


public enum OperationMode
{
    Server,
    Client
}

public class NetzwerkImpl : MonoBehaviour, INetzwerk
{
    // Incoming data from the client.  
    string data;

    volatile bool stopThread = false;

    //And now the threading begins :) !
    Thread t, t1;

    Socket serverSocket;
    Socket clientSocket;

    Spiel spiel;
    OperationMode operationMode;
   // Network network;


    //start network capability
    public void Starten(Spiel spiel, OperationMode operationMode) //, Network network)
    {
        if (spiel != null)
        {
            this.spiel = spiel;
            this.operationMode = operationMode;
          //  this.network = network;

            if (operationMode == OperationMode.Server)
            {
                t = new Thread(CreateGame) { Name = "Network Server Thread" };
                t.Start();
            }
            else if (operationMode == OperationMode.Client)
            {
                t1 = new Thread(JoinGame) { Name = "Network Client Thread" };
                t1.Start();
            }
        }
        else
        {
            Debug.Log("Unable to start network because instance of spiel is null!");
        }
    }

    //send a message to the other partner
    public void Send(string payload)
    {
        if (operationMode == OperationMode.Server)
        {
            if (serverSocket != null && serverSocket.Connected)
                serverSocket.Send(Encoding.ASCII.GetBytes(payload + ">"));
        }
        else if (operationMode == OperationMode.Client)
        {
            if (clientSocket != null && clientSocket.Connected)
                clientSocket.Send(Encoding.ASCII.GetBytes(payload + ">"));
        }
        else
        {
            Debug.Log("Unable to send due to missing socket connection. Have you invoked Start() function?");
        }
    }

    //host a gamename server and listen for incoming connections
    private void CreateGame()
    {
        // Data buffer for incoming data.  
        byte[] bytes = new Byte[1024];

        try
        {
            // Establish the local endpoint for the socket.  
            // Dns.GetHostName returns the name of the   
            // host running the application.  
            var ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
            var ipAddress = ipHostInfo.AddressList.FirstOrDefault(a => a.AddressFamily == AddressFamily.InterNetwork);
			var localEndPoint = new IPEndPoint(ipAddress, 11000);

            // Create a TCP/IP socket.  
            var listener = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

            listener.Bind(localEndPoint);
            listener.Listen(10);

            Debug.Log("[SERVER] Waiting for a connection...");
            // Program is suspended while waiting for an incoming connection.  
            serverSocket = listener.Accept();

            while (!stopThread)
            {
                data = null;

                // An incoming connection needs to be processed.  
                while (!stopThread)
                {
                    int bytesRec = serverSocket.Receive(bytes);
                    data += Encoding.ASCII.GetString(bytes, 0, bytesRec);
                    if (data.IndexOf(">") > -1)
                    {
                        break;
                    }
                }
                // Show the data on the console.
                if (!String.IsNullOrEmpty(data))
                {
                    Debug.Log("[SERVER] Received: " + data);
                    spiel.ProcessNetworkPayload(data);
                }
            }
        }
        catch (Exception ex)
        {
            Debug.Log(ex.ToString());
        }
    }

    //joins a game with the ip address specified in InputText
    private void JoinGame()
    {
        // Data buffer for incoming data.  
        byte[] bytes = new Byte[1024];

        try
        {
            // Establish the local endpoint for the socket.  
            // Dns.GetHostName returns the name of the   
            // host running the application.  
            var ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
            var ipAddress = ipHostInfo.AddressList.FirstOrDefault(a => a.AddressFamily == AddressFamily.InterNetwork);
            var localEndPoint = new IPEndPoint(ipAddress, 11000);

            // Create a TCP/IP socket.  
            clientSocket = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

            try
            {
                clientSocket.Connect(NetworkModel.ip, 11000);
                // Encode the data string into a byte array.

                if (clientSocket.Connected)
                {
                    byte[] msg = Encoding.ASCII.GetBytes("join;client>");
                    int bytesSent = clientSocket.Send(msg);
                    string clientReceive = null;

                    while (!stopThread)
                    {
                        clientReceive = null;
                        var bytesReceived = clientSocket.Receive(bytes);

                        while (!stopThread)
                        {
                            clientReceive += Encoding.ASCII.GetString(bytes, 0, bytesReceived);

                            if (clientReceive.IndexOf(">") > -1)
                                break;
                        }
                        if (!String.IsNullOrEmpty(clientReceive))
                        {
                            Debug.Log("[CLIENT] Received: " + clientReceive);
                            spiel.ProcessNetworkPayload(clientReceive);
                        }
                    }
                }
            }
            catch (ArgumentNullException ane)
            {
                Debug.Log("Argument Null Exception" + ane.ToString());
            }
            catch (SocketException se)
            {
                Debug.Log("Socket Exception:" + se.ToString());
            }
            catch (Exception e)
            {
                Debug.Log("Generic exception: " + e.ToString());
            }
        }
        catch (Exception ex)
        {
            Debug.Log(ex.ToString());
        }
    }

    public void Disconnect()
    {
        stopThread = true;

        if (serverSocket != null)
        {
            serverSocket.Shutdown(SocketShutdown.Both);
            serverSocket.Close();
        }
        if (clientSocket != null)
        {
            clientSocket.Shutdown(SocketShutdown.Both);
            clientSocket.Close();
        }
    }
}