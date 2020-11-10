import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})


export class SocketingService {

  private socket: WebSocket;
  private SERVER_URL = '127.0.0.1:5000';

  constructor() {

  }


  public send(offer: RTCSessionDescriptionInit): void {
    this.socket = new WebSocket(`ws://${this.SERVER_URL}/ws`);
    while (this.socket.readyState === 0) {
      console.log('waiting for connection')
    }
    // this.onMessage();

    var msg = {
      kind: 'checkin',
      peerName: 'carmera',
      offer: offer
    };

    var msgAsText = JSON.stringify(msg);

    console.log(`Sending ${msgAsText}`);
    this.socket.send(msgAsText);
  }

  public onMessage(): Observable<any> {
    return new Observable<any>((observer) => {
      this.socket.onmessage = (data: MessageEvent) => {
        console.log(`Received data: ${data.data}`);
        observer.next(data);
      }
    });
  }

  public closeSocket() {
    console.log(`Closing connection :(`);
    this.socket.close(1000, `Standard closure`);
  }
}
