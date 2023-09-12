import { NgModule } from '@angular/core';
import { SharedModule } from '../shared/shared.module';
import { ViajesRoutingModule } from './viajes-routing.module';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { ViajesComponent } from './viajes.component';


@NgModule({
  declarations: [
    ViajesComponent
  ],
  imports: [
    ViajesRoutingModule,
    SharedModule,
    ReactiveFormsModule,
    FormsModule
  ]
})
export class ViajesModule { }
