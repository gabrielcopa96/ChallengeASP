import type { CreateUpdateViajeDto, ViajeDto, ViajeGetListInput } from './models';
import { RestService, Rest } from '@abp/ng.core';
import type { PagedResultDto } from '@abp/ng.core';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class ViajeService {
  apiName = 'Default';
  

  create = (input: CreateUpdateViajeDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, void>({
      method: 'POST',
      url: '/api/app/viaje',
      body: input,
    },
    { apiName: this.apiName,...config });
  

  delete = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, void>({
      method: 'DELETE',
      url: `/api/app/viaje/${id}`,
    },
    { apiName: this.apiName,...config });
  

  get = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, ViajeDto>({
      method: 'GET',
      url: `/api/app/viaje/${id}`,
    },
    { apiName: this.apiName,...config });
  

  getList = (input: ViajeGetListInput, config?: Partial<Rest.Config>) =>
    this.restService.request<any, PagedResultDto<ViajeDto>>({
      method: 'GET',
      url: '/api/app/viaje',
      params: { sorting: input.sorting, skipCount: input.skipCount, maxResultCount: input.maxResultCount },
    },
    { apiName: this.apiName,...config });
  

  update = (id: string, input: CreateUpdateViajeDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, void>({
      method: 'PUT',
      url: `/api/app/viaje/${id}`,
      body: input,
    },
    { apiName: this.apiName,...config });

  constructor(private restService: RestService) {}
}
