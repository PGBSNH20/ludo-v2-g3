"use strict";

const diceElement = document.querySelector('.dice');

const getCurrentGuid = window.location.href.substring(window.location.href.lastIndexOf('/') + 1);

async function rollDice(id) {
    var requestOptions = {
        method: "PUT",
        headers: {
            'Content-Type': 'application/json',
            'Accept': 'application/json'
        },
        body: JSON.stringify(id)
    };

    var response1 = await fetch("https://localhost:44303/api/Ludo/RollDice", requestOptions);
    var data = await response1.json();
    document.querySelector(".roll").innerText = data;
    connection.invoke("SendDiceRoll", data, getCurrentGuid);
    connection.invoke("UpdateNextPlayerTurn", getCurrentGuid);

    if (!isNaN(data)) {
        var newNumber = parseInt(data);
        diceElement.src = `/img/dice-${newNumber}.png`;
    } else {
        alert(data);
    }
}

// JQuery for the expandable player radiobuttons

$(document).ready(function () {
    $(".pl3").hide();
    $(".pl4").hide();

    $("form#group").click(function () {
        if ($("#player3").is(":checked")) {
            $(".pl4").hide();
            $(".pl3").show();
        } else if ($("#player4").is(":checked")) {
            $(".pl3").show();
            $(".pl4").show();
        } else if ($("#player2").is(":checked")) {
            $(".pl3").hide();
            $(".pl4").hide();
        }
    });
});