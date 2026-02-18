import { inject, Injectable } from '@angular/core';
import { environment } from '../../../envirement/envirement';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Category } from '../../models/Purchase';

@Injectable({
  providedIn: 'root',
})
export class CategoryService {
    private apiUrl = `${environment.apiUrl}/Category`;

  http:HttpClient = inject(HttpClient);

  getAllCategories(): Observable<Category[]>
  {
    return this.http.get<Category[]>(`${this.apiUrl}`);
  }

  // deletePresentFromCart(presentId:number):Observable<any>
  // {
  //   return this.http.delete(`${this.apiUrl}/${presentId}`);
  // }

  // addPresentToCart(purchase:Purchase):Observable<Purchase>
  // {
  //   return this.http.post<Purchase>(`${this.apiUrl}`, purchase);
  // }

}
