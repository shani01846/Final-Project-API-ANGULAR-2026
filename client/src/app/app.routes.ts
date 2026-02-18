import { Routes } from '@angular/router';
import { SignIn } from './componenets/sign-in/sign-in';
import { Cart } from './componenets/cart/cart';
import { HomePage } from './componenets/home-page/home-page';
import { Presents } from './componenets/presents/presents';
import { Admin } from './componenets/Admin/presentCRUD/admin';
import { DonorsList } from './componenets/Admin/donors-list/donors-list';
import { Register } from './componenets/register/register';
import { authGuard } from './guards/auth.guard';
import { adminGuard } from './guards/admin.guard';
console.log("hiii");

export const routes: Routes = [
{path:'', component:HomePage},
{path:'admin', component: Admin,canActivate:[adminGuard] },
{path  :'sign-in', component:SignIn},
{path: 'cart',component:Cart,canActivate:[authGuard] },
{path:'presents', component:Presents},
{path:'donors', component:DonorsList,canActivate:[adminGuard] },
{path:'register', component:Register}
];

