import { Component, OnInit } from '@angular/core';
import { RequestTypes } from 'src/app/common/requestTypesEnum';
import { SocketCommunicationService } from 'src/app/socket-handling/services/socket-communication.service';
import { WebRtcService } from '../../services/web-rtc.service';

@Component({
  selector: 'app-web-rtc-comm',
  templateUrl: './web-rtc-comm.component.html',
  styleUrls: ['./web-rtc-comm.component.css'],
})
export class WebRTCCommComponent implements OnInit {
  constructor(
    public socketMessagingService: SocketCommunicationService,
    public webRTCService: WebRtcService
  ) {
    console.log('loaded');
  }

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
