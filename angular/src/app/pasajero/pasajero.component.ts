import { ListService, PagedResultDto } from '@abp/ng.core';
import { Confirmation, ConfirmationService } from '@abp/ng.theme.shared';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { PasajeroDto, PasajeroService } from '@proxy/pasajeros';
import { BreakpointObserver, Breakpoints, BreakpointState } from '@angular/cdk/layout';

@Component({
  selector: 'app-pasajero',
  templateUrl: './pasajero.component.html',
  styleUrls: ['./pasajero.component.scss'],
  providers: [ListService]
})
export class PasajeroComponent implements OnInit {

  pasajero = { items: [], totalCount: 0 } as PagedResultDto<PasajeroDto>

  form: FormGroup;

  selectedPasajero = {} as PasajeroDto;

  sizeDisplay: boolean;

  isModalOpen = false;

  constructor(
    public readonly list: ListService,
    private PasajeroService: PasajeroService,
    private fb: FormBuilder,
    private confirmation: ConfirmationService,
    private breakpointObserver: BreakpointObserver
  ) { }

  ngOnInit(): void {
    const pasajeroStreamCreator = (query) => this.PasajeroService.getList(query);

    this.list.hookToQuery(pasajeroStreamCreator).subscribe((response) => {
      this.pasajero = response;
    });

    this.mediaQuery();
  }

  mediaQuery() {

    this.breakpointObserver.observe([
      Breakpoints.Web
    ]).subscribe((state: BreakpointState) => {
      this.sizeDisplay = state.matches
    });

  }

  createPasajero() {
    this.selectedPasajero = {} as PasajeroDto;
    this.buildForm();
    this.isModalOpen = true;
  }

  editPasajero(id: string) {
    this.PasajeroService.get(id).subscribe(pasajero => {
      this.selectedPasajero = pasajero;
      this.buildForm();
      this.isModalOpen = true;
    })
  }

  deletePasajero(id: string) {
    this.confirmation.warn('::AreYouSureToDelete', '::AreYouSure').subscribe(status => {
      if (status === Confirmation.Status.confirm) {
        this.PasajeroService.delete(id).subscribe(() => this.list.get());
      }
    });

  }

  buildForm() {
    this.form = this.fb.group({
      Nombre: [this.selectedPasajero.nombre || '', Validators.required],
      Apellido: [this.selectedPasajero.apellido || '', Validators.required],
      Dni: [this.selectedPasajero.dni || '', Validators.required],
      FechaNacimiento: [this.selectedPasajero.fechaNacimiento ? new Date(this.selectedPasajero.fechaNacimiento) : null, Validators.required],
    });
  }

  save() {
    if (this.form.invalid) return;

    const request = this.selectedPasajero.id
      ? this.PasajeroService.update(this.selectedPasajero.id, this.form.value)
      : this.PasajeroService.create(this.form.value);

    request.subscribe(() => {
      this.isModalOpen = false;
      this.form.reset();
      this.list.get();
    });
  }

}


