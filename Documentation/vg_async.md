## Hur används [SignalR](https://dotnet.microsoft.com/apps/aspnet/signalr) i detta ludospel?

* Användaren laddar in webbsidan och blir uppmanad att ange sessionsnamn och blir ansluten till SignalR hubben.
* När användaren ansluter till signalR hubben skickas sessions ID't till servern vilket lägger till den sessionen i en hub [Group](https://docs.microsoft.com/en-us/aspnet/signalr/overview/guide-to-the-api/working-with-groups) om sessionen inte redan finns i en grupp. Detta säkerställer att alla method invocations skickas till rätt klienter.
* Användaren blir ansluten som det namnet de angav i början, vilket bör vara ett av namnen som är knutna till den här spel sessionen.
* När en användare trycker på "Roll Dice" knappen så hämtar vi ett slumpat nummer från 1-6 från API:et och den anropar "SendDiceRoll" metoden på servern vilket i sin tur anropar funktioen "RecieveDiceRoll" på alla klienter i den gruppen (Sessions ID).

Server-side Metod för tärningssynkning 
```csharp
        public async Task SendDiceRoll(int roll, string ludoId)
        {
            //Get the session that the user sent request from
            IGameSession session = _dbContext.GameSessions
                .Include(gs => gs.Players)
                .First(x => x.Id == Guid.Parse(ludoId));

            //Get the current players turn so we can display who sent the roll 
            Player player = 
                session.CurrentPlayer == 0 ? 
                    session.Players.Last() : 
                    session.Players[session.CurrentPlayer - 1];

            //Invoke RecieveDiceRoll(roll, name) on all clients in the specified game session
            await Clients.Group(ludoId).SendAsync("RecieveDiceRoll", roll, player.Name);
        }
```
Client-side Funktion för tärningssynkning 
```js
connection.on("RecieveDiceRoll", function (num, player) {
    //update the diceRoll id to display "[player] rolled [num]!"
    document.getElementById("diceValue").innerText = player + " rolled " + num + "!";
    //update the image value to match the dice roll (so the user can see a picture of a dice with x value)
    document.getElementById("diceVisual").src = "/img/dice-" + num + ".png";
});
```


* Efter att tärningen rullats så måste vi uppdatera vems tur det är på alla klienter, vi gör detta genom att anropa "UpdatePlayerTurn" funktionen på alla klienter.

Server-side metod för spelarrotationen.
```csharp
        public async Task UpdateNextPlayerTurn(string ludoId)
        {
            //Get the relevant session
            IGameSession session = _dbContext.GameSessions
                .Include(gs => gs.Players)
                .First(x => x.Id == Guid.Parse(ludoId));
            //get the current player turn so we can update all clients with this value
            Player playerTurn = session.Players[session.CurrentPlayer];

            await Clients.Group(ludoId.ToString()).SendAsync("UpdatePlayerTurn", playerTurn.Name);
        }
```
Client-side function för spelarrotationen.
```js
connection.on("UpdatePlayerTurn", function (player) {
    //if the name specified in the parameter is not ours the button should be disabled so we can't interact with it
    document.getElementById("diceBtn").disabled = player.toLowerCase() != me.toLowerCase();
    //and if its not our turn the button color should be red, green if its our turn
    document.getElementById("diceBtn").style.backgroundColor = player.toLowerCase() != me.toLowerCase() ? "#f44336" : "#4CAF50";
    //display who's turn it is
    document.getElementById("currentPlayer").innerText = "Current Player: " + player;
});
```

* För att kunna uppdatera spelet visuellt på alla klienter så rensar vi alla pjäser och återskapar dem med deras nya position. All pjäs-logik sker i bakgrunden på servern men vi behöver fortfarande uppdatera dem på klienten så att alla är synkade.

Anropa UpdateGameState funktionen på alla klienter i den angivna sessionen, vilket tvingar alla klienter i det rummet att uppdatera spelbrädans state.
```js
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
```



## Flowcharts

![](https://i.imgur.com/C10eFzy.png)

![](https://i.imgur.com/zCS5qSi.png)
