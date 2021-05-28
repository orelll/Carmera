import { Injectable } from '@angular/core';
import { Observable, Subject } from 'rxjs';
import { webSocket, WebSocketSubject } from 'rxjs/webSocket';
import { RequestTypes } from '../../common/requestTypesEnum';

@Injectable({
  providedIn: 'root',
})
export class SocketCommunicationService {
  public socketConnected = false;
  public offerSent = false;
  public offerAccepted = false;

  //websockets
  private wsSubject: WebSocketSubject<unknown>;
  private ws_port = 44305;
  private ws_is_secured = true;
  private ws_host = 'localhost';
  private ws_address = this.ws_is_secured
    ? `wss://${this.ws_host}:${this.ws_port}`
    : `ws://${this.ws_host}:${this.ws_port}`;

  private offerResponseSubject: Subject<RTCSessionDescription>;
  offerResponseObservable: Observable<RTCSessionDescription>;
  private newICESubject: Subject<RTCIceCandidate>;
  newICEObservable: Observable<RTCIceCandidate>;

  //web rtc

  constructor() {
    //websockets
    this.offerResponseSubject = new Subject<RTCSessionDescription>();
    this.offerResponseObservable = this.offerResponseSubject.asObservable();

    this.newICESubject = new Subject<RTCIceCandidate>();
    this.newICEObservable = this.newICESubject.asObservable();
  }

  public sendData(data: any, type: RequestTypes): void {
    this.initSocket();

    const message = {
      type: type,
      payload: data,
    };

    this.wsSubject.subscribe(
      (msg) => {
        this.handleOfferSent(msg);
        this.socketConnected =
          !this.wsSubject.closed && !this.wsSubject.isStopped;
      },
      (err) => console.log(err),
      () => {
        console.log('complete');
      }
    );

    this.wsSubject.next(message);
    if (type == RequestTypes.serverOffer) {
      console.log(`setting offer sent to true`)
      this.offerSent = true;
    }
  }

  public onOffer(): Observable<RTCSessionDescriptionInit> {
    return this.offerResponseObservable;
  }

  public onNewICE(): Observable<RTCIceCandidate> {
    return this.newICEObservable;
  }

  private initSocket(): void {
    console.log(`Opening socket for address ${this.ws_address}`);
    this.wsSubject = webSocket(this.ws_address);
    this.wsSubject.subscribe();
  }

  public cleanFlags(): void {
    this.socketConnected = false;
    this.offerSent = false;
    console.log(`setting offer sent to false`)
    this.offerAccepted = false;
  }

  private handleOfferSent(msg: any): void {
    var JO = JSON.parse(msg);
    var responseType = JO['ResponseType'];

    switch (responseType) {
      case 'ServerOfferResponse':
        console.log(`response type: ServerOfferResponse`);
        this.offerAccepted = true;
        break;
      case 'NewICEAvailable':
        console.log(`NewICEAvailable accepted`);
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
