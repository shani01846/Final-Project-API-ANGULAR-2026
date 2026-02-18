import { inject, Injectable } from '@angular/core';
import { environment } from '../../../envirement/envirement';
import { HttpClient } from '@angular/common/http';
import { LotteryResult, User } from '../../models/Purchase';

@Injectable({
  providedIn: 'root',
})
export class LotteryService {
    private apiUrl = `${environment.apiUrl}/LotteryResult`;
  
    http:HttpClient = inject(HttpClient);
getAllWinners(){
    return this.http.get<LotteryResult[]>(`${this.apiUrl}`);

}

makeLottery(presentId:number){
    return this.http.get<User>(`${this.apiUrl}/makeLottery/${presentId}`); 
}
}
