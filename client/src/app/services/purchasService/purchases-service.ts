import { Injectable } from '@angular/core';
import { environment } from '../../../envirement/envirement';
import { HttpClient } from '@angular/common/http';
import { Purchase } from '../../models/Purchase';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class PurchasesService {
   private apiUrl = `${environment.apiUrl}/Purchase`;
/**
 *
 */
constructor( private http:HttpClient){}
getAllPresents():Observable<Purchase[]>{
  return this.http.get<Purchase[]>(`${this.apiUrl}`)
}

getById(id:number):Observable<Purchase>{
  return this.http.get<Purchase>(`${this.apiUrl}/${id}`);
}

update(id:number,present:Purchase):Observable<Purchase>{
return this.http.put<Purchase>(`${this.apiUrl}/${id}`,present)
}


create(purchase:Purchase ):Observable<Purchase>
{
  return this.http.post<Purchase>(this.apiUrl,Purchase);
}

delete(id:number):Observable<Purchase>
{
  return this.http.delete<Purchase>(`${this.apiUrl}/${id}`);
}

getPurchasesForPresent(presentId:number):Observable<Purchase[]>
{
  return this.http.get<Purchase[]>(`${this.apiUrl}/present/${presentId}`)
}
 
}
