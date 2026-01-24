import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { InputGroupModule } from 'primeng/inputgroup';
import { InputGroupAddonModule } from 'primeng/inputgroupaddon';
import { InputTextModule } from 'primeng/inputtext';
import { SelectModule } from 'primeng/select';
@Component({
  selector: 'app-sign-in',
  imports: [FormsModule, InputGroupModule, InputGroupAddonModule, InputTextModule, SelectModule],
  templateUrl: './sign-in.html',
  styleUrl: './sign-in.css'
})
export class SignIn {
    text1: string | undefined;

    text2: string | undefined;
}
