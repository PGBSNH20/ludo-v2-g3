const connection = new signalR.HubConnectionBuilder()
    .withUrl('https://localhost:44354', {
        skipNegotiation: true,
        transport: signalR.HttpTransportType.WebSockets
    })
    .configureLogging(signalR.LogLevel.Information)
    .build();

async function connect() {
    try {
        await connection.start();
        console.log("SignalR connection established.");
    } catch (err) {
        console.log(err);
    }
}

connection.on("recieveDiceRoll", function (num) {
    document.getElementById("rollInfo").innerText = "Player rolled " + num + "!";
});