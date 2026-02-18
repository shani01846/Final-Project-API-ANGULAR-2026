import { Component, inject } from '@angular/core';
import { FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { ButtonModule } from 'primeng/button';
import { InputGroupModule } from 'primeng/inputgroup';
import { InputGroupAddonModule } from 'primeng/inputgroupaddon';
import { InputTextModule } from 'primeng/inputtext';
import { SelectModule } from 'primeng/select';
import { AuthService } from '../../services/authService/auth-service';
import { Router, RouterModule } from '@angular/router';
import { FloatLabelModule } from 'primeng/floatlabel';
import { CardModule } from 'primeng/card';
import { jwtDecode } from 'jwt-decode';
import { MessageService } from 'primeng/api';
import { ToastModule } from 'primeng/toast';
@Component({
  selector: 'app-sign-in',
  imports: [FormsModule, InputTextModule,ToastModule,RouterModule,ReactiveFormsModule,FloatLabelModule,CardModule,ButtonModule],
  templateUrl: './sign-in.html',
  styleUrl: './sign-in.css',
  providers:[MessageService]

})

export class SignIn {
  authSrv:AuthService = inject(AuthService);
  router:Router = inject(Router);
  private messageService:MessageService = inject(MessageService)

    loginForm = new FormGroup({
      email: new FormControl('', [Validators.required, Validators.email]),
      password: new FormControl('', [Validators.required, Validators.minLength(6)])
    });


    login() {

       if(this.loginForm.invalid) {
 this.loginForm.markAllAsTouched(); 
      return;
    }

    const { email, password } = this.loginForm.value;
    this.authSrv.login(email!, password!).subscribe( {
      next: (response: any) => {

      this.authSrv.saveToken(response.token);
this.messageService.add({ 
        severity: 'success', 
        summary: 'התחברות הצליחה', 
        detail: 'ברוך הבא למערכת!',
        life: 3000
      });

      setTimeout(() => {
        this.router.navigate(['/']);
      }, 1000); 
    
    },
    error: (err) => {

      console.error('Login failed:', err);
      alert('שם משתמש או סיסמה שגויים');
    }
    });
}
}


