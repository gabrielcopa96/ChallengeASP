import type { EntityDto, PagedAndSortedResultRequestDto } from '@abp/ng.core';

export interface CreateUpdateViajeDto {
  fechaSalida?: string;
  fechaLlegada?: string;
  origen?: string;
  destino?: string;
  medioTransporte?: string;
  pasajerosNames: string[];
  coordinador?: string;
}

export interface ViajeDto extends EntityDto<string> {
  fechaSalida?: string;
  fechaLlegada?: string;
  origen?: string;
  destino?: string;
  medioTransporte?: string;
  pasajerosNames: string[];
  coordinador?: string;
}

export interface ViajeGetListInput extends PagedAndSortedResultRequestDto {
}
