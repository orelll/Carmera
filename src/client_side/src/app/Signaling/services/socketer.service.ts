import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class SocketerService {
  private configuration = {
    iceServers: [{ urls: 'stun:stun.l.google.com:19302' }],
  };
  private peerConnection: RTCPeerConnection;
  private localOffer: RTCSessionDescriptionInit;
  private peerOffer: RTCSessionDescriptionInit;

  constructor() {
    this.peerConnection = new RTCPeerConnection(this.configuration);
  }

  async createOffer(): Promise<RTCSessionDescriptionInit> {
    return await this.peerConnection.createOffer().then((offer) => {
      this.setLocalOffer(offer);
      return offer;
    });
  }

  async setPeerOffer(peerOffer: RTCSessionDescriptionInit): Promise<RTCSessionDescriptionInit> {
    this.peerOffer = peerOffer;

    this.peerConnection.setRemoteDescription(new RTCSessionDescription(peerOffer));
    const answer = await this.peerConnection.createAnswer();
    await this.peerConnection.setLocalDescription(answer);
    return answer;
  }

  private setLocalOffer(offer: RTCSessionDescriptionInit): void {
    this.localOffer = offer;
  }
}
