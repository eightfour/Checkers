using Logik;


public interface INetzwerk
{
    void Starten(Spiel spiel, OperationMode operationMode); //, Network network);

    void Send(string payload);

    void Disconnect();
}
