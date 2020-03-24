const constraints = {
  video: true,
  audio: false
};

async function makeCall() {
  const signalingChannel = new SignalingChannel();
  const configuration = {
    iceServers: [
      {
        urls: "stun:stun.l.google.com:19302"
      }
    ]
  };

  const peerConnection = new RTCPeerConnection(configuration);

  signalingChannel.addEventListener("message", async message => {
    if (message.answer) {
      const remoteDesc = new RTCSessionDescription(message.answer);
      await peerConnection.setRemoteDescription(remoteDesc);
    }
  });

  const offer = await peerConnection.createOffer();
  await peerConnection.setLocalDescription(offer);
  signalingChannel.send({
    offer: offer
  });
}

function sayHello() {
  socket = new WebSocket("ws://localhost:5000/ws");
  socket.onmessage = function(event) {
    alert(`[message] Data received from server: ${event.data}`);
  };

  socket.onopen = function(e) {
    var dto = new CheckoutDTO("carmera");
    var stringed = JSON.stringify(dto);
    socket.send(stringed);
  };

  socket.onerror = function(error) {
    alert(`[error] ${error.message}`);
  };

  socket.onclose = function(event) {
    if (event.wasClean) {
      alert(
        `[close] Connection closed cleanly, code=${event.code} reason=${event.reason}`
      );
    } else {
      // e.g. server process killed or network down
      // event.code is usually 1006 in this case
      alert("[close] Connection died");
    }
  };
}

class CheckoutDTO {
  constructor(name) {
    this.name = name;
    this.kind = "checkout";
  }
}
