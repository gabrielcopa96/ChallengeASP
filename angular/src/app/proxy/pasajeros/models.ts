import type { EntityDto, PagedAndSortedResultRequestDto } from '@abp/ng.core';

export interface CreateUpdatePasajeroDto {
  nombre?: string;
  apellido?: string;
  dni?: string;
  fechaNacimiento?: string;
}

export interface PasajeroDto extends EntityDto<string> {
  nombre?: string;
  apellido?: string;
  dni?: string;
  fechaNacimiento?: string;
}

export interface PasajeroNombreCompletoDto extends EntityDto<string> {
  nombreCompleto?: string;
  dni?: string;
  fechaNacimiento?: string;
}

export interface PasajeroGetListInput extends PagedAndSortedResultRequestDto {
}
