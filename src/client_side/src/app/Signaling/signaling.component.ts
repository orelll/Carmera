import { Component, OnInit } from '@angular/core';
import { RequestTypes } from '../common/requestTypesEnum';
import { SocketMessagingService } from './services/socket-messaging.service';
import { SocketerService } from './services/socketer.service';

@Component({
  selector: 'app-signaling',
  templateUrl: './signaling.component.html',
  styleUrls: ['./signaling.component.css']
})
export class SignalingComponent implements OnInit {


  constructor(private socketMessagingService: SocketMessagingService,
    private socketer: SocketerService) { }

  ngOnInit(): void {
    this.socketMessagingService.onOffer().subscribe(async (offer) => {
      console.log(`Received offer: \n${JSON.stringify(offer)}`);
      var answer = await this.socketer.setPeerOffer(offer);
      this.socketMessagingService.send(answer, RequestTypes.answer);
    });
  }

  async doRegistration(): Promise<void> {
    this.socketMessagingService.send(await this.socketer.createOffer(), RequestTypes.offer);
  }

}
