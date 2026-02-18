import { Component, inject, NgModule } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { first } from 'rxjs';
import { ButtonModule } from 'primeng/button';
import { CardModule } from 'primeng/card';
import { FormsModule } from '@angular/forms';
import { FloatLabelModule } from 'primeng/floatlabel';
import { InputTextModule } from 'primeng/inputtext';
import { User } from '../../models/Purchase';
import { AuthService } from '../../services/authService/auth-service';
@Component({
  selector: 'app-register',
  imports: [  ButtonModule,
  CardModule,
  FloatLabelModule,
  InputTextModule,
  ReactiveFormsModule],
  templateUrl: './register.html',
  styleUrl: './register.css',
})
export class Register {
user:User=new User();
authSrv:AuthService = inject(AuthService);
registerForm = new FormGroup({
  firstName: new FormControl('', [Validators.required]),
  lastName: new FormControl('', [Validators.required]),
  email: new FormControl('', [Validators.required, Validators.email]),
  address: new FormControl('', [Validators.required]),
  phone: new FormControl('', [Validators.required, Validators.pattern('^[0-9]+$')]),
  password: new FormControl('', [Validators.required, Validators.minLength(6)])
});

submit(){
  if(this.registerForm.invalid) {
 this.registerForm.markAllAsTouched(); 
      return;
}
const {firstName, lastName, email, address, phone, password} = this.registerForm.value;
this.user =  {firstName: firstName!, lastName: lastName!, email: email!, address: address!, phone: phone!, password: password!};
console.log(this.user);
this.authSrv.register(this.user).subscribe({
  next: (response) => {
    console.log('User registered successfully:', response);
  },
  error: (error) => {
    console.error('Error registering user:', error);
  }
});
}}
