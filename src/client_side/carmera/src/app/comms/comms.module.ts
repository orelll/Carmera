import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CommsComponent } from './comms.component';
import { SocketService } from './shared/services/socket.service';


@NgModule({
  declarations: [CommsComponent],
  imports: [
    CommonModule,
    SocketService
  ]
})
export class CommsModule { }
