import { Component, OnInit } from '@angular/core';

import { Action } from './shared/model/action';
import { Event } from './shared/model/event';
import { Message } from './shared/model/message';
import { User } from './shared/model/user';
import { SocketService } from './shared/services/socket.service';

@Component({
  selector: 'app-comms',
  templateUrl: './comms.component.html',
  styleUrls: ['./comms.component.css'],
})
export class CommsComponent implements OnInit {
  action = Action;
  user: User;
  messages: Message[] = [];
  messageContent: string;
  ioConnection: any;

  constructor(private socketService: SocketService) { }

  ngOnInit(): void {
  }

  public initWSConnection(): void {
    console.log(`Initializing socket...`);
    this.socketService.initSocket();

    this.ioConnection = this.socketService.onMessage().subscribe((message: Message) => {
      this.messages.push(message);
    });

  }

  public doCheckout() {
    var msg = {
      from: this.user,
      content: `checkout!`,
    };

    var msgAsText = JSON.stringify(msg);
    this.socketService.send(msgAsText);
  }

  public sendMessage(message: string): void {
    if (!message) {
      return;
    }

    var msg = {
      from: this.user,
      content: message,
    };

    var msgAsText = JSON.stringify(msg);

    this.socketService.send(msgAsText);
  }

  public closeWS() {
    this.socketService.closeSocket();
  }

  // public sendNotification(params: any, action: Action): void {
  //   let message: Message;

  //   if (action === Action.JOINED) {
  //     message = {
  //       from: this.user,
  //       action,
  //     };
  //   } else if (action === Action.RENAME) {
  //     message = {
  //       action,
  //       content: {
  //         username: this.user.name,
  //         previousUsername: params.previousUsername,
  //       },
  //     };
  //   }

  //   this.socketService.send(message);
  // }
}
