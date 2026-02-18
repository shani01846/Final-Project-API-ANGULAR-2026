import { Component,inject,OnInit } from '@angular/core';
import { MenubarModule } from 'primeng/menubar';
import { MenuItem } from 'primeng/api';
import { AuthService } from '../../services/authService/auth-service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-navbar',
  imports: [MenubarModule],
  templateUrl: './navbar.html',
  styleUrl: './navbar.css',
})
export class Navbar implements OnInit {
authSrv = inject(AuthService)
  router:Router = inject(Router);

    items: MenuItem[] | undefined;
    ngOnInit(){
this.updateMenu()

this.authSrv.loginStatusChanged.subscribe(() => {
    this.updateMenu();
  });
    }
    updateMenu() {
const isAdmin  = this.authSrv.getRole() === 'Admin';
const isLoggedIn = !!this.authSrv.getToken();
        this.items = [
            {              
                label: 'Home',
                icon: 'pi pi-home',
                routerLink: '/'
            },
            {
              label: 'Admin',
              visible:isAdmin,
                icon: 'pi pi-home',
                 items: [
                    {
                        label: 'Presents',
                        icon: 'pi pi-bolt',
                        routerLink: '/admin'

                    },
                    {
                        label: 'Donors',
                        icon: 'pi pi-server',
                        routerLink: '/donors'
                    }
                ]
            },
            {
                label: 'Gifts',
                icon: 'pi pi-star',
                routerLink: '/presents'
            },
            {
                label: 'Sign-In',
                icon: 'pi pi-star',
                routerLink: '/sign-in',
                visible: !isLoggedIn
            },
            // {
            //     label: 'Donors',
            //     icon: 'pi pi-search',
               
            // },
            {
                label: 'Cart',
                icon: 'pi pi-envelope',
                routerLink: '/cart',
                visible: isLoggedIn

            },
            {
        label: 'Logout',
        icon: 'pi pi-sign-out',
        command: () => { this.logout(); },
        visible: isLoggedIn
      }
        ];
    }

    logout(){
        this.authSrv.logout()
                this.router.navigate(['/']);

this.updateMenu()
    }
}
