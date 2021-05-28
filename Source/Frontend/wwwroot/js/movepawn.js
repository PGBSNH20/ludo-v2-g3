"use strict";

async function PostChanges(pawn, sessionId) {
    var requestModel = { PawnId: pawn.getAttribute("pawn-id"), SessionId: sessionId };

    var response = await fetch('https://localhost:44303/api/Ludo/MovePawn',
        {
            method: 'PUT',
            headers: {
                'Content-Type': 'application/json',
                'Accept': 'application/json'
            },
            body: JSON.stringify(requestModel)
        });

    var data = await response.json();
    document.querySelector(".roll").innerText = data;
}