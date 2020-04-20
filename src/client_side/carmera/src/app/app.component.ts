import { Component } from '@angular/core';
import { webSocket } from 'rxjs/webSocket';
import { CheckInDTO } from '../app/common/checkinDTO';

export class Response {
  constructor(public success: boolean, public exception: string, public message: string) {}
}

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
})
export class AppComponent {
  title = 'carmera';
  url = 'ws://localhost:5000/ws';

  constructor() {}

  doCheckout() {
    const subject = webSocket(this.url);
    subject.subscribe(
      msg => console.log('message received: ' + msg), // Called whenever there is a message from the server.
      err => console.log(err), // Called if at any point WebSocket API signals some kind of error.
      () => console.log('complete') // Called when connection is closed (for whatever reason).
    );

    const dto: CheckInDTO = { kind: 'checkin', peerName: 'carmera'};
    subject.subscribe();
    subject.next(dto);
    subject.complete();
  }
}
