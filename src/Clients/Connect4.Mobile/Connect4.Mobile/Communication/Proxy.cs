using Connect4.Domain.EventArguments;
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
        public event EventHandler<MoveEventArgs> MoveReceived;
        public event EventHandler GameStarted;
        private IHubProxy GameHub { get; }

        public Proxy()
        {
            var hubConnection = new HubConnection("http://10.0.1.60:49919/");
            GameHub = hubConnection.CreateHubProxy("GameHub");
            GameHub.On<int>("onMoved", column => OnMoveReceived(column));
            GameHub.On<bool>("onGameStarted", goesFirst => OnGameStarted(goesFirst));
            hubConnection.Start();
        }

        public void GameRequest()
        {
            try
            {
                GameHub.Invoke("GameRequest");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
        }

        public void CancelRequest()
        {
            try
            {
                GameHub.Invoke("CancelRequest");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
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

        public void OnGameStarted(bool goesFirst)
        {
            Debug.WriteLine($"Game Started. Move First? { goesFirst }");
            GameStarted?.Invoke(this, null);
        }
    }
}
