import { CoreModule } from '@abp/ng.core';
import { NgbDropdownModule } from '@ng-bootstrap/ng-bootstrap';
import { NgModule } from '@angular/core';
import { ThemeSharedModule } from '@abp/ng.theme.shared';
import { NgxValidateCoreModule } from '@ngx-validate/core';
import { NgbDatepickerModule } from '@ng-bootstrap/ng-bootstrap';
import { ButtonModule } from 'primeng/button';
import { InputTextModule } from 'primeng/inputtext';
import { MultiSelectModule } from 'primeng/multiselect';
import { DialogModule } from 'primeng/dialog';
import { CalendarModule } from 'primeng/calendar';
import { DropdownModule } from 'primeng/dropdown';
import { ChipModule } from 'primeng/chip';

@NgModule({
  declarations: [],
  imports: [
    CoreModule,
    ThemeSharedModule,
    NgbDropdownModule,
    NgxValidateCoreModule,
    NgbDatepickerModule,
    ButtonModule,
    InputTextModule,
    MultiSelectModule,
    DialogModule,
    CalendarModule,
    DropdownModule,
    ChipModule
    
  ],
  exports: [
    CoreModule,
    ThemeSharedModule,
    NgbDropdownModule,
    NgxValidateCoreModule,
    NgbDatepickerModule,
    ButtonModule,
    InputTextModule,
    MultiSelectModule,
    DialogModule,
    CalendarModule,
    DropdownModule,
    ChipModule
  ],
  providers: []
})
export class SharedModule {}
