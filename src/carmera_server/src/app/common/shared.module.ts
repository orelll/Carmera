import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatButtonModule } from '@angular/material/button';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatIconModule } from '@angular/material/icon';
import { MatCheckboxModule } from '@angular/material/checkbox';

import { FlexLayoutModule } from '@angular/flex-layout';
import { FormsModule } from '@angular/forms';

var materialModules = [
  MatIconModule,
  MatButtonModule,
  MatToolbarModule,
  MatCheckboxModule,
];

@NgModule({
  declarations: [],
  imports: [FormsModule, CommonModule, materialModules, FlexLayoutModule],
  exports: [FormsModule, materialModules, FlexLayoutModule],
})
export class SharedModule {}
