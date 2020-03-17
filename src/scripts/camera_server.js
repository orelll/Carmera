async function Prepare() {
    const config = {
        'iceServers': [{
            'urls': 'stun:stun.l.google.com:19302'
        }]
    };
    const peerConnection = new RTCPeerConnection(config);
    signalingChannel.addEventListener('message', async message => {
        if (message.offer) {
            peerConnection.setRemoteDescription(new RTCSessionDescription(message.offer));
            const answer = await peerConnection.createAnswer();
            await peerConnection.setLocalDescription(answer);
            signalingChannel.send({'answer' : answer})
        }
    });
}