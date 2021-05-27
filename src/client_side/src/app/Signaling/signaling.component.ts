import {
  AfterViewInit,
  Component,
  ElementRef,
  OnInit,
  ViewChild,
} from '@angular/core';
import { RequestTypes } from '../common/requestTypesEnum';
import { SocketMessagingService } from './services/socket-messaging.service';

@Component({
  selector: 'app-signaling',
  templateUrl: './signaling.component.html',
  styleUrls: ['./signaling.component.css'],
})
export class SignalingComponent implements OnInit, AfterViewInit {
  @ViewChild('remoteVideo') video: ElementRef;

  constructor(private socketMessagingService: SocketMessagingService) {}
  ngAfterViewInit(): void {
    this.socketMessagingService.setVideoElement(this.video);
  }

  ngOnInit(): void {
    this.socketMessagingService.onOffer().subscribe(async (offer) => {
      console.log(`Received offer: \n${JSON.stringify(offer)}`);
      var answer = await this.socketMessagingService.setPeerOffer(offer);
      this.socketMessagingService.send(answer, RequestTypes.answer);
    });
  }

  async doRegistration(): Promise<void> {
    this.socketMessagingService.send(
      await this.socketMessagingService.createOffer(),
      RequestTypes.offer
    );
  }
}
