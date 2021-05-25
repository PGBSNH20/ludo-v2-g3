function PositionPawn(position, isInNest, isAtFinishLine, isFinished, color, pawnId, sessionId) {

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
    if (isInNest === 'True') {
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

    if (isFinished === 'True') {
        var finishSquare = document.querySelector('.goal');
        finishSquare.appendChild(pawn);
    }

    if (isInNest === 'False' && isAtFinishLine === 'False' && isFinished === 'False') {
        console.log(position);
        var foundSquare = document.
            querySelector(".square-" + position);
        foundSquare.appendChild(pawn);
    }

    if (isInNest === 'False' && isAtFinishLine === 'True' && isFinished === 'False') {
        var finishLineSquare = document
            .querySelector(".square_" + color.toLowerCase() + position);
        finishLineSquare.appendChild(pawn);
    }
}