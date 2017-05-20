<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Connect4.Server.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Signalr Chat Messenger</title>
    <script src="Scripts/jquery-1.6.4.js"></script>
    <script src="Scripts/jquery.signalR-2.2.2.js"></script>
    <script type="text/javascript" src="/signalr/hubs"></script>
</head>
<body>
    <form id="form1" runat="server">
    <script type="text/javascript">
        $(function () {

            var hub = $.connection.gameHub;

            hub.client.onRegisteredPlayer = function (message) {
                $('#playerId').html('PlayerId=' + message);
            };
            $("#registerPlayer").click(function () {
                hub.server.registerPlayer();
            });

            hub.client.onMoved = function (column) {
                $('#moveList').append('<li>Move=' + column + '</li>');
            };
            $("#sendMove").click(function () {
                hub.server.move(0, $('#column').val());
            });

            $.connection.hub.start();
        });
    </script>
    <div>
        <p>
            <input type="button" id="registerPlayer" value="Register Player" />
            <label id="playerId" />
        </p>

        <input type="text" id="column" />
        <input type="button" id="sendMove" value="Move" />
        <ul id="moveList"></ul>
    </div>
    </form> 
</body>
</html>