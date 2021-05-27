import { RequestTypes } from 'src/app/common/requestTypesEnum';
import { ElementRef, Injectable } from '@angular/core';
import { webSocket, WebSocketSubject } from 'rxjs/webSocket';
import { Observable, Subject } from 'rxjs';

// check https://rxjs-dev.firebaseapp.com/api/webSocket/webSocket
@Injectable({
  providedIn: 'root',
})
export class SocketMessagingService {
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
  private localOffer: RTCSessionDescriptionInit;
  private peerOffer: RTCSessionDescriptionInit;

  private remoteStream = new MediaStream();
  private remoteVideo: any;

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

  setVideoElement(video: ElementRef): void {
    this.remoteVideo = video.nativeElement;
    this.remoteVideo.srcObject = this.remoteStream;
  }

  async createOffer(): Promise<RTCSessionDescriptionInit> {
    this.peerConnection.addEventListener('track', async (event) => {
      console.log(`new stream available`);
      this.remoteStream.addTrack(event.track);
      this.remoteVideo.srcObject = this.remoteStream;
    });

    return await this.peerConnection.createOffer().then((offer) => {
      this.setLocalOffer(offer);
      return offer;
    });
  }

  async setPeerOffer(
    peerOffer: RTCSessionDescriptionInit
  ): Promise<RTCSessionDescriptionInit> {
    this.peerOffer = peerOffer;
    console.log(`setting remote description`);
    this.peerConnection.setRemoteDescription(peerOffer);

    var answer = await this.peerConnection.createAnswer();
    this.peerConnection.setLocalDescription(answer);
    return answer;
  }

  private setLocalOffer(offer: RTCSessionDescriptionInit): void {
    this.peerConnection.setLocalDescription(offer);
    this.localOffer = offer;
  }

  public sendHello(): void {
    this.sendText('hello from the other side', RequestTypes.txt);
  }

  public send(
    offer: RTCSessionDescriptionInit,
    requestType: RequestTypes
  ): void {
    const stringedOffer = JSON.stringify(offer);
    this.sendText(stringedOffer, requestType);
  }

  public onOffer(): Observable<RTCSessionDescriptionInit> {
    return this.offerResponseObservable;
  }

  private sendText(text: string, type: RequestTypes): void {
    this.initSocket();

    const message = {
      type: type,
      payload: text,
    };

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
      case 'ClientOfferResponse':
        var serverExists = JO['ServerAvailable'];
        if (serverExists) {
          var jsonedOffer = serverExists == true ? JO['ServerOffer'] : '';
          const offer: RTCSessionDescription = new RTCSessionDescription(
            JSON.parse(jsonedOffer)
          );
          this.offerResponseSubject.next(offer);
        }
        break;
      case 'ServerAvailable':
        var serverExists = JO['ServerAvailable'];
        if (serverExists) {
          var jsonedOffer = serverExists == true ? JO['ServerOffer'] : '';
          const offer: RTCSessionDescription = new RTCSessionDescription(
            JSON.parse(jsonedOffer)
          );
          this.offerResponseSubject.next(offer);
        }
        break;

      case 'NewICEAvailable':
        var iceDataJO = JO['ICEData'];
        var parsed = JSON.parse(iceDataJO);
        var iceData = new RTCIceCandidate(parsed);
        console.log(`adding ICE Candidate: ${parsed}`);
        this.peerConnection.addIceCandidate(iceData);
        break;
    }
  }
}
