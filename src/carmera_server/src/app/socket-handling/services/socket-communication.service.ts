import { Injectable } from '@angular/core';
import { Observable, Subject } from 'rxjs';
import { webSocket, WebSocketSubject } from 'rxjs/webSocket';
import { RequestTypes } from '../../common/requestTypesEnum';

// check https://rxjs-dev.firebaseapp.com/api/webSocket/webSocket
@Injectable({
  providedIn: 'root',
})
export class SocketCommunicationService {
  private subject: WebSocketSubject<unknown>;
  private ws_port = 44305;
  private ws_is_secured = true;
  private ws_host = 'localhost';
  private ws_address = this.ws_is_secured
    ? `wss://${this.ws_host}:${this.ws_port}`
    : `ws://${this.ws_host}:${this.ws_port}`;

  private offerResponseSubject: Subject<RTCSessionDescription>;
  offerResponseObservable: Observable<RTCSessionDescription>;

  private configuration = {
    iceServers: [{ urls: 'stun:stun.l.google.com:19302' }],
  };
  private peerConnection: RTCPeerConnection;

  private mediaStream: MediaStream;

  constructor() {
    this.offerResponseSubject = new Subject<RTCSessionDescription>();
    this.offerResponseObservable = this.offerResponseSubject.asObservable();
    this.peerConnection = new RTCPeerConnection(this.configuration);
    this.peerConnection.addEventListener('connectionstatechange', (event) => {
      if (this.peerConnection.connectionState === 'connected') {
        // Peers connected!
        console.log(`connected!!`);
      }
    });
  }

  async createOffer(): Promise<RTCSessionDescriptionInit> {
    this.mediaStream = await navigator.mediaDevices.getUserMedia({
      video: true,
      audio: true,
    });

    this.mediaStream.getTracks().forEach((track) => {
      this.peerConnection.addTrack(track, this.mediaStream);
    });

    this.peerConnection.addEventListener('icecandidate', (event) => {
      if (event.candidate) {
        console.log(`new ice candidate! ${JSON.stringify(event.candidate)}`);
        this.sendICEOffer(event.candidate);
      }
    });

    return await this.peerConnection.createOffer().then((offer) => {
      this.setLocalOffer(offer);
      return offer;
    });
  }

  async setPeerOffer(
    peerOffer: RTCSessionDescriptionInit
  ): Promise<RTCSessionDescriptionInit> {
    console.log(`setting remote description`);
    if (this.peerConnection.signalingState != 'stable')
      await this.peerConnection.setRemoteDescription(peerOffer);
    return null;
  }

  private setLocalOffer(offer: RTCSessionDescriptionInit): void {
    this.peerConnection.setLocalDescription(offer);
  }

  public send(
    offer: RTCSessionDescriptionInit,
    requestType: RequestTypes
  ): void {
    // const stringedOffer = JSON.stringify(offer);
    this.sendText(offer, requestType);
  }

  public sendICEOffer(iceData: RTCIceCandidate): void {
    this.sendText(iceData, RequestTypes.newICE);
  }

  public onOffer(): Observable<RTCSessionDescriptionInit> {
    return this.offerResponseObservable;
  }

  private sendText(data: any, type: RequestTypes): void {
    this.initSocket();

    const message = {
      type: type,
      payload: data,
    };

    var stringedMessage = JSON.stringify(message);

    this.subject.subscribe(
      (msg) => this.handleOfferSent(msg),
      (err) => console.log(err),
      () => console.log('complete')
    );

    this.subject.next(message);
  }

  private initSocket(): void {
    console.log(`Opening socket for address ${this.ws_address}`);
    this.subject = webSocket(this.ws_address);
    this.subject.subscribe();
  }

  private handleOfferSent(msg: any): void {
    if (!(msg instanceof String)) msg = JSON.stringify(msg);
    var JO = JSON.parse(msg);
    var responseType = JO['ResponseType'];

    switch (responseType) {
      case 'ServerOfferResponse':
        console.log(`response type: ServerOfferResponse`);
        break;
      case 'NewICEAvailableResponse':
        console.log(`response type: NewICEAvailableResponse`);
        break;
      case 'Answer':
        console.log(`response type: answer`);
        var jsonedOffer = JO['AnswerOffer'];
        const offer: RTCSessionDescription = new RTCSessionDescription(
          JSON.parse(jsonedOffer)
        );
        this.offerResponseSubject.next(offer);
        break;
    }
  }
}
