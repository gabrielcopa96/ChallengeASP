import { PasajeroService } from './../proxy/pasajeros/pasajero.service';
import { ListService, PagedResultDto } from '@abp/ng.core';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ViajeDto, ViajeService } from '@proxy/viajes';
import { NgbDateNativeAdapter, NgbDateAdapter } from '@ng-bootstrap/ng-bootstrap';
import { PasajeroNombreCompletoDto } from '@proxy/pasajeros';
import { Confirmation, ConfirmationService } from '@abp/ng.theme.shared';
import { BreakpointObserver, Breakpoints, BreakpointState } from '@angular/cdk/layout';

@Component({
  selector: 'app-viajes',
  templateUrl: './viajes.component.html',
  styleUrls: ['./viajes.component.scss'],
  providers: [ListService, { provide: NgbDateAdapter, useClass: NgbDateNativeAdapter }]
})
export class ViajesComponent implements OnInit {

  viaje = { items: [], totalCount: 0 } as PagedResultDto<ViajeDto>

  pasajeros = { items: [], totalCount: 0 } as PagedResultDto<PasajeroNombreCompletoDto>

  sizeDisplay: boolean;

  selectedViaje = {} as ViajeDto;

  form: FormGroup;

  date: Date;

  isModalOpen = false;

  constructor(
    public readonly list: ListService,
    private ViajeService: ViajeService,
    private PasajeroService: PasajeroService,
    private fb: FormBuilder,
    private confirmation: ConfirmationService,
    private breakpointObserver: BreakpointObserver
  ) { }

  ngOnInit(): void {
    const viajeStreamCreator = (query) => this.ViajeService.getList(query);

    const pasajeroStreamCreator = (query) => this.PasajeroService.getList(query);

    this.list.hookToQuery(viajeStreamCreator).subscribe((response) => {
      this.viaje = response;
    });

    this.list.hookToQuery(pasajeroStreamCreator).subscribe((response) => {
      const newDataPasajero = {
        ...response,
        items: response.items.map((pasajero) => {
          const nameFull = `${pasajero.nombre} ${pasajero.apellido}`
          return {
            nombreCompleto: nameFull,
            dni: pasajero.dni,
            fechaNacimiento: pasajero.fechaNacimiento
          }
        })
      }
      this.pasajeros = newDataPasajero;
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

  createViaje() {
    this.selectedViaje = {} as ViajeDto;
    this.buildForm();
    this.isModalOpen = true;
  }

  editarViaje(id: string) {
    this.ViajeService.get(id).subscribe(viaje => {
      this.selectedViaje = viaje
      this.buildForm();
      this.isModalOpen = true;
    })
  }

  deleteViaje(id: string) {
    this.confirmation.warn('::Are You Sure To Delete', '::AreYouSure').subscribe(status => {
      if (status === Confirmation.Status.confirm) {
        this.ViajeService.delete(id).subscribe(() => this.list.get());
      }
    });
  }

  buildForm() {
    let pasajeros = [];
    if (this.selectedViaje.pasajerosNames) {
      pasajeros = this.pasajeros.items.filter(pasajero => {
        return this.selectedViaje.pasajerosNames.includes(pasajero.nombreCompleto);
      })
    }

    this.form = this.fb.group({
      FechaSalida: [this.selectedViaje.fechaSalida ? new Date(this.selectedViaje.fechaSalida) : null, Validators.required],
      FechaLlegada: [this.selectedViaje.fechaLlegada ? new Date(this.selectedViaje.fechaLlegada) : null, Validators.required],
      Origen: [this.selectedViaje.origen || '', Validators.required],
      Destino: [this.selectedViaje.destino || '', Validators.required],
      MedioTransporte: [this.selectedViaje.medioTransporte || '', Validators.required],
      PasajerosNames: [pasajeros],
      Coordinador: [this.selectedViaje.coordinador || ''],
    });
  }

  save() {
    if (this.form.invalid) {
      return;
    }

    const pasajerosSeleccionados = this.form.value.PasajerosNames;

    const coordinadorSeleccionado = this.form.value.Coordinador.nombreCompleto;

    const listaPasajeros = pasajerosSeleccionados.map(pasajero => pasajero.dni);

    this.form.patchValue({
      PasajerosNames: listaPasajeros,
      Coordinador: coordinadorSeleccionado
    });

    const request = this.selectedViaje.id
      ? this.ViajeService.update(this.selectedViaje.id, this.form.value)
      : this.ViajeService.create(this.form.value);

    request.subscribe(() => {
      this.isModalOpen = false;
      this.form.reset();
      this.list.get();
    });
  }

}
