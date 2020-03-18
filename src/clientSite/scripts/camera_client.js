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
    signalingChannel.send({'offer' : offer});
}