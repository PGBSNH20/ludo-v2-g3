let ColorMapper = ["Red", "Green", "Blue", "Yellow"]

function PositionPawn(position, isInNest, isAtFinishLine, isFinished, color, pawnId, sessionId) {

    /*
     * these arguments are passed as primitives from the client and passed as strings from server
     * for whatever reason
     * to amend this stringify them and compare them to "true" string to prevent any incorrect positioning
     * can definitely be improved
     */
    isInNest = ((isInNest + "").toLowerCase() == "true");
    isAtFinishLine = ((isAtFinishLine + "").toLowerCase() == "true");
    isFinished = ((isFinished + "").toLowerCase() == "true");
    pawnId = Number(pawnId);
    sessionId = sessionId.toUpperCase();

    if (typeof (color) == "number") {
        color = ColorMapper[color - 1];
    }

    console.log(position, isInNest, isAtFinishLine, isFinished, color, pawnId, sessionId);

    var pawnColor;
    switch (color) {
    case 'Green':
        pawnColor = 'green-pawn';
        break;
    case 'Red':
        pawnColor = 'red-pawn';
        break;
    case 'Blue':
        pawnColor = 'blue-pawn';
        break;
    default:
        pawnColor = 'yellow-pawn';
        break;
    }

    let pawn = document.createElement('div');
    pawn.classList.add('pawn');
    pawn.classList.add(pawnColor);
    pawn.setAttribute("pawn-id", pawnId);
    pawn.addEventListener("click", function () {
        PostChanges(this, sessionId);
    });
    if (isInNest) {
        var nest;
        switch (color) {
        case 'Green':
            nest = 'greenNest';
            break;
        case 'Red':
            nest = 'redNest';
            break;
        case 'Blue':
            nest = 'blueNest';
            break;
        case 'Yellow':
            nest = 'yellowNest';
            break;
        }

        var foundNest = document.
            querySelector("." + nest);

        foundNest.appendChild(pawn);
        return;
    }

    if (isFinished) {
        var finishSquare = document.querySelector('.goal');
        finishSquare.appendChild(pawn);
    }

    if (!isInNest && !isAtFinishLine && !isFinished) {
        var foundSquare = document.
            querySelector(".square-" + position);
        foundSquare.appendChild(pawn);
    }

    if (!isInNest && isAtFinishLine && !isFinished) {
        var finishLineSquare = document
            .querySelector(".square_" + color.toLowerCase() + position);
        finishLineSquare.appendChild(pawn);
    }
    console.log(".square-" + position);
    console.log(".square_" + color.toLowerCase() + position);
}

