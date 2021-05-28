import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { WebRTCCommComponent } from './components/web-rtc-comm/web-rtc-comm.component';
import { SharedModule } from '../common/shared.module';
import { Router, RouterModule, Routes } from '@angular/router';
import { FlexLayoutModule } from '@angular/flex-layout';

const routes: Routes = [
  {
    path: '',
    component: WebRTCCommComponent,
  },
];

@NgModule({
  declarations: [WebRTCCommComponent],
  imports: [CommonModule, SharedModule, FlexLayoutModule , RouterModule.forChild(routes)],
  exports: [WebRTCCommComponent],
})
export class WebRtcModule {}
