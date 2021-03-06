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
        var currentPlayer = document.getElementById("currentPlayer").innerText.replace("Current player: ", "");
        document.getElementById("diceBtn").disabled = currentPlayer.toLowerCase() != me.toLowerCase();
        document.getElementById("diceBtn").style.backgroundColor = currentPlayer.toLowerCase() != me.toLowerCase() ? "#f44336" : "#4CAF50";

        console.log("SignalR connection established to [" + getCurrentGuid + "] as [" + me + "].");
    } catch (err) {
        console.log(err);
    }
}

connection.on("RecieveDiceRoll", function (num, player) {
    document.getElementById("diceValue").innerText = player + " rolled " + num + "!";
    document.getElementById("diceVisual").src = "/img/dice-" + num + ".png";
});

connection.on("UpdatePlayerTurn", function (player) {
    document.getElementById("diceBtn").disabled = player.toLowerCase() != me.toLowerCase();
    document.getElementById("diceBtn").style.backgroundColor = player.toLowerCase() != me.toLowerCase() ? "#f44336" : "#4CAF50";
    document.getElementById("currentPlayer").innerText = "Current player: " + player;
});

connection.on("UpdateGameState", function (pawns) {
    var data = JSON.parse(pawns);

    var coll = document.getElementsByClassName("pawn");

    for (let i = coll.length - 1; i >= 0; i--) {
        coll[i].remove();
    }

    for (playerPawns of data) {
        for (pawn of playerPawns) {
            PositionPawn(pawn['Position'], pawn['IsInNest'], pawn['AtFinishLine'], pawn['IsFinished'], pawn['Color'], pawn['ID'], getCurrentGuid);
        }
    }
});