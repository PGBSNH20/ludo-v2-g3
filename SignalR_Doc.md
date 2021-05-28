## How does [SignalR](https://dotnet.microsoft.com/apps/aspnet/signalr) work in this Ludo game?

* User loads into the webpage and gets prompted to set their session name.
* When the user connects to the SignalR Ludo Hub it sends the session ID through one of the parameters which adds the room to the hub [Groups](https://docs.microsoft.com/en-us/aspnet/signalr/overview/guide-to-the-api/working-with-groups) if it doesn't already exist. this is to make sure each user is connected to their respective session IDs so not to send all game updates to all session IDs (that would be pretty chaotic).
* User will be connected as the name they put in the prompt, which should be one of the names assigned to the game session.
* When a user presses the "Roll Dice" button it gets a random number from 1-6 from the API and also invokes the SendDiceRoll method on the server which then invokes the function "RecieveDiceRoll" on all clients in that group (session ID). 

Server-side Method for Dice Roll syncing
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
Client-side Function for Dice Roll syncing
```js
connection.on("RecieveDiceRoll", function (num, player) {
    //update the diceRoll id to display "[player] rolled [num]!"
    document.getElementById("diceValue").innerText = player + " rolled " + num + "!";
    //update the image value to match the dice roll (so the user can see a picture of a dice with x value)
    document.getElementById("diceVisual").src = "/img/dice-" + num + ".png";
});
```


* After the dice has been rolled we should update who's turn is next, we do this by invoking the "UpdatePlayerTurn" function on all clients

Server-side method for player turn update
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
Client-side function for player turn update
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

* As for updating the game state visually, we clear the pawns and recreate them at their new positions. All the updating of pawn positions etc happens behind the curtains (on the server) but we still need to update them to their new positions client-side.

Server send UpdateGameState function invocation to all clients on this sessionID, which force updates the game board for everyone in the room.
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
        
