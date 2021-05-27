import { Component, OnInit } from '@angular/core';
import { RequestTypes } from '../common/requestTypesEnum';
import { SocketCommunicationService } from './services/socket-communication.service';

@Component({
  selector: 'app-socket-handling',
  templateUrl: './socket-handling.component.html',
  styleUrls: ['./socket-handling.component.css'],
})
export class SocketHandlingComponent implements OnInit {
  constructor(
    private socketMessagingService: SocketCommunicationService
  ) {}

  ngOnInit(): void {
    this.socketMessagingService.onOffer().subscribe(async (offer) => {
      console.log(`Received offer: \n${JSON.stringify(offer)}`);
      var answer = await this.socketMessagingService.setPeerOffer(offer);
    });
  }

  async registerApp(): Promise<void> {
    const offer = await this.socketMessagingService.createOffer();
    this.socketMessagingService.send(offer, RequestTypes.serverOffer);
  }
}
