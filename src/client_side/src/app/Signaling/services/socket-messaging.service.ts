import { RequestTypes } from 'src/app/common/requestTypesEnum';
import { Injectable } from '@angular/core';
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

  private offerResponseSubject: Subject<RTCSessionDescriptionInit>;
  offerResponseObservable: Observable<RTCSessionDescriptionInit>;

  constructor() {
    this.offerResponseSubject = new Subject<RTCSessionDescriptionInit>();
    this.offerResponseObservable = this.offerResponseSubject.asObservable();
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
    console.log('message received: ' + JSON.stringify(msg));
    var JO = JSON.parse(msg);
    var responseType = JO['ResponseType'];

    switch (responseType) {
      case 'ClientOfferResponse':
        var serverExists = JO['ServerAvailable'];
        if (serverExists) {
          var jsonedOffer = serverExists == true ? JO['ServerOffer'] : '';
          const offer: RTCSessionDescriptionInit = JSON.parse(jsonedOffer);
          this.offerResponseSubject.next(offer);
        }
        break;
      case 'ServerAvailable':
        var serverExists = JO['ServerAvailable'];
        if (serverExists) {
          var jsonedOffer = serverExists == true ? JO['ServerOffer'] : '';
          const offer: RTCSessionDescriptionInit = JSON.parse(jsonedOffer);
          this.offerResponseSubject.next(offer);
        }
        break;
    }
  }
}
