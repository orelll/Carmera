import { Injectable } from '@angular/core';
import { Message } from '../model/message';

import { Observable } from 'rxjs/internal/Observable';
import { Event } from '../model/event';

const SERVER_URL = '127.0.0.1:5000';

@Injectable({
  providedIn: 'root',
})
export class SocketService {
  private socket: WebSocket;

  public initSocket(): void {
    this.socket = new WebSocket(`ws://${SERVER_URL}`);
    this.socket.onopen = function (event) {
      this.send("Connecting!");
    };
  }

  public send(message: string): void {
    var msg = {
      type: 'message',
      text: message,
      date: Date.now()
    };

    var msgAsText = JSON.stringify(msg);

    console.log(`Sending ${msgAsText}`);
    this.socket.send(msgAsText);
  }

  public onMessage(): Observable<Message> {
    return new Observable<Message>((observer) => {
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
