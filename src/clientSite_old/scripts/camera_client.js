const constraints = {
  video: true,
  audio: false,
};

async function makeCall() {
  const signalingChannel = new SignalingChannel();
  const configuration = {
    iceServers: [{
      urls: "stun:stun.l.google.com:19302",
    }, ],
  };

  const peerConnection = new RTCPeerConnection(configuration);

  signalingChannel.addEventListener("message", async (message) => {
    if (message.answer) {
      const remoteDesc = new RTCSessionDescription(message.answer);
      await peerConnection.setRemoteDescription(remoteDesc);
    }
  });

  const offer = await peerConnection.createOffer();
  await peerConnection.setLocalDescription(offer);
  signalingChannel.send({
    offer: offer,
  });
}

function checkIn() {
  socket = new WebSocket("ws://localhost:5000/ws");
  socket.onmessage = function (event) {
    var deserializedMessage = JSON.parse(event.data);
    console.log(`checkout succeded: ${deserializedMessage.Success}`);
  };

  socket.onopen = function (e) {
    var dto = new CheckoutDTO("carmera");
    var stringed = JSON.stringify(dto);
    socket.send(stringed);
  };

  socket.onerror = function (error) {
    console.log(`[error] ${error.message}`);
  };

  socket.onclose = function (event) {
    if (event.wasClean) {
      console.log(
        `[close] Connection closed cleanly, code=${event.code} reason=${event.reason}`
      );
    } else {
      // e.g. server process killed or network down
      // event.code is usually 1006 in this case
      console.log("[close] Connection died");
    }
  };
}
async function startServer() {
  var WebSocketServer = require('websocket').server;
  var http = require('http');

  var server = http.createServer(function (request, response) {
    // process HTTP request. 
  });
  server.listen(8080, function () {});

  // create the server
  wsServer = new WebSocketServer({
    httpServer: server
  });

  // WebSocket server
  wsServer.on('request', function (request) {
    var connection = request.accept(null, request.origin);

    // This is the most important callback for us, we'll handle
    // all messages from users here.
    connection.on('message', function (message) {
      // Process WebSocket message
    });

    connection.on('close', function (connection) {
      // Connection closes
    });
  });

  checkIn();
}
// Make the function wait until the connection is made...
function waitForSocketConnection(socket, callback) {
  setTimeout(
    function () {
      if (socket.readyState === 1) {
        console.log("Connection is made")
        if (callback != null) {
          callback();
        }
      } else {
        console.log("wait for connection...")
        waitForSocketConnection(socket, callback);
      }

    }, 5); // wait 5 milisecond for the connection...
}

function lookForPeer(peerName) {
  socket = new WebSocket("ws://localhost:5000/ws");
  socket.onmessage = function (event) {
    var deserializedMessage = JSON.parse(event.data);
    console.log(`Peer search succeeded: ${deserializedMessage.Success}`);
    deserializedMessage.PeersFound.forEach(printFoundPeer);
  };

  socket.onopen = function (e) {
    var dto = new GetPeerRequestDTO("carmera");
    var stringed = JSON.stringify(dto);
    socket.send(stringed);
  };

  socket.onerror = function (error) {
    console.log(`[error] ${error.message}`);
  };

  socket.onclose = function (event) {
    if (event.wasClean) {
      console.log(
        `[close] Connection closed cleanly, code=${event.code} reason=${event.reason}`
      );
    } else {
      // e.g. server process killed or network down
      // event.code is usually 1006 in this case
      console.log("[close] Connection died");
    }
  };
}

async function makeCall() {
  const configuration = {
    "iceServers": [{
      "urls": "stun:stun.l.google.com:19302"
    }]
  };
  const peerConnection = new RTCPeerConnection(configuration);
  // signalingChannel.addEventListener('message', async message => {
  //     if (message.answer) {
  //         const remoteDesc = new RTCSessionDescription(message.answer);
  //         await peerConnection.setRemoteDescription(remoteDesc);
  //     }
  // });
  const offer = await peerConnection.createOffer();
  await peerConnection.setLocalDescription(offer);
  // signalingChannel.send({'offer': offer});
}

async function sendOffer() {
  const configuration = {
    'iceServers': [{
      'urls': 'stun:stun.l.google.com:19302'
    }]
  }
  const peerConnection = new RTCPeerConnection(configuration);

  socket = new WebSocket("ws://localhost:5000/ws");
  socket.onmessage = function (event) {
    var deserializedMessage = JSON.parse(event.data);
    console.log(`Peer search succeeded: ${deserializedMessage.Success}`);
    deserializedMessage.PeersFound.forEach(printFoundPeer);
  };

  socket.onopen = async function (e) {
    var offer = await peerConnection.createOffer();
    var dto = new OfferRequestDTO(JSON.stringify(offer));
    var stringed = JSON.stringify(dto);
    socket.send(stringed);
  };

  socket.onerror = function (error) {
    console.log(`[error] ${error.message}`);
  };

  socket.onclose = function (event) {
    if (event.wasClean) {
      console.log(
        `[close] Connection closed cleanly, code=${event.code} reason=${event.reason}`
      );
    } else {
      console.log("[close] Connection died");
    }
  };
}

function printFoundPeer(item, index) {
  console.log(`Peer found: ${item.Address}:${item.Port}`);
}

class CheckoutDTO {
  constructor(name) {
    this.peerName = name;
    this.kind = "checkin";
  }
}

class GetPeerRequestDTO {
  constructor(secondSideName) {
    this.secondSideName = secondSideName;
    this.kind = "GetPeer";
  }
}

class OfferRequestDTO {
  constructor(offerData) {
    this.offerData = offerData;
    this.kind = "Offer";
  }
}