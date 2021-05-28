import { Injectable } from '@angular/core';
import { Observable, Subject } from 'rxjs';
import { RequestTypes } from 'src/app/common/requestTypesEnum';

@Injectable({
  providedIn: 'root',
})
export class WebRtcService {
  private peerConnection: RTCPeerConnection = new RTCPeerConnection();
  
  public streamsReady = false;
  public streamsCount = 0;
  public offerGenerated = false;
  public remoteDesciptionSet = false;
  public peerSignalingState = this.peerConnection ? this.peerConnection?.signalingState : '';
  public peerConnectionState = this.peerConnection ? this.peerConnection.connectionState : '';
  

  private configuration = {
    iceServers: [{ urls: 'stun:stun.l.google.com:19302' }],
  };
  private mediaStream: MediaStream;

  private sendDataSubject: Subject<[any, RequestTypes]>;
  private sendDataObservable: Observable<[any, RequestTypes]>;

  constructor() {
    this.sendDataSubject = new Subject<[any, RequestTypes]>();
    this.sendDataObservable = this.sendDataSubject.asObservable();
  }

  //WEBRTC
  public odDataToSend(): Observable<[any, RequestTypes]> {
    return this.sendDataObservable;
  }

  public async setRemoteDescription(
    peerOffer: RTCSessionDescriptionInit
  ): Promise<RTCSessionDescriptionInit> {
    console.log(`setting remote description`);
    if (this.peerConnection.signalingState != 'stable')
      await this.peerConnection.setRemoteDescription(peerOffer);
    return null;
  }

  public async createOffer(): Promise<RTCSessionDescriptionInit> {
    await this.preparePeerConnectionAndStreams();

    return await this.peerConnection.createOffer().then((offer) => {
      this.setLocalDescription(offer);
      return offer;
    });
  }

  private async preparePeerConnectionAndStreams(): Promise<void> {
    this.mediaStream = await navigator.mediaDevices.getUserMedia({
      video: true,
      audio: true,
    });

    this.peerConnection = new RTCPeerConnection(this.configuration);

    this.peerConnection.addEventListener('connectionstatechange', (event) => {
      if (this.peerConnection.connectionState === 'connected') {
        console.log(`connected!!`);
      }
    });

    this.peerConnection.addEventListener('icecandidate', (event) => {
      if (event.candidate) {
        console.log(`new ice candidate! ${JSON.stringify(event.candidate)}`);
        this.sendDataSubject.next([event.candidate, RequestTypes.newICE]);
      }
    });

    this.mediaStream.getTracks().forEach((track) => {
      this.peerConnection.addTrack(track, this.mediaStream);
    });
  }

  private setLocalDescription(offer: RTCSessionDescriptionInit): void {
    this.peerConnection.setLocalDescription(offer);
  }
}
