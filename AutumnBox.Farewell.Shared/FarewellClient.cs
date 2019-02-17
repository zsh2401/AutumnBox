using AutumnBox.Basic.Device;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace AutumnBox.Farewell
{
    static class FarewellClient
    {
        public static IConnection GetConnection(IDevice device)
        {
            if (connections.TryGetValue(device, out IConnection result))
            {
                return result;
            }
            else
            {
                return CreateNewConnection();
            }
        }
        private static IConnection CreateNewConnection()
        {
            throw new NotImplementedException();
        }
        private static readonly Dictionary<IDevice, IConnection> connections = new Dictionary<IDevice, IConnection>();
        internal static void RecordMe(IConnection connection, IDevice owner)
        {
            connections.Add(owner, connection);
        }
        internal static void RemoveMe(IConnection connection, IDevice owner)
        {
            connections.Remove(owner);
        }
    }
}
