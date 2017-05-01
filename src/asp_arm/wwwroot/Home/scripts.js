﻿var image = document.images[0];
var timer = window.setInterval(refresh, 1000);

function startTimer() {
    clearInterval(timer);
    timer = window.setInterval(refresh, 1000);
    document.getElementById("startRefreshBtn").disabled = true;
    document.getElementById("stopRefreshBtn").disabled = false;
    document.getElementById("refreshBtn").disabled = true;
}
function stopTimer() {
    clearInterval(timer);
    document.getElementById("startRefreshBtn").disabled = false;
    document.getElementById("stopRefreshBtn").disabled = true;
    document.getElementById("refreshBtn").disabled = false;
}
function refresh() {
    refreshStates();
    refreshImage();
}
function refreshImage() {
    var downloadingImage = new Image();
    downloadingImage.onload = function () {
        image.src = this.src;
    };
    downloadingImage.src = "../image?" + new Date().getTime();
}
function refreshStates() {
    readStatus(document.getElementById("ledstate"), "led");
    readStatus(document.getElementById("inputstate"), "input");
    console.log(document.getElementById("ledstate").innerHTML);
    if (document.getElementById("ledstate").innerHTML == "on") {
        document.getElementById("ledOnBtn").disabled = true;
        document.getElementById("ledOffBtn").disabled = false;
    } else {
        document.getElementById("ledOnBtn").disabled = false;
        document.getElementById("ledOffBtn").disabled = true;
    }
}
function readStatus(element, address) {
    var rawFile = new XMLHttpRequest();
    rawFile.open("GET", address, true);
    rawFile.onreadystatechange = function () {
        if (rawFile.readyState === 4) {
            if (rawFile.status === 200 || rawFile.status == 0) {
                var allText = rawFile.responseText;
                element.innerHTML = allText;
            }
        }
    }
    rawFile.send(null);
}
function setLed(state) {
    var request = new XMLHttpRequest();
    request.open("GET", "led/" + state, true);
    request.onreadystatechange = function () {
        if (request.readyState === 4) {
            if (request.status === 200 || request.status == 0) {
                if (request.response == "on") {
                    document.getElementById("ledOnBtn").disabled = true;
                    document.getElementById("ledOffBtn").disabled = false;
                } else {
                    document.getElementById("ledOnBtn").disabled = false;
                    document.getElementById("ledOffBtn").disabled = true;
                }
            }
        }
    }
    request.send(null);
}