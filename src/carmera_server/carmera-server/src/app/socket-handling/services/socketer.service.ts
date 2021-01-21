import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class SocketerService {
  private configuration = { iceServers: [{ urls: 'stun:stun.l.google.com:19302' }] };

  constructor() {}

  async createOffer(): Promise<RTCSessionDescriptionInit> {
    const peerConnection = new RTCPeerConnection(this.configuration);

    return await peerConnection.createOffer();
  }
}
