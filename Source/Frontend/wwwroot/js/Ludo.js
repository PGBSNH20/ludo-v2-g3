const connection = new signalR.HubConnectionBuilder()
    .withUrl('https://localhost:44303/ludo', {
        skipNegotiation: true,
        transport: signalR.HttpTransportType.WebSockets
    })
    .configureLogging(signalR.LogLevel.Information)
    .build();

var me = "";

async function connect() {
    try {
        await connection.start();

        me = window.prompt("enter name");

        connection.invoke("JoinRoom", name, getCurrentGuid);

        console.log("SignalR connection established to [" + getCurrentGuid + "] as [" + name + "]");
    } catch (err) {
        console.log(err);
    }
}

connection.on("RecieveDiceRoll", function (num, player) {
    document.getElementById("diceValue").innerText = player + " rolled " + num + "!";
    document.getElementById("diceVisual").src = "/img/dice-" + num + ".png";
});

connection.on("UpdatePlayerTurn", function (player) {
    document.getElementById("diceBtn").enabled = player == me;
    document.getElementById("currentPlayer").innerText = "Current Player: " + player;
});