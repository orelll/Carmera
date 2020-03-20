const constraints = {
    video: true,
    audio: false
};



async function makeCall() {

    const signalingChannel = new SignalingChannel();
    const configuration = {
        'iceServers': [{
            'urls': 'stun:stun.l.google.com:19302'
        }]
    }

    const peerConnection = new RTCPeerConnection(configuration);

    signalingChannel.addEventListener('message', async message => {
        if (message.answer) {
            const remoteDesc = new RTCSessionDescription(message.answer);
            await peerConnection.setRemoteDescription(remoteDesc);
        }
    });

    const offer = await peerConnection.createOffer();
    await peerConnection.setLocalDescription(offer);
    signalingChannel.send({
        'offer': offer
    });
}

function test() {
    //This will open the connection*
    ws = new WebSocket("ws://127.0.0.1:8080", 'echo-protocol');
    ws.onmessage = function(e){ console.log(e.data); };
    ws.onopen = () => ws.send('hello');
    
    // ws.close();
}