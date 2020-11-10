import { Component, OnInit } from '@angular/core';
import { SocketingService } from '../common/services/socketing.service';

@Component({
  selector: 'app-signaling',
  templateUrl: './signaling.component.html',
  styleUrls: ['./signaling.component.css']
})
export class SignalingComponent implements OnInit {

  localOffer;

  constructor(private socketingService: SocketingService) { }

  ngOnInit(): void {
    this.prepareOffer();
  }

  async prepareOffer(): Promise<void> {
    const configuration = { 'iceServers': [{ 'urls': 'stun:stun.l.google.com:19302' }] }
    const peerConnection = new RTCPeerConnection(configuration);

    this.localOffer = await peerConnection.createOffer();;
  }

  doRegistration(): void {
    this.socketingService.onMessage().subscribe((message : any) => {
      console.log(message);
    });

    this.socketingService.send(this.localOffer);
  }

}
