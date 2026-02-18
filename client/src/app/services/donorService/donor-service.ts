import { inject, Injectable } from '@angular/core';
import { environment } from '../../../envirement/envirement';
import { HttpClient } from '@angular/common/http';
import { Donor } from '../../models/Purchase';

@Injectable({
  providedIn: 'root',
})
export class DonorService {
  private apiUrl = `${environment.apiUrl}/Donor`;

  http:HttpClient = inject(HttpClient);

  getAllDonors(){
    return this.http.get<Donor[]>(`${this.apiUrl}`);
  }

  deleteDonor(id:number)
  {
        return this.http.delete<boolean>(`${this.apiUrl}/${id}`);
  }

  createDonor(donor:Donor)
  {
      return this.http.post<Donor>(`${this.apiUrl}`,donor);

  }
  updateDonor(donor:Donor)
  {
    return this.http.put<Donor>(`${this.apiUrl}/${donor.id}`,donor);

  }

  getDonorByName(name:string){
    return this.http.get<Donor>(`${this.apiUrl}/name/${name}`);
  }

  getDonorByEmail(email:string){
    return this.http.get<Donor>(`${this.apiUrl}/email/${email}`);
  }
  getDonorByPresentId(presentId:number){
    return this.http.get<Donor>(`${this.apiUrl}/presentId/${presentId}`);
  }

  getDonorById(id:number){
    return this.http.get<Donor>(`${this.apiUrl}/${id}`);
  }
}
