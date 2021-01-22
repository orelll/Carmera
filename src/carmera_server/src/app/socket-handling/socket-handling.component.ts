import { Component, OnInit } from '@angular/core';
import { RequestTypes } from '../common/requestTypesEnum';
import { SocketCommunicationService } from './services/socket-communication.service';
import { SocketerService } from './services/socketer.service';

@Component({
  selector: 'app-socket-handling',
  templateUrl: './socket-handling.component.html',
  styleUrls: ['./socket-handling.component.css'],
})
export class SocketHandlingComponent implements OnInit {
  constructor(
    private socketMessagingService: SocketCommunicationService,
    private socketer: SocketerService
  ) {}

  ngOnInit(): void {
    this.socketMessagingService.onOffer().subscribe(async (offer) => {
      console.log(`Received offer: \n${JSON.stringify(offer)}`);
      var answer = await this.socketer.setPeerOffer(offer);
    });
  }

  async registerApp(): Promise<void> {
    const offer = await this.socketer.createOffer();
    this.socketMessagingService.send(offer, RequestTypes.serverOffer);
  }
}
