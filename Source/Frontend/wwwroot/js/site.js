"use strict";

const diceElement = document.querySelector('.dice');

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


// Test for modal

var modal = document.getElementById("myModal");

// Get the button that opens the modal
var btn = document.getElementById("get-session-id");

// Get the <span> element that closes the modal
var span = document.getElementsByClassName("close")[0];

// When the user clicks the button, open the modal 
btn.onclick = function () {
    modal.style.display = "block";
}

// When the user clicks on <span> (x), close the modal
span.onclick = function () {
    modal.style.display = "none";
}

// When the user clicks anywhere outside of the modal, close it
window.onclick = function (event) {
    if (event.target == modal) {
        modal.style.display = "none";
    }
}