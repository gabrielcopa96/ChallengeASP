import { NgModule } from '@angular/core';
import { SharedModule } from '../shared/shared.module';
import { PasajeroRoutingModule } from './pasajero-routing.module';
import { PasajeroComponent } from './pasajero.component';


@NgModule({
  declarations: [
    PasajeroComponent
  ],
  imports: [
    PasajeroRoutingModule,
    SharedModule
  ]
})
export class PasajeroModule { }
