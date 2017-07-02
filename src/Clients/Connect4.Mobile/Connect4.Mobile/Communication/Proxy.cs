using Connect4.Domain.Interfaces;
using Microsoft.AspNet.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connect4.Mobile.Communication
{
    public class Proxy : IProxy
    {
        public event EventHandler MoveReceived;
        private IHubProxy GameHub { get; }

        public Proxy()
        {
            var hubConnection = new HubConnection("http://10.0.1.60:49919/");
            GameHub = hubConnection.CreateHubProxy("GameHub");
            GameHub.On<int>("onMoved", column => OnMoveReceived(column));
            hubConnection.Start();
        }

        public bool Move(int column)
        {
            try
            {
                GameHub.Invoke("Move", new object[] { 1, 2 });
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                return false;
            }
            return true;
        }

        public void OnMoveReceived(int column)
        {
            Debug.WriteLine($"Move receinved: { column }");
            MoveReceived?.Invoke(this, null);
        }
    }
}
