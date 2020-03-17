const constraints = {
    'video': true,
    'audio': false
}

function start() {

    navigator.mediaDevices.getUserMedia(constraints)
        .then(stream => {
            console.log('Got MediaStream:', stream);
            var videoElement = document.querySelector('video#localVideo');
            videoElement.srcObject = stream;
        })
        .catch(error => {
            console.error('Error accessing media devices.', error);
        });
}

function stop() {
    var videoElement = document.querySelector('video#localVideo');
    var stream = videoElement.srcObject;
    stream.getTracks().forEach(track => track.stop());
    videoElement.srcObject = null;
}