using System.Collections.Generic;

namespace LokiRAT
{
    public class ClientInfo
    {
        public string id;
        public string ipAddress;
        public string compName;
        public string operatingSystem;
        public string ramMemory;
        public string processor;
        public string webcam;
        public string interval;
        public string lastUpdate;
    }

    class Global
    {
        public static string Url = "";
        public static string Key = "";
        public static string Cert = "";

        public static List<ClientInfo> ClientList = new List<ClientInfo>();

        public static Dictionary<string, string> lastCommand = new Dictionary<string, string>();
        public static Dictionary<string, string> lastCommandText = new Dictionary<string, string>();
        public static Dictionary<string, List<string>> idCommands = new Dictionary<string, List<string>>();

        public static bool ClientExists(string id)
        {
            lock (ClientList)
            {
                foreach (var client in ClientList)
                {
                    if (client.id == id)
                        return true;
                }
                return false;
            }
        }

        public static void ClientRemove(string id)
        {
            lock (ClientList)
            {
                foreach (var client in ClientList)
                {
                    if (client.id == id)
                    {
                        ClientList.Remove(client);
                        break;
                    }
                }
            }
        }

        public static ClientInfo GetClientById(string id)
        {
            lock (ClientList)
            {
                foreach (var client in ClientList)
                {
                    if (client.id == id)
                        return client;
                }
                return null;
            }
        }

        public static void AddCommandToList(string id, string cmd)
        {
            lock (idCommands)
            {
                idCommands[id].Add(cmd);
                lastCommand[id] = cmd;
            }
        }

        public static void AddCommandToAllList(string cmd)
        {
            lock (ClientList)
            lock (idCommands)
            {
                foreach (var client in ClientList)
                {
                    idCommands[client.id].Add(cmd);
                    lastCommand[client.id] = cmd;
                }
            }
        }

        public static string GetCommandFromList(string id)
        {
            lock (idCommands)
            {
                if (idCommands[id].Count > 0)
                {
                    string cmd = idCommands[id][0];
                    idCommands[id].RemoveAt(0);
                    return cmd;
                }
                else
                    return null;
            }
        }

        public static string GetLastCommandText(string id)
        {
            lock (lastCommandText)
            {
                string text = lastCommandText[id];
                lastCommandText[id] = "";
                return text;
            }
        }
    }
}