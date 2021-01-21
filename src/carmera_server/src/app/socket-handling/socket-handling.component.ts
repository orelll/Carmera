import { Component, OnInit } from '@angular/core';
import { SocketCommunicationService } from './services/socket-communication.service';
import { SocketerService } from './services/socketer.service';

@Component({
  selector: 'app-socket-handling',
  templateUrl: './socket-handling.component.html',
  styleUrls: ['./socket-handling.component.css'],
})
export class SocketHandlingComponent implements OnInit {
  constructor(
    private socketCommunicationService: SocketCommunicationService,
    private socketer: SocketerService
  ) {}

  ngOnInit(): void {}

  async registerApp(): Promise<void> {
    const offer = await this.socketer.createOffer();
    this.socketCommunicationService.send(offer);
  }
}
