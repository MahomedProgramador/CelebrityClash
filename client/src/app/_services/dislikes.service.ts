import { inject, Injectable, signal } from '@angular/core';
import { environment } from '../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { Member } from '../_models/members';
import { PaginatedResult } from '../_models/pagination';
import { setPaginatedResponse, setPaginationHeaders } from './paginationHelper';

@Injectable({
  providedIn: 'root'
})
export class DislikesService {
  baseUrl = environment.apiUrl;
  private http = inject(HttpClient);
  dislikesIds = signal<number[]>([]);
  paginatedResult = signal<PaginatedResult<Member[]> | null>(null); // 

  toggleDislike(targetId: number){
    return this.http.post(`${this.baseUrl}dislikes/${targetId}`, {})
  }

  getDislikes(predicate: string, pageNumber: number, pageSize: number){
    let params = setPaginationHeaders(pageNumber, pageSize);

    params = params.append('predicate', predicate);

    return this.http.get<Member[]>(`${this.baseUrl}dislikes`,
       {observe: 'response', params}).subscribe({
        next: response => setPaginatedResponse(response, this.paginatedResult)
       })
  }

  getDislikesIds(){
    return this.http.get<number[]>(`${this.baseUrl}dislikes/list`).subscribe({
      next: ids => this.dislikesIds.set(ids)
    })
  }
}
