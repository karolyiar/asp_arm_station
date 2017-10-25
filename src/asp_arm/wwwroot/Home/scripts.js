var image = document.getElementById("webcamImg");
var refreshBtn = document.getElementById("refreshBtn");
var refreshNowBtn = document.getElementById("refreshNowBtn");
var waterBtn = document.getElementById("waterBtn");
var timer = window.setInterval(refresh, 1000);
var ledChanging = 0;

var autoRefresh = true;
var waterFlow = true;

function toggleRefresh() {
    if (autoRefresh) {
        stopTimer();
    } else {
        startTimer();
    }
    autoRefresh = !autoRefresh;
}

function startTimer() {
    clearInterval(timer);
    timer = window.setInterval(refresh, 1000);

    refreshBtn.innerHTML = "Bekapcsolva";
    refreshBtn.classList.remove("btn-default");
    refreshBtn.classList.add("btn-primary");
}
function stopTimer() {
    clearInterval(timer);

    refreshBtn.innerHTML = "Kikapcsolva"
    refreshBtn.classList.remove("btn-primary");
    refreshBtn.classList.add("btn-default");
    
}
function refresh() {
    if (!ledChanging)
        refreshStates();
    else
        ledChanging -= 1;
    refreshImage();
}
function refreshImage() {
    var downloadingImage = new Image();
    downloadingImage.onload = function () {
        image.src = this.src;
    };
    downloadingImage.src = "../image/saved?" + new Date().getTime();

}
function refreshStates() {
    readStatus(document.getElementById("ledstate"), "led");
    readStatus(document.getElementById("inputstate"), "input");
    if (document.getElementById("ledstate").innerHTML == "on") {
        waterBtn.innerHTML = "Bekapcsolva";
        waterBtn.classList.remove("btn-default");
        waterBtn.classList.add("btn-primary");
        waterFlow = true;
    } else {
        waterBtn.innerHTML = "Kikapcsolva";
        waterBtn.classList.remove("btn-primary");
        waterBtn.classList.add("btn-default");
        waterFlow = false;
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
function toggleWater() {
    waterFlow = !waterFlow;
    var request = new XMLHttpRequest();
    request.open("GET", "led/" + (waterFlow?1:0), true);
    request.onreadystatechange = function () {
        if (request.readyState === 4) {
            if (request.status === 200 || request.status == 0) {
                ledChanging = 10;
                if (request.response == "on") {
                    waterBtn.innerHTML = "Bekapcsolva";
                    waterBtn.classList.remove("btn-default");
                    waterBtn.classList.add("btn-primary");
                    waterFlow = true;
                } else {
                    waterBtn.innerHTML = "Kikapcsolva";
                    waterBtn.classList.remove("btn-primary");
                    waterBtn.classList.add("btn-default");
                    waterFlow = false;
                }
            }
        }
    }
    request.send(null);
}