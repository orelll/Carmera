import { Injectable } from '@angular/core';
import { Message } from '../model/message';

import * as socketIo from 'socket.io-client';
import { Observable } from 'rxjs/internal/Observable';
import { Event } from '../model/event';

const SERVER_URL = 'http://localhost:5000';

@Injectable({
  providedIn: 'root',
})
export class SocketService {
  private socket;

  public initSocket(): void {
    this.socket = socketIo(SERVER_URL);
  }

  public send(message: Message): void {
    this.socket.emit('message', message);
  }

  public onMessage(): Observable<Message> {
    return new Observable<Message>((observer) => {
      this.socket.on('message', (data: Message) => observer.next(data));
    });
  }

  public onEvent(event: Event): Observable<any> {
    return new Observable<Event>((observer) => {
      this.socket.on(event, () => observer.next());
    });
  }
}
