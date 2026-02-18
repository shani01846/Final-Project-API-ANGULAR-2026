import { inject, Injectable } from '@angular/core';
import { environment } from '../../../envirement/envirement';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Present } from '../../models/Present';
import { Purchase } from '../../models/Purchase';

@Injectable({
  providedIn: 'root',
})
export class CartService {
  private apiUrl = `${environment.apiUrl}/Purchase`;

  http:HttpClient = inject(HttpClient);

  getDraftPurchases(userId: number): Observable<Purchase[]>
  {
    return this.http.get<Purchase[]>(`${this.apiUrl}/cart/${userId}`);
  }

  deletePresentFromCart(presentId:number):Observable<any>
  {
    return this.http.delete(`${this.apiUrl}/${presentId}`);
  }

  addPresentToCart(purchase:Purchase):Observable<Purchase>
  {
    return this.http.post<Purchase>(`${this.apiUrl}`, purchase);
  }

}
