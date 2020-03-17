const constraints = {
    'video': true,
    'audio': false
}

function start() {

    MakeCall();
}

function stop() {

}
const signalingChannel;

function getSelectedSdpSemantics() {
    const sdpSemanticsSelect = document.querySelector('#sdpSemantics');
    const option = sdpSemanticsSelect.options[sdpSemanticsSelect.selectedIndex];
    return option.value === '' ? {} : {sdpSemantics: option.value};
  }

async function MakeCall() {
    const config = {
        'iceServers': [{
            'urls': 'stun:stun.l.google.com:19302'
        }]
    };

    console.log(getSelectedSdpSemantics);

    signalingChannel = new SignalingChannel('remoteClientId');

    const peerConnection = new RTCPeerConnection(config);

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