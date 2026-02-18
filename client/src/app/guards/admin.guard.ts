import { inject } from '@angular/core';
import { Router, CanActivateFn } from '@angular/router';
import { AuthService } from '../services/authService/auth-service';

export const adminGuard: CanActivateFn = (route, state) => {
  const router = inject(Router);
  const authService = inject(AuthService);
  const token = localStorage.getItem('auth_token');
const role = authService.getRole(); 
console.log("inside guard");
console.log(role);

 if (token && role === 'Admin') {
    return true; 
  } else {
    router.navigate(['/']); 
    return false;
  }
};