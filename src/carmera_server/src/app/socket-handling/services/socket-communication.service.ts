import { Injectable } from '@angular/core';
import { Action } from 'rxjs/internal/scheduler/Action';
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

  constructor() {}

  public send(offer: RTCSessionDescriptionInit): void {
    const stringedOffer = JSON.stringify(offer);
    this.sendText(stringedOffer, RequestTypes.serverOffer, this.handleReceivedToConsole);
  }

  private sendText(
    text: string,
    type: RequestTypes,
    handleAction: (response: any) => void
  ): void {
    this.initSocket();

    const message = {
      type: type,
      payload: text,
    };

    this.subject.subscribe(
      (msg) => handleAction(msg),
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

  private handleReceivedToConsole(data: any): void {
    console.log(JSON.stringify(data));
    var message = data['Message'];
  }
}
