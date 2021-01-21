import { Injectable } from '@angular/core';
import { webSocket, WebSocketSubject } from 'rxjs/webSocket';
import { RequestTypes } from '../requestTypesEnum';

// check https://rxjs-dev.firebaseapp.com/api/webSocket/webSocket
@Injectable({
  providedIn: 'root',
})
export class SocketingService {
  private subject: WebSocketSubject<unknown>;
  private ws_port = 44305;
  private ws_is_secured = true;
  private ws_host = 'localhost';
  private ws_address = this.ws_is_secured
    ? `wss://${this.ws_host}:${this.ws_port}`
    : `ws://${this.ws_host}:${this.ws_port}`;

  constructor() {}

  public sendHello(): void {
    this.sendText('hello from the other side', RequestTypes.txt, this.handleToConsole);
  }

  public send(offer: RTCSessionDescriptionInit): void {
    const stringedOffer = JSON.stringify(offer);
    this.sendText(stringedOffer, RequestTypes.offer, this.handleOfferSent);
  }

  private sendText(
    text: string,
    type: RequestTypes,
    responseCallback: (data: any) => void
  ): void {
    this.initSocket();

    const message = {
      type: type,
      payload: text,
    };

    this.subject.subscribe(
      (msg) => responseCallback(msg), 
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

  private handleToConsole(msg: any): void {
    console.log('message received: ' + JSON.stringify(msg));
  }

  private handleOfferSent(msg: any): void {
    console.log('message received: ' + JSON.stringify(msg));
    var serverExists = msg['ServerAvailable'];
    var serverOffer = serverExists == true ? msg['ServerOffer'] : '';
  }
}
