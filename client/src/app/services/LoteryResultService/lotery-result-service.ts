import { Injectable } from '@angular/core';
import { environment } from '../../../envirement/envirement';
import { HttpClient } from '@angular/common/http';
import { map, Observable } from 'rxjs';
import { LotteryResult } from '../../models/Purchase';
@Injectable({
  providedIn: 'root',
})
export class LoteryResultService {
    private apiUrl = `${environment.apiUrl}/LotteryResult`;
/**
 *
 */
constructor( private http:HttpClient){}
getAll():Observable<LotteryResult[]>{
  return this.http.get<LotteryResult[]>(`${this.apiUrl}`)
}

getById(id:number):Observable<LotteryResult>{
  return this.http.get<LotteryResult>(`${this.apiUrl}/${id}`);
}



create(lottery:LotteryResult ):Observable<LotteryResult>
{
  return this.http.post<LotteryResult>(this.apiUrl,LotteryResult);
}



}
