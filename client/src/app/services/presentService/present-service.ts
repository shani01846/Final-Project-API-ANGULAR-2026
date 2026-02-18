import { Injectable } from '@angular/core';
import { environment } from '../../../envirement/envirement';
import { HttpClient } from '@angular/common/http';
import { map, Observable } from 'rxjs';
import { Present } from '../../models/Present';

@Injectable({
  providedIn: 'root',
})
export class PresentService {
  private apiUrl = `${environment.apiUrl}/Present`;
/**
 *
 */
constructor( private http:HttpClient){}
getAllPresents():Observable<Present[]>{
  return this.http.get<Present[]>(`${this.apiUrl}/paged`).pipe(
    map((response: any) => response?.items || [])
  );
}

getById(id:number):Observable<Present>{
  return this.http.get<Present>(`${this.apiUrl}/${id}`);
}

update(id:number,present:Present):Observable<Present>{
return this.http.put<Present>(`${this.apiUrl}/${id}`,present)
}


create(present:Present ):Observable<Present>
{
  return this.http.post<Present>(this.apiUrl,present);
}

delete(id:number):Observable<Present>
{
  return this.http.delete<Present>(`${this.apiUrl}/${id}`);
}

getPresentByCategory(categoryId:number):Observable<Present>
{
  return this.http.get<Present>(`${this.apiUrl}/category/${categoryId}`)
}
}
