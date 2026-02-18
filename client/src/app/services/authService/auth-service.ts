import { inject, Injectable } from '@angular/core';
import { environment } from '../../../envirement/envirement';
import { HttpClient } from '@angular/common/http';
import { User } from '../../models/Purchase';
import { jwtDecode } from 'jwt-decode';
import { BehaviorSubject, Subject } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private apiUrl = `${environment.apiUrl}/Auth`;
http:HttpClient = inject(HttpClient);

private loggedIn = new BehaviorSubject<boolean>(!!localStorage.getItem('auth_token'));
 isLoggedIn$ = this.loggedIn.asObservable();

 loginStatusChanged = new Subject<void>();
//login

  login(email:string,password:string) {

return this.http.post(`${this.apiUrl}/login`, { email, password });
}



register(user:User)
{
  return this.http.post<User>(`${this.apiUrl}/register`, user);
}


saveToken(token: string) {
  localStorage.setItem('auth_token', token);
  this.loggedIn.next(true); 
this.loginStatusChanged.next();
}
getToken() {
  return localStorage.getItem('auth_token');
}

getRole(): string | null {
    const token = localStorage.getItem('auth_token');
    
    if (!token) return null;

    try {
      const decoded: any = jwtDecode(token);
      console.log(decoded);
      console.log("role",decoded.role);
      
      
      return decoded.role
    } catch (error) {
      return null;
    }
  }
logout() {
    localStorage.removeItem('auth_token');
    this.loggedIn.next(false); 
    this.loginStatusChanged.next();
  }

  getUserId(): number | null {
  const token = localStorage.getItem('auth_token');
  if (!token) return null;

  try {
    const decoded: any = jwtDecode(token);
    console.log("tokennnnn:",decoded);
    
    // השתמשי בשם השדה כפי שהוא מופיע בטוקן שלך
    return decoded.sub
  } catch (error) {
    return null;
  }
}

}