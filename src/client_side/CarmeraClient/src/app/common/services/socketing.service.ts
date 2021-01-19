import { Injectable } from '@angular/core';
import { webSocket, WebSocketSubject } from 'rxjs/webSocket';

const subject = webSocket("wss://localhost:5011");

// check https://rxjs-dev.firebaseapp.com/api/webSocket/webSocket
@Injectable({
  providedIn: 'root'
})


export class SocketingService {
  constructor() {
    subject.subscribe( );
    subject.next({message: 'some message'});
  }


  public send(offer: RTCSessionDescriptionInit): void {
   
  }
}
